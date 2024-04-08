using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    private const string PATH = "AudioManager";

    [Header("Audio sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundFXSource;

    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var prefab = Resources.Load<AudioManager>(PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    private const float _defaultPitch = 1f;

    public void PlayMusic(AudioClip clip)
    {
        if (_musicSource.clip == clip) return;

        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void PlayMusicForced(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.clip = null;
        _musicSource.loop = false;
        _musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        _soundFXSource.pitch = _defaultPitch;
        _soundFXSource.volume = volume;
        _soundFXSource.PlayOneShot(clip);
    }

    public void PlayRandomPitchedSFX(AudioClip clip, float minPitch, float maxPitch)
    {
        _soundFXSource.pitch = Random.Range(minPitch, maxPitch);
        _soundFXSource.PlayOneShot(clip);
    }
}