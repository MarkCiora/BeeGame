using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class Visuals
{
    public static SpriteBatch sb;
    public static ContentManager cm;
    public static GraphicsDevice gd;

    public static void Init(SpriteBatch sb, ContentManager cm)
    {
        Visuals.sb = sb;
        Visuals.cm = cm;
        gd = sb.GraphicsDevice;
        Textures.LoadContent();
        Console.WriteLine("Textures Loaded");
        Shaders.LoadContent();
        Console.WriteLine("Shaders Loaded");
    }

    public static void Draw(Texture2D tex, Vector2 pos, float rot, float scale, Color tint)
    {
        sb.Draw(tex,
        pos,
        null,
        tint,
        rot,
        new Vector2((float)tex.Width * 0.5f, (float)tex.Height * 0.5f),
        scale,
        SpriteEffects.None,
        0f);
    }

    public static void Draw(Texture2D tex, Point pos, float rot, float scale, Color tint)
    {
        sb.Draw(tex,
        new Vector2(pos.X, pos.Y),
        null,
        tint,
        rot,
        new Vector2((float)tex.Width * 0.5f, (float)tex.Height * 0.5f),
        scale,
        SpriteEffects.None,
        0f);
    }

    public static void DrawToCamera(Camera cam, Texture2D tex, Vector2 pos, float rot, float scale, Color tint)
    {
        scale = scale * cam.zoom / (float)tex.Height;
        pos = cam.WorldToScreen(pos);
        Draw(tex, pos, rot, scale, tint);
    }

    public static void DrawToCamera(Camera cam, Texture2D tex, Point pos, float rot, float scale, Color tint)
    {
        scale = scale * cam.zoom / (float)tex.Height;
        Vector2 pos_ = new Vector2(pos.X, pos.Y);
        pos_ = cam.WorldToScreen(pos_);
        Draw(tex, pos_, rot, scale, tint);
    }


    public static void Update()
    {
        ECSCoordinator ecs = GS.main_ecs;

        HexGridVisualizer.Visualize();

        ecs.GetSystem<TileSpriteVisualizerSystem>().Update();
        ecs.GetSystem<SpriteVisualizerSystem>().Update();

    }

    public static Point ScreenDims()
    {
        return new Point(sb.GraphicsDevice.Viewport.Width, sb.GraphicsDevice.Viewport.Height);
    }
}