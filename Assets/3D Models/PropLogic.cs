using UnityEngine;

public class PropLogic : MonoBehaviour, IInteractable
{
    [Header("Event Manager")]
    [SerializeField] EventManagerSO eventManager;

    [Header("Track Info")]
    [SerializeField] public int trackIndex;

    public void Interact()
    {
        Debug.Log($"{name} was interacted with! Triggering track {trackIndex}");

        eventManager.PropInteract(trackIndex);
    }
}
