using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;


/// <summary>
/// q r s defined hex grid. q up, r down right, s down left.
/// index asixally [q,r] (internally converted) or cubically [q,r,s] 
/// (must satisfy q+r+s=0)
/// </summary>
public class HexGrid
{
    public HexTile[,] tiles;
    public int radius, diameter;
    public HexPoint origin;
    public HexGridVisualizer visualizer;

    public HexGrid(int r = 5)
    {
        radius = r;
        diameter = 2 * r + 1;
        tiles = new HexTile[diameter, diameter];
        origin = new HexPoint(r, r);

        for (int i = 0; i < diameter; i++)
        {
            for (int j = 0; j < diameter; j++)
            {
                HexPoint point = new HexPoint(i, j) - origin;
                if (point.Length() <= radius)
                {
                    tiles[i, j] = new HexTile(point);
                }
                else
                {
                    tiles[i, j] = new HexTile(point);
                    tiles[i, j].type = TileType.Null;
                }
            }
        }
    }

    public void LoadVisualizer()
    {
        visualizer = new HexGridVisualizer(this);
    }

    public void Visualize()
    {
        visualizer.Visualize();
    }

    public HexTile this[HexPoint point]
    {
        get
        {
            int x = point.q + origin.q;
            int y = point.r + origin.r;
            if (x >= 0 && y >= 0 && x < diameter && y < diameter)
            {
                return tiles[x, y];
            }
            else return null;
        }
        set
        {
            int x = point.q + origin.q;
            int y = point.r + origin.r;
            if (x >= 0 && y >= 0 && x < diameter && y < diameter)
            {
                tiles[x, y] = value;
            }
        }
    }

    public HexTile this[int x, int y]
    {
        get
        {
            if (x >= 0 && y >= 0 && x < diameter && y < diameter)
            {
                return tiles[x, y];
            }
            else return null;
        }
        set
        {
            if (x >= 0 && y >= 0 && x < diameter && y < diameter)
            {
                tiles[x, y] = value;
            }
        }
    }

    public HexTile this[Vector2 vec]
    {
        get
        {
            HexPoint point = new HexPoint(vec);
            return this[point];
        }
        set
        {
            HexPoint point = new HexPoint(vec);
            this[point] = value;
        }
    }

    public bool InBounds(HexPoint point)
    {
        return point.Length() <= radius && tiles[point.q, point.r].type != TileType.Null;
    }

    public bool InBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && x < diameter && y < diameter;
    }

}