using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class MathZ
{
    public static Random rand;

    public static void Init()
    {
        rand = new Random();
    }

    public static Vector2 DirFromRot(float rot)
    {
        return new Vector2(MathF.Cos(rot), MathF.Sin(rot));
    }

    public static float Atan2(Vector2 v)
    {
        return MathF.Atan2(v.Y, v.X);
    }

    public static float Atan2Degrees(Vector2 v)
    {
        return MathF.Atan2(v.Y, v.X) * (180f / MathF.PI);
    }

    public static float SignedAngleBetween(Vector2 from, Vector2 to)
    {
        float dot = Vector2.Dot(from, to);             // Cosine of angle
        float det = from.X * to.Y - from.Y * to.X;     // 2D "cross product" (scalar)
        return MathF.Atan2(det, dot);                  // Signed angle in radians
    }

    public static float Clamp(float value, float min, float max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }

    public static Vector2 RotateToward(Vector2 from, Vector2 to, float maxRotation)
    {
        // Get signed angle between vectors
        float angle = SignedAngleBetween(from, to);

        // Clamp the rotation angle
        float clamped = Clamp(angle, -maxRotation, maxRotation);

        // Rotate `from` by the clamped angle
        float cos = MathF.Cos(clamped);
        float sin = MathF.Sin(clamped);

        return new Vector2(
            from.X * cos - from.Y * sin,
            from.X * sin + from.Y * cos
        );
    }
    public static Vector2 OrthogonalCCW(Vector2 v)
    {
        return new Vector2(-v.Y, v.X);
    }

    public static Vector2 P2V(Point point)
    {
        return new Vector2(point.X, point.Y);
    }

    public static Point V2P(Vector2 vec)
    {
        return new Point((int)MathF.Round(vec.X), (int)MathF.Round(vec.Y));
    }

    public static Point RandomDirP()
    {
        int choice = rand.Next(4);
        if (choice == 0) return new Point(1, 0);
        if (choice == 1) return new Point(-1, 0);
        if (choice == 2) return new Point(0, 1);
        else return new Point(0, -1);
    }

    public static Vector2 RandomDirV()
    {
        int choice = rand.Next(4);
        if (choice == 0) return new Vector2(1, 0);
        if (choice == 1) return new Vector2(-1, 0);
        if (choice == 2) return new Vector2(0, 1);
        else return new Vector2(0, -1);
    }

    public static int RandomInt()
    {
        return rand.Next();
    }

    public static bool IsAdjacent(Point a, Point b)
    {
        int dx = Math.Abs(a.X - b.X);
        int dy = Math.Abs(a.Y - b.Y);
        return (dx + dy) == 1;  // exactly one step away
    }

}