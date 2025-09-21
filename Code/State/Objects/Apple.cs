using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public class Apple : GameObj
{
    public static Dictionary<long, Apple> apple_dict = new Dictionary<long, Apple>();
    public static HashSet<Apple> available_apples = new HashSet<Apple>();
    private static readonly object available_apples_locker = new();

    public Apple(Vector2 pos, float rot = MathF.PI * 0.5f)
    {
        apple_dict[id] = this;
        available_apples.Add(this);
        this.pos = pos;
        this.rot = rot;
    }

    public override Texture2D GetTexture()
    {
        return Textures.apple1;
    }

    public override void Delete()
    {
        apple_dict.Remove(id);
        lock (available_apples_locker)
        {
            available_apples.Remove(this);
        }
    }

    public void Release()
    {
        lock (available_apples_locker)
        {
            available_apples.Add(this);
        }
    }

    public static Apple RequestReserve()
    {
        lock (available_apples_locker)
        {
            if (available_apples.Count != 0)
            {
                Apple apple = available_apples.First();
                available_apples.Remove(apple);
                return apple;
            }
        }
        return null;
    }

    public static Apple RequestReserveClosest(Vector2 vec)
    {
        lock (available_apples_locker)
        {
            if (available_apples.Count == 0)
                return null;

            Apple closest = null;
            float closestDistSq = float.MaxValue;
            int checkedCount = 0;

            // pick up to 10 random elements
            var enumerator = available_apples.GetEnumerator();
            while (checkedCount < 100    && enumerator.MoveNext())
            {
                var candidate = enumerator.Current;

                // simulate random selection
                float distSq = Vector2.DistanceSquared(candidate.pos, vec);
                if (distSq < closestDistSq)
                {
                    closest = candidate;
                    closestDistSq = distSq;
                }

                checkedCount++;
            }

            if (closest != null)
            {
                available_apples.Remove(closest);
                return closest;
            }

            // fallback in case random sampling didn't pick anything
            closest = available_apples.First();
            available_apples.Remove(closest);
            return closest;
        }
    }

}