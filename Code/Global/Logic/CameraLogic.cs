using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class CameraLogic
{
    public static void Update()
    {
        Camera camera = GS.main_cameras[GS.focused_grid];

        if (Input.IsScrollUp())
        {
            camera.ZoomIn();
        }
        if (Input.IsScrollDown())
        {
            camera.ZoomOut();
        }
        if (Input.IsHeld(Keys.W))
        {
            camera.pos += new Vector2(0f, 1f) * Camera.camera_speed * Time.dt;
        }
        if (Input.IsHeld(Keys.A))
        {
            camera.pos += new Vector2(-1f, 0f) * Camera.camera_speed * Time.dt;
        }
        if (Input.IsHeld(Keys.S))
        {
            camera.pos += new Vector2(0f, -1f) * Camera.camera_speed * Time.dt;
        }
        if (Input.IsHeld(Keys.D))
        {
            camera.pos += new Vector2(1f, 0f) * Camera.camera_speed * Time.dt;
        }
    }
}
