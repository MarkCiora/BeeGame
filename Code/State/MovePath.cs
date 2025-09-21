using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace BeeGame;

public class MovePath
{
    public List<Vector2> path_list;
    public int index;

    public MovePath()
    {
        path_list = null;
        index = 0;
    }

    /// <summary>
    /// Creates path from start to end
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns>False if unreachable</returns>
    public void MakeNewPath(Vector2 start, Vector2 goal, HexGrid grid)
    {
        path_list = AStar(start, goal, grid);
        index = 0;
    }

    public Vector2 GetStep(Vector2 pos, float size)
    {
        while ((path_list[index] - pos).Length() <= size * 0.5f)
        {
            index += 1;
            if (index >= path_list.Count)
            {
                path_list = null;
                return pos;
            }
        }
        return path_list[index];
    }

    public bool IsReady()
    {
        return !(path_list == null || index >= path_list.Count);
    }

    public List<Vector2> RemainingPath(Vector2 pos)
    {
        // List<Vector2> path = [pos, .. path_list.Skip(index)];
        // return path;
        return [pos, .. path_list.Skip(index)];
    }
    
    public static List<Vector2> AStar(Vector2 start, Vector2 goal, HexGrid grid)
    {
        HexPoint start_hex = new(start);
        HexPoint goal_hex = new(goal);
        var open_set = new PriorityQueue<HexPoint, float>();
        var came_from = new Dictionary<HexPoint, HexPoint>();
        var g_score = new Dictionary<HexPoint, int> { [start_hex] = 0 };

        open_set.Enqueue(start_hex, 0);

        while (open_set.Count > 0)
        {
            var current = open_set.Dequeue();

            if (current == goal_hex) // reconstruct path, return
            {
                List<Vector2> path = new() { goal, current.ToWorldPos() };
                while (came_from.TryGetValue(current, out var prev))
                {
                    current = prev;
                    path.Add(current.ToWorldPos());
                }
                path.Reverse();
                return path;
            }

            foreach (var neighbor in current.GetNeighbors())
            {
                if (grid[neighbor] == null || !grid[neighbor].CanWalk()) continue; // skip blocked hexes

                int tentative_g = g_score[current] + 1; // all edges cost 1
                if (!g_score.TryGetValue(neighbor, out int old_g) || tentative_g < old_g)
                {
                    came_from[neighbor] = current;
                    g_score[neighbor] = tentative_g;

                    float f_score = tentative_g + 0.0f*(neighbor - current).Length();
                    // float f_score = tentative_g + neighbor.EuclidDistance(goal_hex);
                    open_set.Enqueue(neighbor, f_score);
                }
            }
        }

        return null; // no path
    }

    // private static List<HexPoint> ReconstructPath(Dictionary<HexPoint, HexPoint> cameFrom, HexPoint current)
    // {
    //     var path = new List<HexPoint> { current };
    //     while (cameFrom.TryGetValue(current, out var prev))
    //     {
    //         current = prev;
    //         path.Add(current);
    //     }
    //     path.Reverse();
    //     return path;
    // }
}