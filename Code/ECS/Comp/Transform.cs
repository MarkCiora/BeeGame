using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public struct Transform
{
    public Vector2 pos;
    public Vector2 push;
    public int grid_level;
    public bool can_move;
    public float speed;
    public float scale;
    public float rot;
}