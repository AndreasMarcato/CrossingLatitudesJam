using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private Camera playerCamera;
    private StarterAssets.StarterAssetsInputs input;

    public EventManagerSO eventManager;


    private void Awake()
    {
        GameObject.FindFirstObjectByType<ReferenceHandler>().playerReference = transform;

        input = GetComponent<StarterAssets.StarterAssetsInputs>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (input.interact)
        {
            input.interact = false; // Reset flag

            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit[] hits = Physics.RaycastAll(ray, interactionDistance);

            if (hits.Length > 0)
            {
                foreach (RaycastHit hit in hits)
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.green, 1f);
                    Debug.Log("Ray hit: " + hit.collider.name);

                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.Interact();
                    }
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red, 1f);
                Debug.Log("Raycast missed");
            }
        }
    }

}
