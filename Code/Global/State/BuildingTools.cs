using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace BeeGame;

/// <summary>
/// State for building selection, hovering
/// </summary>
public static class BuildingTools
{
    public static bool build_mode = false;
    public static BuildingType selected_type;
    public static Vector2 hover_offset;
    public static HexPoint selected_hex;
    public static int orientation;
}
