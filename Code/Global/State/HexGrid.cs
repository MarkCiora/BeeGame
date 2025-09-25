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
    public int elevation; // 0 ground level
    public byte type; // 0 null
    public bool collision; // indicates if has a collider
    public bool built; // true if built on
    public int occupied_entity; // id of building (if built)
}

public struct HexGrid
{
    public int diameter;
    public HexTile[,] tiles;
}