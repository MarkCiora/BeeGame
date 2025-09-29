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

public class BuildingSystem : ECSSystem
{
    public void Update()
    {

    }

    public int TryCreateBuilding(int grid, int x, int y, int orientation, TileShape shape)
    {
        HexGrid hex_grid = GS.grids[grid];
        HexPoint origin = new HexPoint(x, y);

        // pre check if tiles are free
        foreach (var (dq, dr) in TileOccupier.footprints[shape])
        {
            HexPoint point = new(dq, dr);
            point = origin + point.RotatedBy(orientation);
            HexTile tile = hex_grid.tiles[point.q, point.r];
            if (tile.built || tile.elevation != 0) return -1;
        }

        // make the entity
        int entity = ecs.CreateEntity();
        TileOccupier tile_occupier = new();
        tile_occupier.grid = grid;
        tile_occupier.x = x;
        tile_occupier.y = y;
        tile_occupier.orientation = orientation;
        tile_occupier.shape = shape;
        ecs.AddComponent<TileOccupier>(entity, tile_occupier);

        // assign tiles to building
        foreach (var (dq, dr) in TileOccupier.footprints[shape])
        {
            HexPoint point = new(dq, dr);
            point = origin + point.RotatedBy(orientation);
            ref HexTile tile = ref hex_grid.tiles[point.q, point.r];
            tile.collision = true;
            tile.built = true;
            tile.occupied_entity = entity;
        }

        return entity;
    }

    public bool TryDeleteBuilding(int grid, int x, int y)
    {
        HexGrid hex_grid = GS.grids[grid];

        // pre check if there is a building
        if (!hex_grid.tiles[x, y].built) return false;

        // retrieve entity and unassign all tiles
        int entity = hex_grid.tiles[x, y].occupied_entity;
        TileOccupier tile_occupier = ecs.GetComponent<TileOccupier>(entity);
        HexPoint origin = new HexPoint(tile_occupier.x, tile_occupier.y);
        int orientation = tile_occupier.orientation;
        TileShape shape = tile_occupier.shape;
        foreach (var (dq, dr) in TileOccupier.footprints[shape])
        {
            HexPoint point = new(dq, dr);
            point = origin + point.RotatedBy(orientation);
            ref HexTile tile = ref hex_grid.tiles[point.q, point.r];
            tile.built = false;
            if (tile.elevation == 0)
            {
                tile.collision = false;
            }
        }

        // delete entity
        ecs.DestroyEntity(entity);

        return true;
    }

    public bool HasBuilding(int grid, int x, int y)
    {
        return GS.grids[grid].tiles[x, y].built;
    }

    public int SelectBuildingEntity(int grid, int x, int y)
    {
        if (HasBuilding(grid, x, y))
        {
            Debug.WriteLine($"Tried to select {grid}:[{x}, {y}], but no building");
        }
        return GS.grids[grid].tiles[x, y].occupied_entity;
    }
}