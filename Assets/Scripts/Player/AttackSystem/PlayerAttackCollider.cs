using System;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    public event Action<IDamageable> DamageableEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            DamageableEntered?.Invoke(damageable);
        }
    }

    public void SetRotation(Vector2 direction)
    {
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle); 
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
