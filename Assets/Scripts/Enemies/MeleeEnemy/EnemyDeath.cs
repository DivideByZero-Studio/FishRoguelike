using System.Collections;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private const float _timeToDeactivate = 2f;

    public void Dead()
    {
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(_timeToDeactivate);
        ObjectPool.Instance.DeactivateObject(gameObject);
    }
}
