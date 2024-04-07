using UnityEngine;

public class Boot : MonoBehaviour
{
    private void Awake()
    {
        SceneLoader.LoadScene("MainMenu");
    }
}
