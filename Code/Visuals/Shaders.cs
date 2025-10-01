using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class Shaders
{
    public static Effect HexTileShader;
    public static Effect BuildingPrePlacementShader;
    public static Effect BuildingBlueprintShader;

    public static void LoadContent()
    {
        ContentManager cm = Visuals.cm;
        GraphicsDevice gd = Visuals.gd;

        HexTileShader = cm.Load<Effect>("HexTileShader");
        BuildingPrePlacementShader = cm.Load<Effect>("BuildingPrePlacementShader");
        BuildingBlueprintShader = cm.Load<Effect>("BuildingBlueprintShader");
    }
}