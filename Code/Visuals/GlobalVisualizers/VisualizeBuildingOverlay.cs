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

public static class VisualizeBuildingOverlay
{
    public static void Visualize()
    {
        if (BuildingTools.build_mode)
        {
            Visuals.sb.Begin(
                // effect: Shaders.BuildingBlueprintShader
                effect: Shaders.BuildingPrePlacementShader
                // samplerState: SamplerState.PointClamp
            );

            // draw background ?

            // draw selected building
            Camera camera = GS.main_cameras[GS.focused_grid];
            HexPoint hex_point = BuildingTools.selected_hex;
            Vector2 screen_pos = camera.WorldToScreen(hex_point.ToWorldPos() + BuildingTools.hover_offset);
            float rotation = BuildingTools.orientation * MathF.PI / 3;
            Texture2D tex;
            float scale;
            switch (BuildingTools.selected_type)
            {
                case BuildingType.HoneyComb:
                    tex = Textures.BeeComb1;
                    scale = 2f * camera.zoom / tex.Height;
                    break;
                case BuildingType.Pool:
                    tex = Textures.GooPool;
                    scale = 4f * camera.zoom / tex.Height;
                    break;
                default:
                    tex = Textures.white_circle;
                    scale = 2f * camera.zoom / tex.Height;
                    break;
            }
            Visuals.sb.Draw(
                tex, // tex
                screen_pos, // pos
                null,
                // new Color(0.5f, 0.5f, 0.5f, 1f),
                Color.White,
                -rotation, // rotation
                new Vector2(tex.Height * 0.5f, tex.Height * 0.5f), // center
                scale, // scale
                SpriteEffects.None, // shader?
                0f
            );

            // Console.WriteLine(screen_pos);

            Visuals.sb.End();
        }
    }
}