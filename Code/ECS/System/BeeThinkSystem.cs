using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public class BeeThinkSystem : ECSSystem
{
    public void Update()
    {
        Vector2 mouse_pos = GS.main_cameras[GS.focused_grid].MousePosInWorld();

        foreach (var entity in m_entities)
        {
            var transform = ecs.GetComponent<Transform>(entity);
            ref var move_desc = ref ecs.GetComponent<MovementDescriptor>(entity);

            move_desc.move_target = mouse_pos;
        }
    }
}