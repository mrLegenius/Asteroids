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

    /// <summary>
    /// Создает пустой пул
    /// </summary>
    public Pool() { }
    
    /// <summary>
    /// Создает пул переданного объекта
    /// </summary>
    /// <param name="prefab">Игровой объект для которого создается пул</param>
    public Pool(GameObject prefab)
    {
        this.prefab = prefab;
    }

    /// <summary>
    /// Создает начальное число игровый объектов в пуле
    /// </summary>
    public void Initialize()
    {
        for (int i = 0; i < startAmount; i++)
        {
            CreateNewEntity();
        }
    }
    
    /// <summary>
    /// Меняет контейнер в котором создаются новые объекты
    /// </summary>
    /// <param name="parent">Новый контейнер</param>
    public void SetParent(Transform parent)
    {
        _parent = parent;
    }

    /// <summary>
    /// Получение объекта из пула, если он не активен. Создание нового объекта, когда нет ни одного неактивного объекта.
    /// </summary>
    /// <returns>Игровой объект из пула</returns>
    public GameObject GetEntity()
    {
        foreach (var entity in _pool.Where(entity => !entity.gameObject.activeInHierarchy))
        {
            return entity;
        }
        
        return CreateNewEntity();
    }
    
    /// <summary>
    /// Возвращает количество активных объектов
    /// </summary>
    public int ActiveEntitiesCount()
    {
        return _pool.Count(entity => entity.gameObject.activeInHierarchy);
    }

    /// <summary>
    /// Возвращает список активных объектов
    /// </summary>
    public List<GameObject> GetActiveEntities()
    {
        return _pool.Where(entity => entity.gameObject.activeInHierarchy).ToList();
    }

    /// <summary>
    /// Отключает все объекты в пуле
    /// </summary>
    public void DisableAllEntities()
    {
        foreach (var gameObject in _pool)
        {
            gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// Добавляет в пул новый объект
    /// </summary>
    /// <returns>Ссылка на новый объект или null, если нет префаба</returns>
    private GameObject CreateNewEntity()
    {
        if (!prefab)
        {
            Debug.LogError("No Prefab in a pool");
            return null;
        }

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
