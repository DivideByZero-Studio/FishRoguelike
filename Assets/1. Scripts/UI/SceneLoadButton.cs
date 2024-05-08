using UnityEngine;

public class SceneLoadButton : MonoBehaviour
{
    [SerializeField] private string _sceneNameToLoad;
    [SerializeField] private AudioClip clickSound;

    public void LoadSceneInstantly()
    {
        SceneLoader.LoadSceneInstantly(_sceneNameToLoad);
        AudioManager.Instance.PlaySFX(clickSound, 0.7f);
    }

    public void LoadScene()
    {
        SceneLoader.LoadScene(_sceneNameToLoad);
        AudioManager.Instance.PlaySFX(clickSound, 0.7f);
    }
}
