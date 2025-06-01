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



    public void InteractProp(int trackIndex)
    {
        onInteractionProp?.Invoke(trackIndex);
        
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
