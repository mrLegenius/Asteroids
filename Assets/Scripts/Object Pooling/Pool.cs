using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public GameObject Prefab => prefab;

    [SerializeField] private GameObject prefab;
    [SerializeField] private int startAmount = 5;
    
    private readonly List<GameObject> _pool = new List<GameObject>();
    private Transform _parent;

    public Pool() { }
    
    public Pool(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public void Initialize()
    {
        for (int i = 0; i < startAmount; i++)
        {
            CreateNewEntity();
        }
    }
    
    public void SetParent(Transform parent)
    {
        _parent = parent;
    }

    public GameObject GetEntity()
    {
        if(!prefab) Debug.LogError("No Prefab in a pool");

        foreach (var entity in _pool.Where(entity => !entity.gameObject.activeInHierarchy))
        {
            return entity;
        }
        
        return CreateNewEntity();
    }
    
    public int ActiveEntitiesCount()
    {
        return _pool.Count(entity => entity.gameObject.activeInHierarchy);
    }

    public List<GameObject> GetActiveEntities()
    {
        return _pool.Where(entity => entity.gameObject.activeInHierarchy).ToList();
    }

    public void DisableAllEntities()
    {
        foreach (var gameObject in _pool)
        {
            gameObject.SetActive(false);
        }
    }
    
    private GameObject CreateNewEntity()
    {
        if(!_parent) 
        {
            _parent = new GameObject().transform;
            _parent.name = prefab.name;
        }     

        var newEntity = Object.Instantiate(prefab, _parent);
        _pool.Add(newEntity);

        newEntity.SetActive(false);

        return newEntity;
    }
}
