
using System.Collections;
using UnityEngine;
using TMPro;

public class TypingText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float typingSpeed = 0.05f;
    public float cursorBlinkSpeed = 0.5f;
    public bool TitleWritten;

    private string fullText;
    private bool isTyping = true;
    private string currentText = ""; // Almacena el texto actual sin el cursor

    public AudioClip Keys;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        TitleWritten = false;
        fullText = textMeshPro.text;
        textMeshPro.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in fullText)
        {
            currentText += letter; // Añade la letra sin modificar el cursor
            PlayAudio(Keys);
            textMeshPro.text = currentText + "|"; // Muestra el cursor al final del texto
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        textMeshPro.text = currentText;
        TitleWritten = true;
    }

    public void PlayAudio(AudioClip audio)
    {
        if (audio != null)
        {
            audioSource.clip = audio;
            audioSource.Play();
        }
    }

}


