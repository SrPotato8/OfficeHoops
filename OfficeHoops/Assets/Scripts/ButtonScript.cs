using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    Vector3 CurrentScale = Vector3.one;

    public AudioClip Keys;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        CurrentScale = transform.localScale;
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.transform.localScale = CurrentScale + new Vector3(0.2f, 0.2f, 0.2f); 
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        button.transform.localScale = CurrentScale;
    }

    //ButtonFunctions
    public void ExitFunction()
    {
        Application.Quit();
    }

    public void OptionsFunction()
    {
        PlayAudio(Keys);
        StartCoroutine(a1(Keys.length));
    }
    IEnumerator a1(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("OptionsScene");
    }

    public void LevelsFunction()
    {
        PlayAudio(Keys);
        StartCoroutine(a2(Keys.length));
    }
    IEnumerator a2(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("LevelsScene");
    }

    public void BackToMenu()
    {
        PlayAudio(Keys);
        StartCoroutine(a3(Keys.length));
    }

    IEnumerator a3(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }

    //Levels
    public void Level0()
    {
        PlayAudio(Keys);
        StartCoroutine(a4(Keys.length));
    }
    IEnumerator a4(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Tutorial");
    }

    public void Level1()
    {
        PlayAudio(Keys);
        StartCoroutine(a5(Keys.length));
    }

    IEnumerator a5(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        PlayAudio(Keys);
        StartCoroutine(a6(Keys.length));
    }

    IEnumerator a6(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Level2");
    }
    public void Level3()
    {
        PlayAudio(Keys);
        StartCoroutine(a7(Keys.length));
    }
    IEnumerator a7(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Level3");
    }
    public void Level4()
    {
        PlayAudio(Keys);
        StartCoroutine(a8(Keys.length));
    }
    IEnumerator a8(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Level4");
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