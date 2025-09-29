using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class Screen
{
    public static GraphicsDeviceManager gdm;

    public static void Init(GraphicsDeviceManager gdm)
    {
        Screen.gdm = gdm;
        SetFullscreen(false);
    }

    public static int GetWidth()
    {
        return gdm.PreferredBackBufferWidth;
    }

    public static int GetHeight()
    {
        return gdm.PreferredBackBufferHeight;
    }

    public static void SetScreenSize(int width, int height)
    {
        gdm.PreferredBackBufferWidth = width;
        gdm.PreferredBackBufferHeight = height;
        gdm.ApplyChanges();
    }

    public static void SetFullscreen(bool full)
    {
        if (full)
        {
            gdm.IsFullScreen = true;
            SetScreenSize(1920, 1080);
        }
        else
        {
            gdm.IsFullScreen = false;
            SetScreenSize(960, 540);
        }
        gdm.ApplyChanges();
    }

    public static void ToggleFullscreen()
    {
        SetFullscreen(!gdm.IsFullScreen);
    }

}