using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public static class HexGridLogic
{
    public static void SetMain(int index)
    {
        if (index < GS.grids.Count)
        {
            GS.focused_grid = index;
        }
    }

    public static int CreateBlank(int diameter)
    {
        HexGrid grid = new();
        grid.diameter = diameter;
        grid.tiles = new HexTile[diameter, diameter];
        for (int i = 0; i < diameter; i++)
        {
            for (int j = 0; j < diameter; j++)
            {
                HexPoint hex_pos = new(i, j);
                grid.tiles[i, j].type = 0;
                grid.tiles[i, j].elevation = 0;
                if (i == 0 || j == 0 || i == diameter - 1 || j == diameter - 1)
                {
                    grid.tiles[i, j].type = 1;
                }
            }
        }
        GS.grids.Add(grid);

        Vector2 center = new HexPoint(diameter / 2, diameter / 2).ToWorldPos();
        GS.main_cameras.Add(new Camera(center));

        return GS.grids.Count - 1;
    }
}