using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public class EntityManager
{
    private const int MAX_ENTITIES = 25000; // example, tune to your needs
    private readonly Queue<int> _availableEntities = new();
    private readonly Signature[] _signatures = new Signature[MAX_ENTITIES];
    private int _livingEntityCount;

    public EntityManager()
    {
        // Fill queue with all possible entity IDs
        for (int entity = 0; entity < MAX_ENTITIES; entity++)
        {
            _availableEntities.Enqueue(entity);
        }
    }

    public int CreateEntity()
    {
        if (_livingEntityCount >= MAX_ENTITIES)
            throw new InvalidOperationException("Too many entities in existence.");

        int id = _availableEntities.Dequeue();
        _livingEntityCount++;
        return id;
    }

    public void DestroyEntity(int entity)
    {
        if (entity >= MAX_ENTITIES)
            throw new ArgumentOutOfRangeException(nameof(entity), "Entity out of range.");

        // Invalidate signature
        _signatures[entity] = new Signature();

        // Return ID to pool
        _availableEntities.Enqueue(entity);
        _livingEntityCount--;
    }

    public void SetSignature(int entity, Signature signature)
    {
        if (entity >= MAX_ENTITIES)
            throw new ArgumentOutOfRangeException(nameof(entity), "Entity out of range.");

        _signatures[entity] = signature;
    }

    public Signature GetSignature(int entity)
    {
        if (entity >= MAX_ENTITIES)
            throw new ArgumentOutOfRangeException(nameof(entity), "Entity out of range.");

        return _signatures[entity];
    }
}
