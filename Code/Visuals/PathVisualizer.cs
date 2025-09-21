using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class PathVisualizer
{
    public static void Visualize()
    {
        Visuals.sb.Begin();
        foreach (Bee bee in Bee.bee_dict.Values)
        {
            if (bee.deleted) continue;
            MovePath path = bee.move_path;
            if (!path.IsReady()) continue;
            List<Vector2> remaining = path.RemainingPath(bee.pos);
            for (int i = 0; i < remaining.Count - 1; i++)
            {
                DrawLineWorld(remaining[i], remaining[i + 1]);
            }
        }
        Visuals.sb.End();
    }

    public static void DrawLineWorld(Vector2 p1, Vector2 p2)
    {
        Vector2 start = GS.main_camera.WorldToScreen(p1);
        Vector2 end = GS.main_camera.WorldToScreen(p2);
        Vector2 delta = end - start;
        float length = delta.Length();
        float rotation = MathF.Atan2(delta.Y, delta.X);
        float thickness = 5f;

        Visuals.sb.Draw(
            Textures.white_pixel,
            position: start,
            sourceRectangle: null,
            color: new Color(1,1,1,0.5f),
            rotation: rotation,
            origin: Vector2.Zero,
            scale: new Vector2(length, thickness),
            effects: SpriteEffects.None,
            layerDepth: 0
        );
    }
}