using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    [SerializeField] private UFO prefab;
    [SerializeField] private float minSpawnTime, maxSpawnTime;
    
    [SerializeField] private Vector2 spawnOffset;
    
    private Camera _mainCamera;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void StartSpawning()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        
        var x = Screen.width + spawnOffset.x;
        var y = Random.Range(-spawnOffset.y, Screen.height + spawnOffset.y);
        var position = _mainCamera.ScreenToWorldPoint(new Vector2(x, y));
        position.z = 0;
        var ufo = PoolManager.Instance.GetObject(prefab.gameObject);

        var ufoTransform = ufo.transform;
        ufoTransform.position = position;

        ufo.GetComponent<UFO>().StartMoving();
        ufo.SetActive(true);
        
        StartCoroutine(Spawn());
    }
}
