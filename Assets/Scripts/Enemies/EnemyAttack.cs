using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public abstract void Attack();

    public abstract void StartAttack(Transform _playerTransform);
}
