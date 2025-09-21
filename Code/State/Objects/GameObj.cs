using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public abstract class GameObj
{
    public static long next_id;
    public static Dictionary<long, GameObj> obj_dict = new Dictionary<long, GameObj>();
    public static List<GameObj> delete_list = new List<GameObj>();

    public long id;
    public bool deleted;

    public Vector2 pos;
    public float rot;
    public float scale;
    public Color tint;
    public bool asleep;

    public abstract void Delete();
    public abstract Texture2D GetTexture();

    public GameObj(bool asleep = false)
    {
        id = next_id;
        obj_dict[id] = this;
        next_id++;
        this.asleep = asleep;
        pos = Vector2.Zero;
        rot = 0f;
        scale = 1f;
        tint = Color.White;
    }

    public override bool Equals(object obj)
    {
        return obj is GameObj p && p.id == id;
    }

    public override int GetHashCode()
    {
        return (int)id;
    }

    public void FlagDelete()
    {
        deleted = true;
        delete_list.Add(this);
    }

    public static void FlushDeleteList()
    {
        foreach (GameObj obj in delete_list)
        {
            obj_dict.Remove(obj.id);
            obj.Delete();
        }
        delete_list.Clear();
    }

}