using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;



[CreateAssetMenu(fileName = "EventManager", menuName = "Scriptable Objects/Managers SO/EventManager")]
public class EventManagerSO : ScriptableObject
{

    public event Action onInteractCube;
    public event Action onInteractionProp;
    public event Action<string> onInteractNPC;


    public GameObject playerReference;

    
    public void CubeInteract()
    {
        onInteractCube?.Invoke();
    }

    public void InteractProp()
    {
        onInteractionProp?.Invoke();
    }

    public void InteractNPC(string dialogueLine)
    {
        onInteractNPC?.Invoke(dialogueLine);
    }

    public void OpenGameSettings()
    {
        throw new NotImplementedException();
    }

    public void StartGame()
    {
        // START CUTSCENE
        ActivatePlayerInput();
    }


    public void ActivatePlayerInput()
    {
        playerReference.GetComponent<PlayerInput>().currentActionMap.Enable();
    }
    public void DeactivatePlayerInput()
    {
        playerReference.GetComponent<PlayerInput>().currentActionMap.Disable();
    }


}

[UxmlElement]
public partial class LocalizedButton : Button
{
    public static BindingId keyProperty = nameof(key);

    [UxmlAttribute, CreateProperty]
    public string key;
    public LocalizedButton()
    {

    }
}
