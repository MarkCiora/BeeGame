using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class GlobalLogic
{
    public static void Init()
    {
        // Global state initialization
        HexGridLogic.Init();


        // new global main ecs
        GS.main_ecs = new();
        var ecs = GS.main_ecs;


        // Register components
        ecs.RegisterComponent<Transform>();
        ecs.RegisterComponent<Sprite>();
        ecs.RegisterComponent<TileOccupier>();
        ecs.RegisterComponent<MovementDescriptor>();
        ecs.RegisterComponent<CircleCollider>();


        // Register systems
        Signature signature;

        //Sprite visualizer system
        signature = new();
        ecs.RegisterSystem<SpriteVisualizerSystem>();
        signature.Set(ecs.GetComponentType<Transform>(), true);
        signature.Set(ecs.GetComponentType<Sprite>(), true);
        ecs.SetSystemSignature<SpriteVisualizerSystem>(signature);

        //Bee think system
        signature = new();
        ecs.RegisterSystem<BeeThinkSystem>();
        signature.Set(ecs.GetComponentType<Transform>(), true);
        signature.Set(ecs.GetComponentType<MovementDescriptor>(), true);
        ecs.SetSystemSignature<BeeThinkSystem>(signature);

        //Bee move system
        signature = new();
        ecs.RegisterSystem<BeeMoveSystem>();
        signature.Set(ecs.GetComponentType<Transform>(), true);
        signature.Set(ecs.GetComponentType<MovementDescriptor>(), true);
        ecs.SetSystemSignature<BeeMoveSystem>(signature);

        //Tile sprite visualizer
        signature = new();
        ecs.RegisterSystem<TileSpriteVisualizerSystem>();
        signature.Set(ecs.GetComponentType<TileOccupier>(), true);
        signature.Set(ecs.GetComponentType<Sprite>(), true);
        ecs.SetSystemSignature<TileSpriteVisualizerSystem>(signature);
        
        //Collision system
        signature = new();
        ecs.RegisterSystem<CollisionSystem>();
        signature.Set(ecs.GetComponentType<CircleCollider>(), true);
        signature.Set(ecs.GetComponentType<Transform>(), true);
        ecs.SetSystemSignature<CollisionSystem>(signature);


        // Startup entity creation
        Vector2 center_of_grid = HexGridLogic.GetCenter(GS.grids[GS.focused_grid]).ToWorldPos();
        for (int i = 0; i < 5; i++)
        {
            float displacement = MathZ.rand.NextSingle();
            displacement = MathF.Sqrt(displacement) * 1;
            Vector2 placement_pos = center_of_grid + MathZ.RandomDirV() * displacement;
            BeeEntity.CreateBee(ecs, placement_pos, 0);
        }
        HoneyCombEntity.CreateHoneyComb(ecs, new HexPoint(2, 2), 0);
    }

    public static void Update()
    {
        var ecs = GS.main_ecs;

        if (Input.IsPressed(Keys.F11))
        {
            Screen.SetFullscreen(true);
        }

        if (Input.IsPressed(Keys.Space))
        {
            Vector2 center_of_grid = HexGridLogic.GetCenter(GS.grids[GS.focused_grid]).ToWorldPos();
            for (int i = 0; i < 5000; i++)
            {
                float displacement = MathZ.rand.NextSingle();
                displacement = MathF.Sqrt(displacement) * 10f;
                Vector2 placement_pos = center_of_grid + MathZ.RandomDirV() * displacement;
                BeeEntity.CreateBee(ecs, placement_pos, 0);
            }
        }
        
        ecs.GetSystem<BeeThinkSystem>().Update();
        ecs.GetSystem<BeeMoveSystem>().Update();
        ecs.GetSystem<CollisionSystem>().Update();

        CameraLogic.Update();
    }
}