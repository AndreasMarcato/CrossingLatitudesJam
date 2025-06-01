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

    [Header("Audio Clip Data"), Tooltip("All the SFX you want to spawn in the Scene.")]
    [SerializeField] private List<AudioClipData> audioClipDatas;

    private Dictionary<string, AudioClipData> clipDataMap = new();

    [Header("Events")]
    public EventManagerSO eventManager;


    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mixer;

    private readonly string[] trackVolumeParams = new string[]
    {
        "MainMenuLoop",
        "Track1",
        "Track2",
        "Track3",
        "Track4",
        "Track5",
        "Track6",
        "Track7"
    };

    private void Awake()
    {
        foreach (var data in audioClipDatas)
        {
            if (!clipDataMap.ContainsKey(data.id))
                clipDataMap.Add(data.id, data);
        }
        for (int i = 1; i < trackVolumeParams.Length; i++)
        {
            SetTrackVolume(trackVolumeParams[i], 0f);
            Debug.LogError("Test");
        }
    }

    private void OnEnable()
    {
        eventManager.onInteractCube += HandleVolumeTrack1;
        eventManager.onGameStartEvent += MainMenuLoopVolume;
    }


    private void OnDisable()
    {
        eventManager.onInteractCube -= HandleVolumeTrack1;
    }


    #region VOLUME TRACK HANDLERS
    private void MainMenuLoopVolume()
    {
        SetTrackVolume(trackVolumeParams[0], 0f);
    }
    private void HandleVolumeTrack1()
    {
        SetTrackVolume(trackVolumeParams[1], 1f);
    }
    private void HandleVolumeTrack2()
    {
        SetTrackVolume(trackVolumeParams[1], 0f);
    }
    private void HandleVolumeTrack3()
    {
        SetTrackVolume(trackVolumeParams[1], 0f);
    }
    private void HandleVolumeTrack4()
    {
        SetTrackVolume(trackVolumeParams[1], 0f);
    }
    private void HandleVolumeTrack5()
    {
        SetTrackVolume(trackVolumeParams[1], 0f);
    }
    private void HandleVolumeTrack6()
    {
        SetTrackVolume(trackVolumeParams[1], 0f);
    }
    private void HandleVolumeTrack7()
    {
        SetTrackVolume(trackVolumeParams[1], 0f);
    }
    #endregion


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

    public void SetTrackVolume(string trackName, float targetNormalized)
    {
        StartCoroutine(LerpTrackVolume(trackName, targetNormalized, 2f));
    }

    private IEnumerator LerpTrackVolume(string trackName, float targetNormalized, float duration)
    {
        if (!mixer.GetFloat(trackName, out float currentDb))
        {
            Debug.LogWarning($"Failed to get volume. Exposed parameter '{trackName}' may not exist.");
            yield break;
        }

        float startNormalized = Mathf.InverseLerp(-80f, 0f, currentDb);
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            float interpolated = Mathf.Lerp(startNormalized, targetNormalized, t);
            float dB = Mathf.Lerp(-80f, 0f, interpolated);
            mixer.SetFloat(trackName, dB);
            yield return null;
        }

        float finalDb = Mathf.Lerp(-80f, 0f, targetNormalized);
        mixer.SetFloat(trackName, finalDb);
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