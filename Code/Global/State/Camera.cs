using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public class Camera
{
    public Vector2 pos;
    public float zoom; //pixels per unit in world space
    public float rot; //DOESNT WORK YET
    public bool using_preset;
    public int selected_zoom;

    public static float camera_speed = 4f;
    public static readonly float[] zoom_presets =
        {20f, 30f, 50f, 75f, 100f, 150f, 200f, 250f };

    public Camera(Vector2 p, float r = 0)
    {
        pos = p;
        rot = r;
        using_preset = true;
        selected_zoom = 4;
        zoom = zoom_presets[selected_zoom];
    }

    public void ZoomOut()
    {
        if (selected_zoom != 0)
        {
            selected_zoom -= 1;
            zoom = zoom_presets[selected_zoom];
        }
    }

    public void ZoomIn()
    {
        if (selected_zoom != zoom_presets.Length - 1)
        {
            selected_zoom += 1;
            zoom = zoom_presets[selected_zoom];
        }
    }

    public Vector2 WorldToScreen(Vector2 vec)
    {
        vec -= pos;
        vec *= zoom;
        vec.Y = -vec.Y;
        vec.X += (float)Screen.GetWidth() / 2;
        vec.Y += (float)Screen.GetHeight() / 2;
        return vec;
    }

    public Vector2 ScreenToWorld(Vector2 vec)
    {
        vec.X -= (float)Screen.GetWidth() / 2;
        vec.Y -= (float)Screen.GetHeight() / 2;
        vec.Y = -vec.Y;
        vec /= zoom;
        vec += pos;
        return vec;
    }

    public Vector2 ScreenToWorld(Point point)
    {
        point.X -= Screen.GetWidth() / 2;
        point.Y -= Screen.GetHeight() / 2;
        point.Y = -point.Y;
        Vector2 vec = new Vector2((float)point.X, (float)point.Y);
        vec /= zoom;
        vec += pos;
        return vec;
    }

    public Vector2 ScreenMinWorldSpace()
    {
        return ScreenToWorld(new Point(0, Screen.GetHeight()));
    }

    public Vector2 ScreenMaxWorldSpace()
    {
        return ScreenToWorld(new Point(Screen.GetWidth(), 0));
    }

    public Vector2 MousePosInWorld()
    {
        return ScreenToWorld(Input.MousePosP());
    }
}