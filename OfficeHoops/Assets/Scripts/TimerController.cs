using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance;

    public float totalTime = 63f; // 10 minutos = 600 segundos
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI NumberOfHoops;

    private float remainingTime;
    private bool isRunning = true;

    public float NumberBuckets = 0f;
    public float NumberBucketsNeeded = 3f;

    void Start()
    {
        remainingTime = totalTime;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (isRunning)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0)
            {
                remainingTime = 0;
                isRunning = false;
                OnTimerEnd();
            }

            UpdateTimerDisplay();
        }

        NumberOfHoops.text = NumberBuckets.ToString();
        
            //if (NumberBuckets == NumberBucketsNeeded)
            //{
            //    Invoke("NextScene", 1.5f);
            //}
        
    }

    //private void NextScene()
    //{
    //    SceneManager.LoadScene("Level3");
    //}


    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerEnd()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void AddTime(float seconds)
    {
        remainingTime += seconds;
        UpdateTimerDisplay();
    }
}