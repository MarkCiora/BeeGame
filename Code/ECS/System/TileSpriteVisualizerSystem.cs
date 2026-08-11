using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public class TileSpriteVisualizerSystem : ECSSystem
{
    public void Update()
    {
        Camera camera = GS.main_cameras[GS.focused_grid];
        Visuals.sb.Begin(samplerState: SamplerState.PointClamp);
        foreach (var entity in m_entities)
        {
            var tile_occupier = ecs.GetComponent<TileOccupier>(entity);
            int x = tile_occupier.x;
            int y = tile_occupier.y;
            var sprite = ecs.GetComponent<Sprite>(entity);
            var tex = sprite.texture;
            Vector2 world_pos = new HexPoint(x,y).ToWorldPos();
            Vector2 offset = TileOccupier.footprint_offsets[tile_occupier.shape];
            Vector2 screen_pos = camera.WorldToScreen(world_pos + offset);
            Rectangle rect = new(0, 0, tex.Height, tex.Height);
            float rot = 2f * MathF.PI * tile_occupier.orientation / 6f;

            //lookup scale based on building shape
            float scale;
            switch (tile_occupier.type)
            {
                case BuildingType.HoneyComb:
                    scale = 2f * camera.zoom / tex.Height;
                    break;
                case BuildingType.Pool:
                    scale = 4f * camera.zoom / tex.Height;
                    break;
                default:
                    scale = 2f * camera.zoom / tex.Height;
                    break;
            }

            Visuals.sb.Draw(
                sprite.texture, // tex
                screen_pos, // pos
                rect,
                sprite.tint, // tint
                -rot, // rotation
                new Vector2(tex.Height * 0.5f, tex.Height * 0.5f), // center
                scale, // scale
                SpriteEffects.None, // shader?
                0f
            );
        }
        Visuals.sb.End();
    }
}