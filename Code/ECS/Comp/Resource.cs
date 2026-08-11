using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public struct ResourceDeposit
{
    public ResourceType type;
}

public struct Resource
{
    public ResourceType type;
    public int amount;
}

public struct Inventory
{
    public ResourceType type;
    public int amount;
    public int capacity;
}

public enum ResourceType
{
    None, Meat, Fruit, Dirt, Stone, Honey
}
