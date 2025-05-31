using StarterAssets;
using UnityEngine;

public class DialogueUIController : MonoBehaviour
{
    public Canvas dialogueCanvas;
    public StarterAssetsInputs playerInput; // Assign in Inspector

    private bool isDialogueActive = false;

    public void ShowDialogue()
    {
        isDialogueActive = true;
        dialogueCanvas.enabled = true;

        playerInput.cursorLocked = false;
        playerInput.cursorInputForLook = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        DisablePlayerMovement();
    }

    public void HideDialogue()
    {
        isDialogueActive = false;
        dialogueCanvas.enabled = false;

        playerInput.cursorLocked = true;
        playerInput.cursorInputForLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EnablePlayerMovement();
    }

    private void DisablePlayerMovement()
    {
        playerInput.move = Vector2.zero;
        playerInput.look = Vector2.zero;
        playerInput.jump = false;
        playerInput.sprint = false;
    }

    private void EnablePlayerMovement()
    {
        // Restore movement here if you have a saved state
        // For now, movement will resume naturally on next frame via input system
    }
}
