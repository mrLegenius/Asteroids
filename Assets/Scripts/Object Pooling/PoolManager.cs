using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.PoolSystem
{
public class PoolManager : MonoBehaviour
{
    private readonly Dictionary<string, MonoPool> _pools =
        new Dictionary<string, MonoPool>();
    
    /// <summary>
    /// Получение объекта по имени obj из пула. Если нет нужного пула, то он создается.
    /// </summary>
    /// <param name="obj">Объект по имени которого берется объект из пула</param>
    /// <returns>Объект пула</returns>
    public GameObject GetObject(GameObject obj)
    {
        return _pools.ContainsKey(obj.name)
            ? _pools[obj.name].GetEntity()
            : CreatePoolFromGameObject(obj).GetEntity();
    }

    public MonoPool CreatePoolFromGameObject(GameObject go, int initialAmountInPool = 1)
    {
        if (_pools.ContainsKey(go.name)) return _pools[go.name];
        
        var newPool = new MonoPool(go, initialAmountInPool);
        _pools.Add(go.name, newPool);

        return newPool;
    }

    /// <summary>
    /// Отключение всех объектов во всех пулах
    /// </summary>
    public void DisableAllObjects()
    {
        foreach (var pool in _pools)
        {
            pool.Value.DisableAllEntities();
        }
    }
    
    private void AddPool(MonoPool monoPool)
    {
        _pools.Add(monoPool.Prefab.name, monoPool);
    }
}
}