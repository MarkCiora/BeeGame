using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class CameraControl
{
    public static void Update()
    {
        if (Input.IsScrollUp())
        {
            GS.main_camera.ZoomIn();
        }
        if (Input.IsScrollDown())
        {
            GS.main_camera.ZoomOut();
        }
        if (Input.IsHeld(Keys.W))
        {
            GS.main_camera.pos += new Vector2(0f, 1f) * GS.camera_speed * Time.dt;
        }
        if (Input.IsHeld(Keys.A))
        {
            GS.main_camera.pos += new Vector2(-1f, 0f) * GS.camera_speed * Time.dt;
        }
        if (Input.IsHeld(Keys.S))
        {
            GS.main_camera.pos += new Vector2(0f, -1f) * GS.camera_speed * Time.dt;
        }
        if (Input.IsHeld(Keys.D))
        {
            GS.main_camera.pos += new Vector2(1f, 0f) * GS.camera_speed * Time.dt;
        }
    }
}