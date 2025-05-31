using System;
using UnityEngine;



[CreateAssetMenu(fileName = "EventManager", menuName = "Scriptable Objects/Managers SO/EventManager")]
public class EventManagerSO : ScriptableObject
{

    public event Action onInteractCube;
    public event Action onInteractionProp;
    public event Action<string> onInteractNPC;
    
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



}
