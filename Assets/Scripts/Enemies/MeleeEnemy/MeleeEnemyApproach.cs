using UnityEngine;

public class MeleeEnemyApproach : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _playerTransform;

    public void StartApproach(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public void Approach()
    {

    }

    public void StopApproach()
    {
        _playerTransform = null;
    }
}
