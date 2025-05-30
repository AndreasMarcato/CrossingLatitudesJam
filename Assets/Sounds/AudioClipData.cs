using UnityEngine;

[System.Serializable]
public class AudioClipData
{
    public string id; // Used in code to match to events like "SFX_TurretShot"
    public AudioClip[] clips;
    public Vector2 pitchRange = new Vector2(1f, 1f);
}