using System.Collections;
using TMPro;
using UnityEngine;

public class SpeechSystem : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Assign in Inspector
    public string fullText = "Hello, world!";
    public float displayTime = 2f; // Time in seconds for the full text to appear
    public float shakeIntensity = 2f; // Shake intensity
    public float shakeSpeed = 0.05f; // Speed of shaking (lower = faster)

    public AudioClip[] talkSounds;

    AudioSource audioSource;
    private float letterDelay;

    private Coroutine typeTextCoroutine;
    private Coroutine shakeLettersCoroutine;

    void Start()
    {
        letterDelay = displayTime / fullText.Length;
        audioSource = GetComponent<AudioSource>();
        StartSpeech();
    }

    void OnEnable()
    {
        // Restart the speech when the object is activated
        StartSpeech();
    }

    void OnDisable()
    {
        // Stop coroutines when the object is deactivated
        StopSpeech();
    }

    public void StartSpeech()
    {
        // Stop any existing coroutines to avoid duplicates
        StopSpeech();

        // Clear the text and start new coroutines
        textComponent.text = "";
        typeTextCoroutine = StartCoroutine(TypeText());
        shakeLettersCoroutine = StartCoroutine(ShakeLetters());
    }

    public void StopSpeech()
    {
        if (typeTextCoroutine != null)
        {
            StopCoroutine(typeTextCoroutine);
        }
        if (shakeLettersCoroutine != null)
        {
            StopCoroutine(shakeLettersCoroutine);
        }
    }

    IEnumerator TypeText()
    {
        textComponent.text = ""; // Clear text first
        int charPerLine = 30; // Adjust for word wrapping
        int currentLineLength = 0;

        for (int i = 0; i < fullText.Length; i++)
        {
            if (fullText[i] == ' ' && currentLineLength >= charPerLine)
            {
                textComponent.text += "\n"; // Add a new line at a space
                currentLineLength = 0;
            }
            else
            {
                textComponent.text += fullText[i]; // Add next character
                currentLineLength++;
                audioSource.PlayOneShot(talkSounds[Random.Range(0, talkSounds.Length)]);

                // Update mesh for new letters
                textComponent.ForceMeshUpdate();
            }

            yield return new WaitForSeconds(letterDelay);
        }
    }

    IEnumerator ShakeLetters()
    {
        while (true) // Keep shaking while TMP is active
        {
            textComponent.ForceMeshUpdate();
            TMP_TextInfo textInfo = textComponent.textInfo;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible) continue;

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                Vector3[] vertices = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].vertices;

                float shakeX = Random.Range(-shakeIntensity, shakeIntensity);
                float shakeY = Random.Range(-shakeIntensity, shakeIntensity);

                for (int j = 0; j < 4; j++) // Apply shake to all 4 vertices of the letter
                {
                    vertices[vertexIndex + j] += new Vector3(shakeX, shakeY, 0);
                }
            }

            textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            yield return new WaitForSeconds(shakeSpeed); // Control speed of shaking
        }
    }
}