using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public event Action OnDone;
    [SerializeField] private SpawnTrigger _trigger;
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private List<GameObject> _spawnedObjects;
    private Transform _playerTransform;

    private bool _done = false;
    private bool _spawned = false;

    private void Awake()
    {
        _spawnedObjects = new List<GameObject>();
    }

    private void Update()
    {
        if (_done == false && _spawned)
        {
            foreach (var obj in _spawnedObjects)
            {
                if (obj.GetComponent<IPoolObject>().IsAlive == true) return;
            }
        }
        _done = true;
        OnDone?.Invoke();
    }


    private GameObject Spawn(SpawnPoint spawnPoint)
    {
        Type type = spawnPoint.Type;
        var obj = ObjectPool.Instance.SmartGetObject(type);
        if (obj.TryGetComponent<IPoolObject>(out var poolObj))
        {
            poolObj.Init(spawnPoint.Position);
            poolObj.IsAlive = true;
        }

        if (obj.TryGetComponent<Health>(out var healthObj))
        {
            healthObj.SetHealthOnMaxValue();
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
        _spawned = true;
    }

    private void OnEnable()
    {
        _trigger.OnEntered += SpawnEnemyCluster;
    }
}
