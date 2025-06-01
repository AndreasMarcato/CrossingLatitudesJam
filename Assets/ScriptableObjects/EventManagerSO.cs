using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;



[CreateAssetMenu(fileName = "EventManager", menuName = "Scriptable Objects/Managers SO/EventManager")]
public class EventManagerSO : ScriptableObject
{
    public event Action onGameStartEvent;

    public event Action<int> onInteractionProp;

    public event Action<string> onInteractNPC;
    public event Action onPauseEvent;

    public ReferenceHandler referenceHandler;

    int numberOfInteractions = 1;

    //UI
    public event Action<string> onSignifierUpdate;



    public void InteractProp()
    {
        onInteractionProp?.Invoke(numberOfInteractions);
        numberOfInteractions++;
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
        numberOfInteractions = 1;
        referenceHandler.ActivatePlayerInput();
        onGameStartEvent?.Invoke();
    }


    public void Pause()
    {
        onPauseEvent?.Invoke();
    }

    internal void UpdateTextSignifiers(string newSignifierText)
    {
        onSignifierUpdate?.Invoke(newSignifierText);
        Debug.Log(newSignifierText);
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
