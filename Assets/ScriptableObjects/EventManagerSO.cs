using System;
using UnityEngine;



[CreateAssetMenu(fileName = "EventManager", menuName = "Scriptable Objects/Managers SO/EventManager")]
public class EventManagerSO : ScriptableObject
{

    public event Action onCubeInteract;

    
    public void CubeInteract()
    {
        onCubeInteract?.Invoke();
    }
}
