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
/// A 3D point used for representing hex coordinates.
/// q+r+s=0 must always be satisfied.
/// q is oriented to the right.
/// r is oriented down-left.
/// s is oriented up-left.
/// </summary>
public class HexPoint
{
    public int q { get; }
    public int r { get; }
    // public int s { get; }

    // public static HexPoint[] adj =
    // {
    //     new HexPoint(1,-1), new HexPoint(1,0), new HexPoint(0,1),
    //     new HexPoint(-1,1), new HexPoint(-1,0), new HexPoint(0,-1)
    // };

    public HexPoint[] GetNeighbors()
    {
        return new HexPoint[]
        {
            this + new HexPoint(1,-1),
            this + new HexPoint(1,0),
            this + new HexPoint(0,1),
            this + new HexPoint(-1,1),
            this + new HexPoint(-1,0),
            this + new HexPoint(0,-1)
        };
    }

    public HexPoint[] GetThisAndNeighbors()
    {
        return new HexPoint[]
        {
            this,
            this + new HexPoint(1,-1),
            this + new HexPoint(1,0),
            this + new HexPoint(0,1),
            this + new HexPoint(-1,1),
            this + new HexPoint(-1,0),
            this + new HexPoint(0,-1)
        };
    }

    public HexPoint[] GetRing(int r)
    {
        if (r < 0)
            throw new ArgumentException("Radius must be >0");

        if (r == 0)
            return [this];

        HexPoint[] directions =
        [
            new HexPoint(1, -1),
            new HexPoint(1, 0),
            new HexPoint(0, 1),
            new HexPoint(-1, 1),
            new HexPoint(-1, 0),
            new HexPoint(0, -1)
        ];

        HexPoint hex = this + directions[4] * r;

        HexPoint[] ring = new HexPoint[r == 0 ? 1 : 6 * r];

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < r; j++)
            {
                ring[i * r + j] = hex;
                hex += directions[i];
            }
        }

        return ring;
    }

    public HexPoint[] ToRingSorted(int radius, Vector2 target)
    {
        if (radius < 0)
            throw new ArgumentException("Radius must be >= 0");

        // Preallocate array for the ring
        HexPoint[] ring = new HexPoint[radius == 0 ? 1 : 6 * radius];

        if (radius == 0)
        {
            ring[0] = this;
        }
        else
        {
            HexPoint[] directions =
            {
                new HexPoint(1, -1),
                new HexPoint(1, 0),
                new HexPoint(0, 1),
                new HexPoint(-1, 1),
                new HexPoint(-1, 0),
                new HexPoint(0, -1)
            };

            HexPoint hex = this + directions[4] * radius;
            int index = 0;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < radius; j++)
                {
                    ring[index++] = hex;
                    hex += directions[i];
                }
            }
        }

        // Sort by distance to target in world coordinates
        Array.Sort(ring, (a, b) =>
        {
            float da = Vector2.DistanceSquared(a.ToWorldPos(), target);
            float db = Vector2.DistanceSquared(b.ToWorldPos(), target);
            return da.CompareTo(db);
        });

        return ring;
    }

    /// <summary>
    /// Axial coordinates q and s. Cubic coordinate s = -q - r
    /// </summary>
    /// <param name="q_"></param>
    /// <param name="r_"></param>
    public HexPoint(int q_, int r_)
    {
        q = q_;
        r = r_;
        // s = -q - r;
    }

    public HexPoint(Vector2 vec)
    {
        float qf = vec.X * 2 / 3;
        float rf = -vec.Y * MathF.Sqrt(3) / 3 - vec.X * 1 / 3;
        float sf = -qf - rf;
        q = (int)Math.Round(qf);
        r = (int)Math.Round(rf);
        int s = (int)Math.Round(sf);
        float dq = Math.Abs(qf - q);
        float dr = Math.Abs(rf - r);
        float ds = Math.Abs(sf - s);
        if (dq > dr && dq > ds)
            q = -r - s;
        else if (dr > ds)
            r = -q - s;
        // else
        //     s = -q - r;
    }

    public float EuclidDistance(HexPoint target)
    {
        return (this - target).Length();
    }

    public int Length()
    {
        return (Math.Abs(q) + Math.Abs(r) + Math.Abs(-q - r)) / 2;
    }

    public Point3 ToPoint3()
    {
        return new Point3(q, r, -q - r);
    }

    public Vector2 ToWorldPos()
    {
        float x = q * 1.5f;
        float y = -MathF.Sqrt(3) * (q * 0.5f + r);
        return new Vector2(x, y);
    }

    public HexPoint RotatedBy(int orientation)
    {
        orientation = (orientation + 600) % 6;
        switch (orientation)
        {
            case 0:
                return new HexPoint(q, r);
            case 1:
                return new HexPoint(-r, q + r);
            case 2:
                return new HexPoint(-q - r, q);
            case 3:
                return new HexPoint(-q, -r);
            case 4:
                return new HexPoint(r, -q - r);
            case 5:
                return new HexPoint(q + r, -q);
            default:
                return new HexPoint(q, r);
        }
    }
    
    public static HexPoint operator +(HexPoint a, HexPoint b)
    {
        return new HexPoint(a.q + b.q, a.r + b.r);
    }

    public static HexPoint operator -(HexPoint a, HexPoint b)
    {
        return new HexPoint(a.q - b.q, a.r - b.r);
    }

    public static HexPoint operator *(HexPoint a, int k)
    {
        return new HexPoint(a.q * k, a.r * k);
    }

    public static bool operator ==(HexPoint a, HexPoint b)
    {
        return a.q == b.q && a.r == b.r;
    }

    public static bool operator !=(HexPoint a, HexPoint b)
    {
        return a.q != b.q || a.r != b.r;
    }

    public override bool Equals(object obj)
    {
        if (obj is HexPoint other)
            return this == other;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(q, r);
    }

    public override string ToString() => $"({q},{r},{-q - r})";
}