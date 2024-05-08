using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private EnemyType _enemyType;

    public Vector3 Position => transform.position;
    public Type Type => _enemyType.GetEnemyType();
}
