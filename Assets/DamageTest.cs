using UnityEngine;

public class DamageTest : MonoBehaviour, IDamageable
{
    public void TakeDamage(int value)
    {
       Debug.Log("damage taken: " +  value);
    }
}
