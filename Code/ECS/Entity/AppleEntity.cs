using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class AppleEntity
{
    public static int CreateApple(ECSCoordinator ecs, Vector2 pos)
    {
        int id = ecs.CreateEntity();

        Transform transform = new();
        transform.pos = pos;
        ecs.AddComponent(id, transform);

        Sprite sprite = new();
        sprite.texture = 1;
        sprite.tint = Color.White;
        ecs.AddComponent(id, sprite);

        return id;
    }
}
