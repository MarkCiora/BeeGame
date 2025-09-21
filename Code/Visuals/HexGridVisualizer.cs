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

public class HexGridVisualizer
{
    HexGrid grid;

    public HexGridVisualizer(HexGrid grid_)
    {
        grid = grid_;
        int size = grid.diameter;
    }

    public void Visualize()
    {
        Visuals.sb.Begin(effect: Shaders.HexTileShader);
        Texture2D white_pixel = Textures.white_pixel;
        for (int i = 0; i < grid.diameter; i++)
        {
            for (int j = 0; j < grid.diameter; j++)
            {
                Vector2 world_pos = grid[i, j].WorldPos();
                Vector2 screen_pos = GS.main_camera.WorldToScreen(world_pos);
                float zoom = GS.main_camera.zoom;
                float scale = zoom * 2;
                Rectangle rect = new Rectangle(
                    (int)(screen_pos.X - scale * 0.5),
                    (int)(screen_pos.Y - scale * 0.5),
                    (int)scale,
                    (int)scale
                );
                Visuals.sb.Draw(white_pixel, rect, Color.Red);
            }
        }
        Visuals.sb.End();

        Visuals.sb.Begin();
        Visuals.sb.End();
    }

    public void Update(HexPoint point)
    {

    }
}