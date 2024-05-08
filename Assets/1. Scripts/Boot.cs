using UnityEngine;
using Zenject;

public class Boot : MonoBehaviour
{
    [Inject] private GameInput gameInput;

    private void Awake()
    {
        gameInput.OnPrimaryAttack += Print;
    }
    private void Print()
    {
        Debug.Log("sdfksfd");
    }
}
