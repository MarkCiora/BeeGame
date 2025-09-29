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


public class ComponentManager
{
    private readonly Dictionary<Type, int> _componentTypes = new();
    private readonly Dictionary<Type, IComponentArray> _componentArrays = new();
    private int _nextComponentType;

    // Register a component type T
    public void RegisterComponent<T>(int n) where T : struct
    {
        Type type = typeof(T);
        Debug.Assert(!_componentTypes.ContainsKey(type), $"Registering component type {type} more than once.");

        _componentTypes[type] = _nextComponentType;
        _componentArrays[type] = new ComponentArray<T>(n);

        _nextComponentType++;
    }

    // Get the component type of T (used for signatures)
    public int GetComponentType<T>()
    {
        Type type = typeof(T);
        Debug.Assert(_componentTypes.ContainsKey(type), $"Component type {type} not registered before use.");
        return _componentTypes[type];
    }

    // Add a component to an entity
    public void AddComponent<T>(int entity, T component) where T : struct
    {
        GetComponentArray<T>().InsertData(entity, component);
    }

    // Remove a component from an entity
    public void RemoveComponent<T>(int entity) where T : struct
    {
        GetComponentArray<T>().RemoveData(entity);
    }

    // Get a reference to a component of an entity
    public ref T GetComponent<T>(int entity) where T : struct
    {
        return ref GetComponentArray<T>().GetData(entity);
    }

    // Notify all component arrays that an entity was destroyed
    public void EntityDestroyed(int entity)
    {
        foreach (var array in _componentArrays.Values)
        {
            array.EntityDestroyed(entity);
        }
    }

    // Internal helper to get the ComponentArray<T> casted properly
    private ComponentArray<T> GetComponentArray<T>() where T : struct
    {
        Type type = typeof(T);
        Debug.Assert(_componentTypes.ContainsKey(type), $"Component type {type} not registered before use.");

        return (ComponentArray<T>)_componentArrays[type];
    }
}