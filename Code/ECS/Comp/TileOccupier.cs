using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public struct TileOccupier
{
    public int grid;
    public int x;
    public int y;
    public int orientation;
    public TileShape shape;
}

public enum TileShape
{
    no, dot, line2, line3, L, tri, hex
}