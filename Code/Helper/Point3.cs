using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

/// <summary>
/// A 3D point
/// </summary>
public struct Point3
{
    public int X { get; set;  }
    public int Y { get; set;  }
    public int Z { get; set;  }

    public Point3(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    // Addition
    public static Point3 operator +(Point3 a, Point3 b)
    {
        return new Point3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    // Subtraction
    public static Point3 operator -(Point3 a, Point3 b)
    {
        return new Point3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    // Equality
    public static bool operator ==(Point3 a, Point3 b)
    {
        return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
    }

    // Inequality
    public static bool operator !=(Point3 a, Point3 b)
    {
        return !(a == b);
    }

    // For == and != to work properly, also override Equals and GetHashCode
    public override bool Equals(object obj)
    {
        if (obj is Point3 other)
            return this == other;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public override string ToString() => $"({X},{Y},{Z})";
}
