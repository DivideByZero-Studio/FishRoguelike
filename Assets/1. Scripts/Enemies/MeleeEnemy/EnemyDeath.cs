using System.Collections;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private float _timeToDeactivate;

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
