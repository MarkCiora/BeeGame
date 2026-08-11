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

struct TaskActor
{
    public int target;
    public TaskStatus status;
}

struct TaskTarget
{
    public int actor;
    public TaskType type;
}

public enum TaskType
{
    None, Collect, Deposit
}

public enum TaskStatus
{
    Inactive, Active, Complete, Failed
}

// struct TaskTargetComp
// {
//     public int task;
// }
