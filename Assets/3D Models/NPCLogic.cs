using UnityEngine;

public class NPCLogic : MonoBehaviour, IInteractable
{
    [SerializeField] private EventManagerSO eventManager;

    [SerializeField] private string[] dialogueLines;
    int dialogueIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        if (dialogueLines[dialogueIndex+1] == null)
            return;

        eventManager.InteractNPC(dialogueLines[dialogueIndex]);
        dialogueIndex++;
    }

}
