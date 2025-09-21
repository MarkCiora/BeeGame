using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class GameLogic
{
    public static void Init()
    {
        GS.tick_period = GS.default_tick_period;
        GS.time_since_tick = 0f;
        GS.paused = false;
        GS.main_camera = new Camera(Vector2.Zero);
        GS.camera_speed = GS.default_camera_speed;

        GS.grid = new HexGrid(50);
    }

    public static void Update()
    {
        GS.time_since_tick += Time.dt;

        if (Input.IsPressed(Keys.F11))
        {
            Screen.SetFullscreen(true);
        }

        if (Input.IsPressed(MouseButton.Left))
        {
            Bee bee = new Bee(GS.main_camera.MousePosInWorld());
        }
        if (Input.IsPressed(MouseButton.Right))
        {
            Apple bee = new Apple(GS.main_camera.MousePosInWorld());
        }

        Bee.UpdateAll();

        GameObj.FlushDeleteList();

        CameraControl.Update();
    }

}