using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class GameObjVisualizer
{
    public static void Visualize()
    {
        Visuals.sb.Begin();
        foreach (GameObj obj in GameObj.obj_dict.Values)
        {
            DrawToCamera(GS.main_camera, obj);
        }
        Visuals.sb.End();
    }

    public static void DrawToCamera(Camera cam, GameObj obj)
    {
        if (obj.deleted) return;
        if (obj == null) return;
        Texture2D tex = obj.GetTexture();
        Visuals.sb.Draw(
            tex,
            cam.WorldToScreen(obj.pos),
            new Rectangle(0, 0, tex.Height, tex.Height),
            obj.tint,
            MathF.PI * 0.5f - obj.rot,
            new Vector2((float)tex.Height * 0.5f, (float)tex.Height * 0.5f),
            obj.scale * cam.zoom / (float)tex.Height,
            SpriteEffects.None,
            0f
        );
    }
}