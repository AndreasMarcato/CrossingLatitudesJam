using UnityEngine;

[System.Serializable]
public class AudioClipData
{
    // Used in code to match to events like "SFX_InteractProp"
    public string id; 
    public AudioClip[] clips;
    public Vector2 pitchRange = new Vector2(1f, 1f);
}