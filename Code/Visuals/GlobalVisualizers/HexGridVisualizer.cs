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

public static class HexGridVisualizer
{
    public static void Visualize()
    {
        HexGrid grid = GS.grids[GS.focused_grid];
        HexTile[,] tiles = grid.tiles;
        int diameter = grid.diameter;
        Camera camera = GS.main_cameras[GS.focused_grid];

        Visuals.sb.Begin(effect: Shaders.HexTileShader);
        Texture2D white_pixel = Textures.white_pixel;
        for (int i = 0; i < diameter; i++)
        {
            for (int j = 0; j < diameter; j++)
            {
                Color color = tiles[i,j].type == 0 ? Color.Brown : Color.Gray;
                HexPoint hex_pos = new HexPoint(i,j);
                Vector2 world_pos = hex_pos.ToWorldPos();
                Vector2 screen_pos = camera.WorldToScreen(world_pos);
                float zoom = camera.zoom;
                float scale = zoom * 2;
                Rectangle rect = new Rectangle(
                    (int)(screen_pos.X - scale * 0.5),
                    (int)(screen_pos.Y - scale * 0.5),
                    (int)scale,
                    (int)scale
                );
                Visuals.sb.Draw(white_pixel, rect, color);
            }
        }
        Visuals.sb.End();

        Visuals.sb.Begin();
        Visuals.sb.End();
    }
}