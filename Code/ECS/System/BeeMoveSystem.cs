using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public class BeeMoveSystem : ECSSystem
{
    public void Update()
    {
        foreach (var entity in m_entities)
        {
            ref var transform = ref ecs.GetComponent<Transform>(entity);
            var move_desc = ecs.GetComponent<MovementDescriptor>(entity);

            Vector2 move_diff = move_desc.move_target - transform.pos;
            float distance = move_diff.Length();
            if (distance < transform.scale / 2)
            {
                transform.speed = MathF.Max(
                    0f,
                    transform.speed - Time.dt * move_desc.acc
                );
            }
            else
            {
                transform.rot = MathZ.Atan2(move_diff);
                transform.speed = MathF.Min(
                    move_desc.max_speed,
                    transform.speed + Time.dt * move_desc.acc
                );
            }
            Vector2 dir = MathZ.DirFromRot(transform.rot);
            transform.pos += dir * transform.speed * Time.dt;
        }
    }
}