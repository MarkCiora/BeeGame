using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace BeeGame;

public static class SpriteVisualizer
{
    public static void Visualize()
    {
        Visuals.sb.Begin();
        Visuals.sb.End();
    }
}