
//using System.Collections;
//using UnityEngine;

using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypingSubText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float typingSpeed = 0.05f;
    public float cursorBlinkSpeed = 0.5f;
    private TypingText Title;

    private string fullText;
    private bool isTyping = true;
    private bool showCursor = true;
    private string currentText = "";
    private float typingTimer = 0f;
    private float cursorTimer = 0f;
    private int charIndex = 0;

    public AudioClip Keys;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        fullText = textMeshPro.text;
        textMeshPro.text = "";
        Title = GameObject.Find("Title").GetComponent<TypingText>();
    }

    void Update()
    {
     
        if (Title.TitleWritten == true)
        {
            if (isTyping)
            {
                typingTimer += Time.deltaTime;
                if (typingTimer >= typingSpeed && charIndex < fullText.Length)
                {
                    currentText += fullText[charIndex];
                    PlayAudio(Keys);
                    textMeshPro.text = currentText + "|";
                    charIndex++;
                    typingTimer = 0f;
                }
                else if (charIndex >= fullText.Length)
                {
                    isTyping = false;
                }
            }
            else
            {
                cursorTimer += Time.deltaTime;
                if (cursorTimer >= cursorBlinkSpeed)
                {
                    showCursor = !showCursor;
                    textMeshPro.text = showCursor ? currentText + "|" : currentText;
                    cursorTimer = 0f;
                }
            }
            if (isTyping == false)
            {
                if (Input.anyKeyDown) // Detecta si se pulsa cualquier tecla
                {
                    SceneManager.LoadScene("MainMenu");
                    Debug.Log("hola caracola");
                }
            }

        }
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

