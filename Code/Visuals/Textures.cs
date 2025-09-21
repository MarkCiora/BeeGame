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
    public static Texture2D white_pixel;

    public static Texture2D apple1;
    public static Texture2D basic_tile;
    public static Texture2D Bee1_Sheet;
    public static Texture2D guy1;
    public static Texture2D guy2_active;
    public static Texture2D guy2_idle;
    public static Texture2D tile_highlight;

    public static void LoadContent()
    {
        ContentManager cm = Visuals.cm;
        GraphicsDevice gd = Visuals.gd;

        white_pixel = new Texture2D(gd, 1, 1);
        white_pixel.SetData(new[] { Color.White });

        apple1         = cm.Load<Texture2D>("apple1");
        basic_tile     = cm.Load<Texture2D>("basic_tile");
        Bee1_Sheet     = cm.Load<Texture2D>("Bee1_Sheet");
        guy1           = cm.Load<Texture2D>("guy1");
        guy2_active    = cm.Load<Texture2D>("guy2_active");
        guy2_idle      = cm.Load<Texture2D>("guy2_idle");
        tile_highlight = cm.Load<Texture2D>("tile_highlight");
    }
}