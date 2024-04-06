using UnityEngine;

public class EnemyTest : Enemy
{
    public override void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }
}
