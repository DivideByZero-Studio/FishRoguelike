using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected Dictionary<Type, Behaviour> _behavioursMap;
    protected Behaviour _currentBehaviour;

    private void Start()
    {
        InitBehaviours();
        SetBehaviourByDefault();
    }

    private void Update()
    {
        _currentBehaviour.Update();
    }

    protected virtual void InitBehaviours()
    {
        _behavioursMap = new Dictionary<Type, Behaviour>();
    }

    protected void SetBehaviour(Behaviour newBehaviour)
    {
        if (newBehaviour == _currentBehaviour)
        {
            return;
        }
        _currentBehaviour?.Exit();
        _currentBehaviour = newBehaviour;
        _currentBehaviour?.Enter();
    }

    protected Behaviour GetBehaviour<T>() where T : Behaviour
    {
        var type = typeof(T);
        return _behavioursMap[type];
    }

    protected virtual void SetBehaviourByDefault()
    {
        return;
    }

    protected virtual void Subscribe()
    {
        return;
    }

    protected virtual void Unsubscribe()
    {
        return;
    }

    protected virtual void OnEnable()
    {
        Subscribe();
    }

    protected virtual void OnDisable()
    {
        Unsubscribe();
    }
}
