using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MonoPool
{
    public GameObject Prefab => _prefab;

    public int ActiveEntitiesCount => _pool.Count(entity
        => entity.gameObject.activeInHierarchy);

    public IEnumerable<GameObject> ActiveEntities => _pool
        .Where(entity => entity.gameObject.activeInHierarchy);

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private int _startAmount = 5;

    private readonly List<GameObject> _pool = new List<GameObject>();
    private Transform _container;

    public MonoPool() { }

    public MonoPool(GameObject prefab, int startAmount = 5)
    {
        _prefab = prefab;
        _startAmount = startAmount;
        
        AddInstancesToPool(_startAmount);
    }
    
    public void AddInstancesToPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateNewEntity();
        }
    }

    public void SetContainer(Transform container)
    {
        _container = container;
    }

    /// <summary>
    /// Получение объекта из пула, если он не активен. Создание нового объекта, когда нет ни одного неактивного объекта.
    /// </summary>
    /// <returns>Игровой объект из пула</returns>
    public GameObject GetEntity()
    {
        foreach (var entity in _pool.Where(entity
            => !entity.gameObject.activeInHierarchy))
        {
            return entity;
        }

        return CreateNewEntity();
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
        if (!_prefab)
        {
            Debug.LogError("No Prefab in a pool");
            return null;
        }

        if (!_container)
        {
            _container = new GameObject().transform;
            _container.name = _prefab.name;
        }

        var newEntity = Object.Instantiate(_prefab, _container);
        _pool.Add(newEntity);

        newEntity.SetActive(false);

        return newEntity;
    }
}