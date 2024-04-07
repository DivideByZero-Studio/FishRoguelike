using UnityEngine;

public abstract class Enemy : MonoBehaviour, IPoolObject
{
    [SerializeField] protected Transform _playerTransform;

    public void Init(Vector3 position)
    {
        transform.position = position;
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public Transform GetPlayerTransform()
    {
        return _playerTransform;
    }

    public void Term()
    {
        transform.position = ObjectPool.DefaultPosition;
    }

    private void OnDisable()
    {
        _playerTransform = null;
    }
}
