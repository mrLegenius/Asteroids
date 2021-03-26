using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Синглтон класс, ответственный за управление пулами
/// </summary>
public class PoolManager : MonoBehaviour
{
    [SerializeField] private Pool[] startingPools;
    public static PoolManager Instance;
    
    private readonly Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();
    
    private void Awake()
    {
        if(Instance)
            Destroy(gameObject);
        else
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        foreach (var pool  in startingPools)
        {
            AddPool(pool);
        }
    }

    /// <summary>
    /// Получение объекта по имени obj из пула. Если нет нужного пула, то он создается.
    /// </summary>
    /// <param name="obj">Объект по имени которого берется объект из пула</param>
    /// <returns>Объект пула</returns>
    public GameObject GetObject(GameObject obj)
    {
        return _pools.ContainsKey(obj.name) ? _pools[obj.name].GetEntity() : AddPool(obj).GetEntity();
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
    
    private Pool AddPool(GameObject obj)
    {
        var newPool = new Pool(obj);
        _pools.Add(obj.name, newPool);

        return newPool;
    }
    private void AddPool(Pool pool)
    {
        _pools.Add(pool.Prefab.name, pool);
        pool.Initialize();
    }
}
