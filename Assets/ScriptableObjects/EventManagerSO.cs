using System;
using UnityEngine;



[CreateAssetMenu(fileName = "EventManager", menuName = "Scriptable Objects/Managers SO/EventManager")]
public class EventManagerSO : ScriptableObject
{
    public event Action<int> onInteractionProp;
    public event Action<string> onInteractNPC;

    public void PropInteract(int trackIndex)
    {
        onInteractionProp?.Invoke(trackIndex);
    }

    public void InteractNPC(string dialogueLine)
    {
        onInteractNPC?.Invoke(dialogueLine);
    }



}
