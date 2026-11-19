using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private string displayName = "Interact";
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private UnityEvent onInteract;
    public string DisplayName => displayName;

    public bool CanInteract() => isEnabled;


    public void Interact()
    {
        onInteract?.Invoke();
    }

    public void OnFocusGained()
    {
        throw new System.NotImplementedException();
    }

    public void OnFocusLost()
    {
        throw new System.NotImplementedException();
    }
}
