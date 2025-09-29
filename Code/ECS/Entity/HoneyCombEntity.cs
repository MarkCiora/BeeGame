using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class HoneyCombEntity
{
    public static int CreateHoneyComb(ECSCoordinator ecs, HexPoint hex_pos, int grid)
    {
        int id = ecs.CreateEntity();

        TileOccupier tile_occupier = new();
        tile_occupier.grid = grid;
        tile_occupier.x = hex_pos.q;
        tile_occupier.y = hex_pos.r;
        // tile_occupier.orientation = 0;
        // tile_occupier.shape = TileShape.dot;
        ecs.AddComponent(id, tile_occupier);

        Sprite sprite = new();
        sprite.texture = Textures.BeeComb1;
        sprite.tint = Color.White;
        ecs.AddComponent(id, sprite);

        Transform transform = new();
        transform.pos = hex_pos.ToWorldPos();
        transform.can_move = false;
        ecs.AddComponent(id, transform);


        return id;
    }
}
