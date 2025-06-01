using TMPro;
using UnityEngine;

public class SignifierLogic : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI signifierTextInteraction;
    public EventManagerSO eventManager;


    [Tooltip("Minimum distance at which the Canvas becomes visible.")]
    public float minVisibleDistance = 2f;

    [Tooltip("Maximum distance at which the Canvas remains visible.")]
    public float maxVisibleDistance = 10f;
    
    
    
    void Start()
    {
        if (playerCamera == null && Camera.main != null)
        {
            playerCamera = Camera.main.transform;
        }
    }
    private void OnEnable()
    {
        eventManager.onSignifierUpdate += SetSignifierText;
    }
    private void OnDisable()
    {
        eventManager.onSignifierUpdate -= SetSignifierText;
    }

    private void SetSignifierText(string incomingText)
    {
        signifierTextInteraction.SetText(incomingText);
    }

    void LateUpdate()
    {

        float distance = Vector3.Distance(transform.position, playerCamera.position);

        // Enable the canvas only if the player is within the specified range.
        bool inRange = (distance >= minVisibleDistance && distance <= maxVisibleDistance);
        if (canvas.enabled != inRange)
        {
            canvas.enabled = inRange;
        }
        if (canvas.enabled)
        {
            transform.LookAt(playerCamera);
            transform.rotation = Quaternion.LookRotation(playerCamera.forward);
        }
    }
}
