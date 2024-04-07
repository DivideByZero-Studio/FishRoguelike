using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    private bool _isConditionsSatisfied;

    public void SatisfyConditions()
    {
        _isConditionsSatisfied = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isConditionsSatisfied == false) return;

        if (collision.TryGetComponent<PlayerVisuals>(out var playerVisuals))
        {
            Debug.Log("level completed");
        }
    }
}
