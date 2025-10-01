using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class Textures
{

    public static Texture2D white_pixel; //0
    public static Texture2D apple1; //1
    public static Texture2D basic_tile; //2
    public static Texture2D Bee1_Sheet; //3
    public static Texture2D guy1; //4
    public static Texture2D guy2_active; //5
    public static Texture2D guy2_idle; //6
    public static Texture2D tile_highlight; //7
    public static Texture2D BeeComb1; //8
    public static Texture2D white_circle;
    public static Texture2D GooPool;

    public static void LoadContent()
    {
        ContentManager cm = Visuals.cm;
        GraphicsDevice gd = Visuals.gd;

        white_pixel = new Texture2D(gd, 1, 1);
        white_pixel.SetData(new[] { Color.White });

        apple1 = cm.Load<Texture2D>("apple1");
        basic_tile = cm.Load<Texture2D>("basic_tile");
        Bee1_Sheet = cm.Load<Texture2D>("Bee1_Sheet");
        guy1 = cm.Load<Texture2D>("guy1");
        guy2_active = cm.Load<Texture2D>("guy2_active");
        guy2_idle = cm.Load<Texture2D>("guy2_idle");
        tile_highlight = cm.Load<Texture2D>("tile_highlight");
        BeeComb1 = cm.Load<Texture2D>("BeeComb1");
        white_circle = CreateCircleTexture(gd, 32, Color.White);
        GooPool = cm.Load<Texture2D>("GooPool");
    }

    public static Texture2D CreateCircleTexture(GraphicsDevice gd, int radius, Color color)
    {
        int size = radius * 2;
        var tex = new Texture2D(gd, size, size, false, SurfaceFormat.Color);
        Color[] data = new Color[size * size];

        float rSq = radius * radius;
        float cx = radius - 0.5f;
        float cy = radius - 0.5f;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float dx = x - cx;
                float dy = y - cy;
                float distSq = dx * dx + dy * dy;

                // filled circle
                float thickness = 2f;
                if (distSq <= rSq && distSq >= (radius - thickness) * (radius - thickness))
                    data[y * size + x] = color;
                else
                    data[y * size + x] = Color.Transparent;
            }
        }

        tex.SetData(data);
        return tex;
    }
}