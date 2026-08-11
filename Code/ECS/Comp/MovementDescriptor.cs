using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public struct MovementDescriptor
{
    public Vector2 move_target;
    public bool move_intent;
    public bool can_fly;
    public bool flying;
}

// public struct Transform
// {
//     public Vector2 pos;
//     public Vector2 vel;
//     public float scale;
//     public float rot;
//     public int grid_level;
// }
