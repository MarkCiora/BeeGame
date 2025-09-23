using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;


public struct HexTile
{
    public int type; // 0 null
    public int elevation; // 0 ground level
}

public struct HexGrid
{
    public int diameter;
    public HexTile[,] tiles;
}