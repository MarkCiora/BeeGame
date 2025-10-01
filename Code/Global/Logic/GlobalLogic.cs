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
        GameplayUI.Init();
        BuildingToolsLogic.Init();


        // new global main ecs
        GS.main_ecs = new();
        var ecs = GS.main_ecs;


        // Register components
        ecs.RegisterComponent<Transform>(20000);
        ecs.RegisterComponent<Sprite>(20000);
        ecs.RegisterComponent<TileOccupier>(5000);
        ecs.RegisterComponent<MovementDescriptor>(10000);
        ecs.RegisterComponent<CircleCollider>(10000);


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
        
        //Collision visualizer system
        signature = new();
        ecs.RegisterSystem<ColliderVisualizerSystem>();
        signature.Set(ecs.GetComponentType<CircleCollider>(), true);
        signature.Set(ecs.GetComponentType<Transform>(), true);
        ecs.SetSystemSignature<ColliderVisualizerSystem>(signature);
        
        //Bee move system
        signature = new();
        ecs.RegisterSystem<BuildingSystem>();
        signature.Set(ecs.GetComponentType<Transform>(), true);
        signature.Set(ecs.GetComponentType<TileOccupier>(), true);
        ecs.SetSystemSignature<BuildingSystem>(signature);


        // Startup entity creation
        Vector2 center_of_grid = HexGridLogic.GetCenter(GS.grids[GS.focused_grid]).ToWorldPos();
        for (int i = 0; i < 5; i++)
        {
            float displacement = MathZ.rand.NextSingle();
            displacement = MathF.Sqrt(displacement) * 1;
            Vector2 placement_pos = center_of_grid + MathZ.RandomDirV() * displacement;
            BeeEntity.CreateBee(ecs, placement_pos, 0);
        }
        // HoneyCombEntity.CreateHoneyComb(ecs, new HexPoint(15, 15), 0);
    }

    public static void Update()
    {
        var ecs = GS.main_ecs;

        if (Input.IsPressed(Keys.F11))
        {
            Screen.ToggleFullscreen();
        }

        if (Input.IsPressed(Keys.Space))
        {
            Vector2 center_of_grid = HexGridLogic.GetCenter(GS.grids[GS.focused_grid]).ToWorldPos();
            for (int i = 0; i < 500; i++)
            {
                float displacement = MathZ.rand.NextSingle();
                displacement = MathF.Sqrt(displacement) * 2f;
                Vector2 placement_pos = center_of_grid + MathZ.RandomDirV() * displacement;
                BeeEntity.CreateBee(ecs, placement_pos, 0);
            }
        }

        BuildingToolsLogic.Update();
        
        ecs.GetSystem<BeeThinkSystem>().Update();
        ecs.GetSystem<BeeMoveSystem>().Update();
        ecs.GetSystem<CollisionSystem>().Update();

        CameraLogic.Update();
    }
}