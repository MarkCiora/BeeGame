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

/// <summary>
/// Logic for building selection, hovering
/// </summary>
public static class BuildingToolsLogic
{
    public static void Init()
    {
        AttachLeftClickToRouter();
        AttachRightClickToRouter();
    }

    public static void AttachLeftClickToRouter()
    {
        InputRouter.RegisterMouseHandler(MouseButton.Left, (e) =>
        {
            if (BuildingTools.build_mode)
            {
                TryBuild();
                return true;
            }
            else
            {
                return false;
            }
        }, priority: 100);
    }

    public static void AttachRightClickToRouter()
    {
        InputRouter.RegisterMouseHandler(MouseButton.Right, (e) =>
        {
            if (BuildingTools.build_mode)
            {
                BuildingTools.build_mode = false;
                return true;
            }
            else
            {
                return false;
            }
        }, priority: 100);
    }

    public static void ClickBuildingMenuHandler(BuildingType type)
    {
        // if selecting the same building as type, turn off build mode
        if (BuildingTools.build_mode && type == BuildingTools.selected_type)
        {
            BuildingTools.build_mode = false;
        }
        else
        {
            BuildingTools.build_mode = true;
            BuildingTools.selected_type = type;
            TileShape shape = TileOccupier.building_to_shape[type];
            BuildingTools.hover_offset = TileOccupier.footprint_offsets[shape];
        }
    }

    public static void Update()
    {
        if (!BuildingTools.build_mode) return;

        Vector2 mouse_pos = GS.main_cameras[GS.focused_grid].MousePosInWorld();
        Vector2 aligned_offset = Vector2.Rotate(BuildingTools.hover_offset, MathF.PI / 3 * BuildingTools.orientation);
        BuildingTools.selected_hex = new(mouse_pos - aligned_offset);
    }

    public static bool CheckCanBuild()
    {
        if (!BuildingTools.build_mode) return false;

        // check each tile this building should own
        HexGrid grid = GS.grids[GS.focused_grid];
        BuildingType type = BuildingTools.selected_type;
        TileShape shape = TileOccupier.building_to_shape[type];
        foreach (var (dq, dr) in TileOccupier.footprints[shape])
        {
            HexPoint hex = BuildingTools.selected_hex + new HexPoint(dq, dr);
            HexTile tile = grid.tiles[hex.q, hex.r];
            if (tile.built || tile.elevation != 0) return false;
        }

        return true;
    }

    public static bool TryBuild()
    {
        // update build tool state (selected hex in case mouse moved)
        Update();
        if (!CheckCanBuild())
        {
            Console.WriteLine("Cant build there");
            return false;
        }

        TileOccupier tile_occupier = new();
        tile_occupier.grid = GS.focused_grid;
        tile_occupier.x = BuildingTools.selected_hex.q;
        tile_occupier.y = BuildingTools.selected_hex.r;
        tile_occupier.orientation = BuildingTools.orientation;
        tile_occupier.shape = TileOccupier.building_to_shape[BuildingTools.selected_type];
        tile_occupier.type = BuildingTools.selected_type;
        int cond = GS.main_ecs.GetSystem<BuildingSystem>().TryCreateBuilding(tile_occupier);
        Console.WriteLine(cond);

        return true;
    }
}
