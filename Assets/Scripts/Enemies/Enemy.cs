using UnityEngine;

public abstract class Enemy : MonoBehaviour, IPoolObject
{
    protected Transform _playerTransform;
    protected EnemyStateMachine _ESM;

    private void Awake()
    {
        _ESM = GetComponent<EnemyStateMachine>();
    }

    public void Init(Vector3 position)
    {
        transform.position = position;
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
        _ESM.Enable();
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
