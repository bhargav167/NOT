using System;
using Tero.ObjectPoolSystem;
using UnityEngine;

namespace Tero.Interfaces
{
    public interface IObjectPoolItem
    {
        void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component;

        void Release();
    }
}