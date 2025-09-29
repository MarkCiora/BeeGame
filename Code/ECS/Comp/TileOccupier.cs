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
    no, dot, line2, line3, L, tri, hex

}

public struct TileOccupier
{
    public int grid;
    public int x;
    public int y;
    public int orientation;
    public TileShape shape;

    public static readonly Dictionary<TileShape, (int dq, int dr)[]> footprints = new()
    {
        { TileShape.dot, new[] { (0,0) } },
        { TileShape.line2, new[] { (0,0), (1,0) } },
        { TileShape.line3, new[] { (0,0), (1,0), (-1,0) } },
        { TileShape.L, new[] { (0,0), (1,0), (-1,1) } },
        { TileShape.tri, new[] { (0,0), (1,0), (0,1) } },  // tweak as needed
        { TileShape.hex, new[] { (0,0), (1,0), (0,1), (-1,1), (-1,0), (0,-1) , (1,-1)} }
    };
}


