using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public class CollisionSystem : ECSSystem
{
    public void Update()
    {
        Dictionary<HexPoint, List<int>> lookup_grid = new();

        //register entities to lookup grid
        foreach (var entity in m_entities)
        {
            ref var transform = ref ecs.GetComponent<Transform>(entity);
            ref var collider = ref ecs.GetComponent<CircleCollider>(entity);
            int grid_level = transform.grid_level;

            HexPoint hex_point = new HexPoint(transform.pos);

            //check collisions with hex tiles
            HexPoint[] hexes = hex_point.GetThisAndNeighbors();
            foreach (var hex in hexes)
            {
                //check if tile exists and is a wall
                if (hex.q >= 0 && hex.r >= 0 &&
                hex.q < GS.grids[grid_level].diameter &&
                hex.r < GS.grids[grid_level].diameter &&
                GS.grids[grid_level].tiles[hex.q, hex.r].collision)
                {
                    Vector2 hex_world_pos = hex.ToWorldPos();
                    Vector2 diff = transform.pos - hex_world_pos;

                    //cut test for if target is definitely too far away
                    float dist = diff.Length();
                    if (dist >= collider.radius + 1f)
                    {
                        // Console.WriteLine("outside");
                        continue;
                    }

                    //go edge by edge (6 edge) compute distance to edge
                    int bin = ((int)MathF.Floor(MathZ.Atan2(diff) * 3 / MathF.PI) + 6) % 6;
                    Vector2 a, b;
                    switch (bin)
                    {
                        case 0:
                            a = hex_world_pos + new Vector2(1, 0);
                            b = hex_world_pos + new Vector2(0.5f, 0.8660254038f);
                            break;
                        case 1:
                            a = hex_world_pos + new Vector2(0.5f, 0.8660254038f);
                            b = hex_world_pos + new Vector2(-0.5f, 0.8660254038f);
                            break;
                        case 2:
                            a = hex_world_pos + new Vector2(-0.5f, 0.8660254038f);
                            b = hex_world_pos + new Vector2(-1, 0);
                            break;
                        case 3:
                            a = hex_world_pos + new Vector2(-1, 0);
                            b = hex_world_pos + new Vector2(-0.5f, -0.8660254038f);
                            break;
                        case 4:
                            a = hex_world_pos + new Vector2(-0.5f, -0.8660254038f);
                            b = hex_world_pos + new Vector2(0.5f, -0.8660254038f);
                            break;
                        case 5:
                            a = hex_world_pos + new Vector2(0.5f, -0.8660254038f);
                            b = hex_world_pos + new Vector2(1, 0);
                            break;
                        default:
                            a = Vector2.One;
                            b = Vector2.One;
                            break;
                    }
                    Vector2 ab = b - a;
                    float t = Vector2.Dot(transform.pos - a, ab) / Vector2.Dot(ab, ab);
                    t = MathF.Max(0, MathF.Min(1, t)); // clamp to [0,1]
                    Vector2 closest = a + t * ab;
                    float distance = (transform.pos - closest).Length();
                    // Console.WriteLine($"{bin}, {closest}, {transform.pos - closest}");
                    if (hex_point == hex)
                    {
                        transform.pos = closest - collider.radius * (transform.pos - closest) / distance;
                    }
                    else if (distance <= collider.radius)
                    {
                        transform.pos = closest + collider.radius * (transform.pos - closest) / distance;
                    }
                }
            }

            hex_point = new HexPoint(transform.pos / .1f);
            if (lookup_grid.ContainsKey(hex_point))
            {
                lookup_grid[hex_point].Add(entity);
            }
            else
            {
                lookup_grid[hex_point] = new List<int>() { entity };
            }
        }

        // apply circle collisions
        List<(int, int)> collision_list = new();
        HexPoint[] next_neighbors =
        [
            new HexPoint(1, 0),
            new HexPoint(1, -1),
            new HexPoint(0, -1)
        ];
        foreach (var kvp in lookup_grid)
        {
            HexPoint tile = kvp.Key;
            List<int> entities = kvp.Value;

            for (int v1 = 0; v1 < entities.Count; v1++)
            {
                for (int v2 = v1 + 1; v2 < entities.Count; v2++)
                {
                    int i = entities[v1];
                    int j = entities[v2];
                    Vector2 diff = ecs.GetComponent<Transform>(i).pos - ecs.GetComponent<Transform>(j).pos;
                    float rad1 = ecs.GetComponent<CircleCollider>(i).radius;
                    float rad2 = ecs.GetComponent<CircleCollider>(j).radius;
                    if (diff.LengthSquared() < (rad1 + rad2) * (rad1 + rad2))
                        collision_list.Add((i, j));
                }
            }

            foreach (var neighbor in next_neighbors)
            {
                var true_neighbor = neighbor + tile;
                if (!lookup_grid.TryGetValue(true_neighbor, out var entities_adj))
                    continue;
                foreach (int i in entities)
                {
                    foreach (int j in entities_adj)
                    {
                        Vector2 diff = ecs.GetComponent<Transform>(i).pos - ecs.GetComponent<Transform>(j).pos;
                        float rad1 = ecs.GetComponent<CircleCollider>(i).radius;
                        float rad2 = ecs.GetComponent<CircleCollider>(j).radius;
                        if (diff.LengthSquared() < (rad1 + rad2) * (rad1 + rad2))
                            collision_list.Add((i, j));
                    }
                }
            }
        }

        // calculate circle collision push
        foreach (var pair in collision_list)
        {
            int a = pair.Item1;
            int b = pair.Item2;
            ref Transform transform1 = ref ecs.GetComponent<Transform>(a);
            ref Transform transform2 = ref ecs.GetComponent<Transform>(b);
            CircleCollider collider1 = ecs.GetComponent<CircleCollider>(a);
            CircleCollider collider2 = ecs.GetComponent<CircleCollider>(b);
            Vector2 diff = transform1.pos - transform2.pos;
            float dist = diff.Length();
            float target_distance = collider1.radius + collider2.radius;
            float dist_diff = target_distance - dist;
            Vector2 dir;
            if (dist == 0)
                dir = new Vector2(1, 0);
            else
                dir = Vector2.Normalize(diff);
            Vector2 push = dir * dist_diff * 0.5f;
            transform1.push += push;
            transform2.push -= push;
        }

        // apply circle collision push
        float push_max = 1f;
        foreach (var entity in m_entities)
        {
            ref Transform transform = ref ecs.GetComponent<Transform>(entity);
            if (transform.push.LengthSquared() >= push_max * push_max)
            {
                transform.push *= push_max / transform.push.Length();
            }
            transform.pos += transform.push;
            transform.push.X = 0;
            transform.push.Y = 0;
        }
    }
}