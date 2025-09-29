using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.TextureAtlases;  // TextureRegion
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.Brushes;

namespace BeeGame;

public static class GameplayUI
{
    public static void Init()
    {
        AttachMyraToRouter(GS.desktop);
        BuildUI.CreateBasicBuildUI(GS.desktop);
    }

    public static void AttachMyraToRouter(Desktop desktop)
    {
        InputRouter.RegisterMouseHandler(MouseButton.Left, (InputEvent e) =>
        {
            return desktop.IsMouseOverGUI;
        }, priority: 1000); // very high priority
    }
}