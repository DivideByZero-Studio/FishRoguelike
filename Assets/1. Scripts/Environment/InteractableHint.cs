using UnityEngine;

public class InteractableHint : MonoBehaviour
{
    [SerializeField] private GameObject hintObject;

    private bool _canBeShown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_canBeShown && collision.TryGetComponent<PlayerVisuals>(out var playerVisuals))
        {
            Show();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerVisuals>(out var playerVisuals))
        {
            Hide();
        }
    }

    private void Show()
    {
        hintObject.SetActive(true);
    }

    private void Hide()
    {
        hintObject.SetActive(false);
    }

    public void On()
    {
        _canBeShown = true;
    }

    public void Off()
    {
        _canBeShown = false;
        Hide();
    }
}
