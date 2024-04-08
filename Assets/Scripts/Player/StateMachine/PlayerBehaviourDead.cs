using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourDead : Behaviour
{
    private GameObject _obj;

    public PlayerBehaviourDead(GameObject obj)
    {
        _obj = obj;
    }

    public override void Enter()
    {
        Transform[] children = _obj.GetComponentsInChildren<Transform>();
        _obj.layer = LayerMask.NameToLayer("Dead");

        foreach (var child in children)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Dead");
        }
    }
}
