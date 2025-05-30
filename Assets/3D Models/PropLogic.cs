using UnityEngine;

public class PropLogic : MonoBehaviour, IInteractable
{
    [SerializeField] EventManagerSO eventManager;
    public void Interact()
    {
        Debug.Log($"{name} was interacted with!");
        // Example action:
        eventManager.CubeInteract();
    }
}
