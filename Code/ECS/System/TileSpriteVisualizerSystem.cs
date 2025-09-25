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
        Visuals.sb.Begin();
        foreach (var entity in m_entities)
        {
            var tile_occupier = ecs.GetComponent<TileOccupier>(entity);
            int x = tile_occupier.x;
            int y = tile_occupier.y;
            var sprite = ecs.GetComponent<Sprite>(entity);
            var tex = Textures.array[sprite.texture];
            Vector2 world_pos = new HexPoint(x,y).ToWorldPos();
            Vector2 screen_pos = camera.WorldToScreen(world_pos);
            Rectangle rect = new(0, 0, tex.Height, tex.Height);
            float scale = 2f * camera.zoom / tex.Height;
            float rot = 2f * MathF.PI * tile_occupier.orientation / 6f;

            Visuals.sb.Draw(
                tex, // tex
                screen_pos, // pos
                rect,
                sprite.tint, // tint
                MathF.PI * 0.5f - rot, // rotation
                new Vector2(tex.Height * 0.5f, tex.Height * 0.5f), // center
                scale, // scale
                SpriteEffects.None, // shader?
                0f
            );
        }
        Visuals.sb.End();
    }
}