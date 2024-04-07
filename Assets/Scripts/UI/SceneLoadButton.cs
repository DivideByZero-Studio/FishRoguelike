using UnityEngine;

public class SceneLoadButton : MonoBehaviour
{
    [SerializeField] private string _sceneNameToLoad;

    public void LoadSceneInstantly()
    {
        SceneLoader.LoadSceneInstantly(_sceneNameToLoad);
    }

    public void LoadScene()
    {
        SceneLoader.LoadScene(_sceneNameToLoad);
    }
}
