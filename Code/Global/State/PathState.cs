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

public static class PathState
{
    public const int MAX_PATH_LOOKUP_SIZE = 100000;
    public static int path_index = 0;
    public static Vector2[] path_lookup = new Vector2[MAX_PATH_LOOKUP_SIZE];
}