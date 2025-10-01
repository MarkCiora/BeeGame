using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public enum TileShape
{
    none, dot, line2, line3, L, tri, hex
}

public struct TileOccupier
{
    public int grid;
    public int x;
    public int y;
    public int orientation;
    public TileShape shape;
    public BuildingType type;

    public static readonly Dictionary<TileShape, (int dq, int dr)[]> footprints = new()
    {
        { TileShape.dot, new[] { (0,0) } },
        { TileShape.line2, new[] { (0,0), (0,1) } },
        { TileShape.line3, new[] { (0,0), (0,1), (0,-1) } },
        { TileShape.L, new[] { (0,0), (1,0), (-1,1) } },
        { TileShape.tri, new[] { (0,0), (1,0), (1,-1) } },
        { TileShape.hex, new[] { (0,0), (1,0), (0,1), (-1,1), (-1,0), (0,-1) , (1,-1)} }
    };

    public static readonly Dictionary<TileShape, Vector2> footprint_offsets = new()
    {
        { TileShape.dot, new Vector2(0,0) },
        { TileShape.line2, new Vector2(0,-MathF.Sqrt(3)) },
        { TileShape.line3, new Vector2(0,0) },
        { TileShape.L, new Vector2(0,-0.5f) },
        { TileShape.tri, new Vector2(1,0) },
        { TileShape.hex, new Vector2(0,0) },
    };

    public static readonly Dictionary<BuildingType, TileShape> building_to_shape = new()
    {
        { BuildingType.HoneyComb, TileShape.dot},
        { BuildingType.Pool, TileShape.tri},
    };
}

/// <summary>
/// All possible building types
/// </summary>
public enum BuildingType
{
    none, HoneyComb, Pool
}

