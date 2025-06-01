using UnityEngine;
using UnityEngine.InputSystem;

public class ReferenceHandler : MonoBehaviour
{
    public EventManagerSO eventManager;
    private void Awake()
    {
        eventManager.referenceHandler = this;

    }

    public Transform playerReference;

    public void ActivatePlayerInput()
    {
        playerReference.GetComponent<PlayerInput>().currentActionMap.Enable();
    }
    public void DeactivatePlayerInput()
    {
        playerReference.GetComponent<PlayerInput>().currentActionMap.Disable();
    }
}
