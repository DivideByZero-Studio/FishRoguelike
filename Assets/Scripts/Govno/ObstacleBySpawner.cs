using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;

    private void OnEnable()
    {
        _spawner.OnDone += Delete;
    }

    private void OnDisable()
    {
        _spawner.OnDone -= Delete;
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
