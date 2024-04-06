using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Transform _playerTransform;
    public abstract void SetPlayerTransform(Transform playerTransform);

    private void OnDisable()
    {
        _playerTransform = null;
    }
}
