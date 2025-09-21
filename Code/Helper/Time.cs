using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class Time
{
    public static GameTime game_time;
    public static float dt;

    public static void Update(GameTime time)
    {
        game_time = time;
        dt = (float)game_time.ElapsedGameTime.TotalSeconds;
    }

}