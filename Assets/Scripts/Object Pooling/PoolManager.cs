using System;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject GetObject(GameObject obj)
    {
        return _pools.ContainsKey(obj.name) ? _pools[obj.name].GetEntity() : AddPool(obj).GetEntity();
    }

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
