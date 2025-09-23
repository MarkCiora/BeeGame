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

public class SystemManager
{
    private readonly Dictionary<Type, Signature> _signatures = new();
    private readonly Dictionary<Type, ECSSystem> _systems = new();

    // Register a system and return it
    public T RegisterSystem<T>() where T : ECSSystem, new()
    {
        Type type = typeof(T);
        Debug.Assert(!_systems.ContainsKey(type), $"Registering system {type} more than once.");

        var system = new T();
        _systems[type] = system;
        return system;
    }

    // Set signature for a system
    public void SetSignature<T>(Signature signature) where T : ECSSystem
    {
        Type type = typeof(T);
        Debug.Assert(_systems.ContainsKey(type), $"System {type} used before registered.");

        _signatures[type] = signature;
    }

    // Notify all systems that an entity was destroyed
    public void EntityDestroyed(int entity)
    {
        foreach (var system in _systems.Values)
        {
            system.m_entities.Remove(entity);
        }
    }

    // Notify systems that an entity's signature changed
    public void EntitySignatureChanged(int entity, Signature entitySignature)
    {
        foreach (var kvp in _systems)
        {
            var type = kvp.Key;
            var system = kvp.Value;

            if (!_signatures.TryGetValue(type, out var systemSignature))
                continue;

            if ((entitySignature.bits & systemSignature.bits) == systemSignature.bits)
                system.m_entities.Add(entity);
            else
                system.m_entities.Remove(entity);
        }
    }
}
