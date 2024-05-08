using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnTrigger : MonoBehaviour
{
    public event Action<Transform> OnEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerStateMachine>(out var playerSM))
        {
            OnEntered?.Invoke(collision.gameObject.GetComponent<Transform>());
            Destroy(gameObject);
        }
    }
}
