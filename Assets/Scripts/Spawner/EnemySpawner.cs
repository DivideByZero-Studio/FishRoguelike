using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnTrigger _trigger;
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private List<GameObject> _spawnedObjects;

    private void Awake()
    {
        _spawnedObjects = new List<GameObject>();
    }

    private void Spawn(SpawnPoint spawnPoint)
    {
        Type type = spawnPoint.Type;
        _spawnedObjects.Add(ObjectPool.Instance.SmartGetObject(type));
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn(_spawnPoints[0]);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            DespawnAll();
        }
    }

    private void DespawnAll()
    {
        foreach (GameObject obj in _spawnedObjects)
        {
            ObjectPool.Instance.DeactivateObject(obj);
        }
    }
}
