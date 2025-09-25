using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class BeeEntity
{
    public static int CreateBee(ECSCoordinator ecs, Vector2 pos, int grid)
    {
        int id = ecs.CreateEntity();

        Transform transform = new();
        transform.pos = pos;
        transform.scale = 0.3f;
        transform.grid_level = grid;
        ecs.AddComponent(id, transform);

        Sprite sprite = new();
        sprite.texture = 3;
        sprite.tint = Color.White;
        ecs.AddComponent(id, sprite);

        MovementDescriptor movement_descriptor = new();
        movement_descriptor.max_speed = 0.5f;
        movement_descriptor.acc = 2f;
        ecs.AddComponent(id, movement_descriptor);

        CircleCollider collider = new();
        collider.radius = 0.12f;
        ecs.AddComponent(id, collider);

        return id;
    }
}
