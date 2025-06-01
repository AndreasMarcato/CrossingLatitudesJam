using TMPro;
using UnityEngine;

public class SignifierLogic : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private TextMeshProUGUI signifierTextInteraction;
    public EventManagerSO eventManager;

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
        if (playerCamera == null) return;

        transform.LookAt(playerCamera);
        transform.rotation = Quaternion.LookRotation(playerCamera.forward);
    }
}
