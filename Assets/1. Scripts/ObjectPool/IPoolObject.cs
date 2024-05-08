using UnityEngine;

public interface IPoolObject
{
    public bool IsAlive { get; set; }
    void Init(Vector3 position);

    void Term();
}
