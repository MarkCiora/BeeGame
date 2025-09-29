using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;

namespace BeeGame;


public static class GS
{
    public static bool game_logic_ready = false;
    public static bool game_running = false;

    public static Desktop desktop;

    public static ECSCoordinator main_ecs;

    public static int focused_grid = 0;
    public static List<HexGrid> grids = new();
    public static List<Camera> main_cameras = new();
}