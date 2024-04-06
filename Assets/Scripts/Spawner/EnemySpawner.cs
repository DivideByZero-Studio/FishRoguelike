using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnTrigger _trigger;
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private List<GameObject> _spawnedObjects;
    private Transform _playerTransform;

    private void Awake()
    {
        _spawnedObjects = new List<GameObject>();
    }

    private GameObject Spawn(SpawnPoint spawnPoint)
    {
        Type type = spawnPoint.Type;
        var obj = ObjectPool.Instance.SmartGetObject(type);
        if (obj.TryGetComponent<IPoolObject>(out var poolObj))
        {
            poolObj.Init(spawnPoint.Position);
        }
        _spawnedObjects.Add(obj);
        return obj;
    }

    private void SpawnEnemyCluster(Transform playerTransform)
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            var obj = Spawn(spawnPoint);
            if (obj.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.SetPlayerTransform(playerTransform);
            }
        }
    }

    private void OnEnable()
    {
        _trigger.OnEntered += SpawnEnemyCluster;
    }
}
