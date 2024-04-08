using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    private void Awake()
    {
        AudioManager.Instance.PlayMusic(music);
    }
}
