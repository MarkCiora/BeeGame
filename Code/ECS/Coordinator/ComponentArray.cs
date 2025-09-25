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

public interface IComponentArray
{
    void EntityDestroyed(int entity);
}

public class ComponentArray<T> : IComponentArray where T : struct
{
    private const int MAX_ENTITIES = 20000;

    private readonly T[] _componentArray;
    private readonly Dictionary<int, int> _entityToIndexMap;
    private readonly Dictionary<int, int> _indexToEntityMap;
    private int _size;

    public ComponentArray()
    {
        _componentArray = new T[MAX_ENTITIES];
        _entityToIndexMap = new Dictionary<int, int>();
        _indexToEntityMap = new Dictionary<int, int>();
        _size = 0;
    }

    public void InsertData(int entity, T component)
    {
        Debug.Assert(!_entityToIndexMap.ContainsKey(entity),
            "Component added to same entity more than once.");

        int newIndex = _size;
        _entityToIndexMap[entity] = newIndex;
        _indexToEntityMap[newIndex] = entity;
        _componentArray[newIndex] = component;
        _size++;
    }

    public void RemoveData(int entity)
    {
        Debug.Assert(_entityToIndexMap.ContainsKey(entity),
            "Removing non-existent component.");

        int indexOfRemovedEntity = _entityToIndexMap[entity];
        int indexOfLastElement = _size - 1;

        // Move last element into removed slot
        _componentArray[indexOfRemovedEntity] = _componentArray[indexOfLastElement];

        int entityOfLastElement = _indexToEntityMap[indexOfLastElement];
        _entityToIndexMap[entityOfLastElement] = indexOfRemovedEntity;
        _indexToEntityMap[indexOfRemovedEntity] = entityOfLastElement;

        _entityToIndexMap.Remove(entity);
        _indexToEntityMap.Remove(indexOfLastElement);

        _size--;
    }

    public ref T GetData(int entity)
    {
        Debug.Assert(_entityToIndexMap.ContainsKey(entity),
            "Retrieving non-existent component.");

        return ref _componentArray[_entityToIndexMap[entity]];
    }

    public void EntityDestroyed(int entity)
    {
        if (_entityToIndexMap.ContainsKey(entity))
        {
            RemoveData(entity);
        }
    }

    public ref T this[int entity]
    {
        get => ref GetData(entity);
    }
}
