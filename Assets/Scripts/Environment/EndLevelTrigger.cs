using UnityEngine;
using UnityEngine.Events;

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoadName;
    private bool _isConditionsSatisfied;

    [SerializeField] private UnityEvent OnLevelEnd;

    public void SatisfyConditions()
    {
        _isConditionsSatisfied = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isConditionsSatisfied == false) return;

        if (collision.TryGetComponent<PlayerVisuals>(out var playerVisuals))
        {
            OnLevelEnd?.Invoke();
            SceneLoader.LoadScene(sceneToLoadName);
        }
    }
}
