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
        int main_grid_index = HexGridLogic.CreateBlank(6);
        HexGridLogic.SetMain(main_grid_index);

        GS.main_ecs = new();
    }

    public static void Update()
    {
        CameraLogic.Update();

        // ECSLogic.Update();
    }
}