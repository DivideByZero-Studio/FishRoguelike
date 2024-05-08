using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class MainEngine : MonoBehaviour
{
    [SerializeField] private MainTerminal mainTerminal;
    [SerializeField] private Color[] stateColors;
    [SerializeField] private float[] animationSpeedMultiplier;

    [SerializeField] private UnityEvent OnExtremeEngineCondition;
    [SerializeField] private AudioClip explosionSound;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        mainTerminal.StabilizerBroken += SetEngineIntensity;
    }

    private void OnDisable()
    {
        mainTerminal.StabilizerBroken -= SetEngineIntensity;
    }

    public void PlayExplosionSound()
    {
        AudioManager.Instance.PlaySFX(explosionSound);
    }

    private IEnumerator SoundRoutine()
    {
        yield return new WaitForSeconds(0.7f);
    }

    private void SetEngineIntensity(StabilizersState stabilizersState)
    {
        switch (stabilizersState)
        {
            case StabilizersState.OK:
                _spriteRenderer.color = stateColors[0];
                _animator.speed = animationSpeedMultiplier[0];
                break;
            case StabilizersState.INCORRECT:
                _spriteRenderer.color = stateColors[1];
                _animator.speed = animationSpeedMultiplier[1];
                break;
            case StabilizersState.UNSTABLE:
                _spriteRenderer.color = stateColors[2];
                _animator.speed = animationSpeedMultiplier[2];
                break;
            case StabilizersState.BAD:
                _spriteRenderer.color = stateColors[3];
                _animator.speed = animationSpeedMultiplier[3];
                break;
            case StabilizersState.EXTREME:
                OnExtremeEngineCondition?.Invoke();
                _spriteRenderer.color = stateColors[4];
                _animator.speed = animationSpeedMultiplier[4];
                break;
        }
    }
}
