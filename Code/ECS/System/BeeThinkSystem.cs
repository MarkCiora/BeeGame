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
        foreach (var entity in m_entities)
        {
            var transform = ecs.GetComponent<Transform>(entity);
            ref var move_desc = ref ecs.GetComponent<MovementDescriptor>(entity);
        }
    }
}