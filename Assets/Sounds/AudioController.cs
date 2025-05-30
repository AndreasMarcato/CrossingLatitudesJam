using InfinityCode.UltimateEditorEnhancer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [Header("Audio Setup")]
    [SerializeField] private AudioSource sfxSourcePrefab;
    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioMixerGroup sfxMixer;
    [SerializeField] private AudioMixerGroup bgmMixer;

    [Header("Audio Clip Data")]
    [SerializeField] private List<AudioClipData> audioClipDatas;

    private Dictionary<string, AudioClipData> clipDataMap = new();

    [Header("Events")]
    public EventManagerSO eventManager;


    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mixer;

    private readonly string[] trackVolumeParams = new string[]
    {
    "Volume_Track1",
    "Volume_Track2",
    "Volume_Track3",
    "Volume_Track4",
    "Volume_Track5",
    "Volume_Track6",
    "Volume_Track7"
    };

    private void Awake()
    {
        foreach (var data in audioClipDatas)
        {
            if (!clipDataMap.ContainsKey(data.id))
                clipDataMap.Add(data.id, data);
        }
    }

    private void OnEnable()
    {
        eventManager.onCubeInteract += Track1VolumeTest;
    }

   

    private void OnDisable()
    {
        eventManager.onCubeInteract -= Track1VolumeTest;
    }

    // Event handlers
    private void Track1VolumeTest()
    {
        SetTrackVolume("Track1", 0f);
    }
    //=======

    public void PlaySFX(string id)
    {
        if (!clipDataMap.TryGetValue(id, out var data)) return;

        var clip = data.clips[UnityEngine.Random.Range(0, data.clips.Length)];
        var sfxSource = Instantiate(sfxSourcePrefab, transform);
        sfxSource.outputAudioMixerGroup = sfxMixer;
        sfxSource.pitch = UnityEngine.Random.Range(data.pitchRange.x, data.pitchRange.y);
        sfxSource.clip = clip;
        sfxSource.Play();

        Destroy(sfxSource.gameObject, clip.length / sfxSource.pitch + 0.1f);
    }

    public void SetTrackVolume(string trackName, float normalizedVolume)
    {
        string parameterName = trackName;
        float dB = Mathf.Lerp(-80f, 0f, Mathf.Clamp01(normalizedVolume));

        bool success = mixer.SetFloat(parameterName, dB);
        if (!success)
        {
            Debug.LogWarning($"Failed to set volume. Exposed parameter '{parameterName}' may not exist in the AudioMixer.");
        }
    }

    public float GetTrackVolume(string trackName)
    {
        string parameterName = trackName;

        if (mixer.GetFloat(parameterName, out float dB))
        {
            return Mathf.InverseLerp(-80f, 0f, dB);
        }

        Debug.LogWarning($"Failed to get volume. Exposed parameter '{parameterName}' may not exist in the AudioMixer.");
        return 0f;
    }



}