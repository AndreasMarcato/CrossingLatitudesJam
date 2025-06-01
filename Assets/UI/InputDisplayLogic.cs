using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputDisplayLogic : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private string signifierText;
    public EventManagerSO eventManager;

    void OnEnable()
    {

        if (playerInput == null)
        {
            playerInput = GameObject.FindFirstObjectByType<PlayerInput>();
        }

        UpdateLabelText();
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Reconnected || change == InputDeviceChange.Disconnected)
        {
            UpdateLabelText();
        }
    }

    void UpdateLabelText()
    {

        if (Gamepad.current != null && Gamepad.current.enabled)
        {
            signifierText = "[X]";
        }
        else if (Keyboard.current != null && Keyboard.current.enabled)
        {
            signifierText = "[E]";
        }
        else
        {
            signifierText = "Press [Interact]";
        }

        eventManager.UpdateTextSignifiers(signifierText);
    }
}
