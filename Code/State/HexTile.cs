using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public enum TileType
{
    Null,
    Dirt,
    Stone
}

public class HexTile
{
    public HexPoint pos;
    public TileType type;
    public int elevation;

    public HexTile(HexPoint point, TileType ttype = TileType.Null)
    {
        pos = point;
        type = ttype;
        switch (type)
        {
            case TileType.Null:
                elevation = 2;
                break;
            case TileType.Dirt:
                elevation = 0;
                break;
            case TileType.Stone:
                elevation = 0;
                break;
            default:
                elevation = 2;
                break;
        }
    }

    public Vector2 WorldPos()
    {
        return pos.ToWorldPos();
    }

    public Texture2D GetTexture()
    {
        switch (type)
        {
            case TileType.Null:
                return Textures.basic_tile;
            case TileType.Dirt:
                return Textures.basic_tile;
            case TileType.Stone:
                return Textures.basic_tile;
            default:
                return null;
        }
    }

    public bool CanWalk()
    {
        // return elevation == 0;
        return true;
    }
}