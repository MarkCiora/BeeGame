using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class GS
{
    public const float default_tick_period = 0.5f;
    public static float tick_period;
    public static float time_since_tick;
    public static bool paused;

    public static Camera main_camera;
    public const float default_camera_speed = 4f;
    public static float camera_speed;

    public static HexGrid grid;
}