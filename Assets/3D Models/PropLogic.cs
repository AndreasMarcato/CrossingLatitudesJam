using UnityEngine;

public class PropLogic : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log($"{name} was interacted with!");
        // Example action:
        GetComponent<Renderer>().material.color = Color.yellow;
    }
}
