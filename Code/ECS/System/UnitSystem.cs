using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public class UnitSystem : ECSSystem
{

    public int CreateUnit(UnitType type, Vector2 pos, int grid)
    {
        int id = ecs.CreateEntity();

        switch (type)
        {
            case UnitType.Bee:
                {
                    Transform transform = new();
                    transform.pos = pos;
                    transform.scale = 0.3f;
                    transform.grid_level = grid;
                    ecs.AddComponent(id, transform);

                    Sprite sprite = new();
                    sprite.texture = Textures.Bee1_Sheet;
                    sprite.tint = Color.White;
                    ecs.AddComponent(id, sprite);

                    MovementDescriptor movement_descriptor = new();
                    movement_descriptor.move_target = transform.pos;
                    movement_descriptor.move_intent = false;
                    ecs.AddComponent(id, movement_descriptor);

                    CircleCollider collider = new();
                    collider.radius = 0.1f;
                    ecs.AddComponent(id, collider);

                    Inventory inventory = new();
                    inventory.type = ResourceType.None;
                    inventory.amount = 0;
                    inventory.capacity = 1;
                    ecs.AddComponent(id, inventory);

                    TaskActor task_component = new();
                    task_component.target = -1;
                    ecs.AddComponent(id, task_component);

                    break;
                }

            case UnitType.Apple:
                {
                    Transform transform = new();
                    transform.pos = pos;
                    transform.scale = 0.2f;
                    transform.grid_level = grid;
                    ecs.AddComponent(id, transform);

                    Sprite sprite = new();
                    sprite.texture = Textures.apple1;
                    sprite.tint = Color.White;
                    ecs.AddComponent(id, sprite);

                    TaskTarget task_target = new();
                    task_target.actor = -1;
                    task_target.type = TaskType.Collect;
                    ecs.AddComponent(id, task_target);

                    Resource resource = new();
                    resource.type = ResourceType.Fruit;
                    resource.amount = 1;
                    ecs.AddComponent(id, resource);

                    break;
                }

            default:
                ecs.DestroyEntity(id);
                return -1;
        }

        Unit unit = new();
        unit.type = type;
        ecs.AddComponent(id, unit);

        return id;
    }
}
