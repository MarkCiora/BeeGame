using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeGame;

public class ECSCoordinator
{
    public EntityManager EM;
    public ComponentManager CM;
    public SystemManager SM;

    public ECSCoordinator()
    {
        EM = new();
        CM = new();
        SM = new();
    }

    public int CreateEntity()
    {
        return EM.CreateEntity();
    }

    public void DestroyEntity(int entity)
    {
        EM.DestroyEntity(entity);
        CM.EntityDestroyed(entity);
        SM.EntityDestroyed(entity);
    }

    public void RegisterComponent<T>(int n = 2000) where T : struct
    {
        CM.RegisterComponent<T>(n);
    }

    public void AddComponent<T>(int entity, T component) where T : struct
    {
        CM.AddComponent<T>(entity, component);
        Signature signature = EM.GetSignature(entity);
        signature.Set(CM.GetComponentType<T>(), true);
        EM.SetSignature(entity, signature);
        SM.EntitySignatureChanged(entity, signature);
    }

    public void RemoveComponent<T>(int entity) where T : struct
    {
        CM.RemoveComponent<T>(entity);
        Signature signature = EM.GetSignature(entity);
        signature.Set(CM.GetComponentType<T>(), false);
        EM.SetSignature(entity, signature);
        SM.EntitySignatureChanged(entity, signature);
    }

    public ref T GetComponent<T>(int entity) where T : struct
    {
        return ref CM.GetComponent<T>(entity);
    }

    public int GetComponentType<T>() where T : struct
    {
        return CM.GetComponentType<T>();
    }

    public T RegisterSystem<T>() where T : ECSSystem, new()
    {
        return SM.RegisterSystem<T>(this);
    }

    public void SetSystemSignature<T>(Signature signature) where T : ECSSystem
    {
        SM.SetSignature<T>(signature);
    }

    public T GetSystem<T>() where T : ECSSystem
    {
        return SM.GetSystem<T>();
    }
}