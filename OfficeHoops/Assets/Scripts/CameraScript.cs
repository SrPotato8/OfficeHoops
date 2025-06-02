using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class CameraScript: MonoBehaviour
{
    public float targetY = 10f;  
    public float rotationX = 30f; 
    public float speed = 2f;     
    public GameObject uiMessage1; 
    public GameObject uiMessage2; 


    private float startY;  
    private float startRotationX; 
    public bool movingUp = false;

    public AudioClip chairSound;
    public AudioClip officeSound;
    private AudioSource audioSource;

    void Start()
    {
        startY = transform.position.y; 
        startRotationX = transform.eulerAngles.x;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movingUp = !movingUp;
            //PlayAudio(chairSound);
            if (uiMessage1 != null)
            {
                uiMessage1.SetActive(!movingUp); 
            }
            if (uiMessage1 != null)
            {
                uiMessage2.SetActive(movingUp); 
            }
        }

        
        float targetPositionY = movingUp ? targetY : startY;
        float targetRotationX = movingUp ? rotationX : startRotationX;

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Lerp(transform.position.y, targetPositionY, Time.deltaTime * speed),
            transform.position.z
        );

        
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(targetRotationX, transform.eulerAngles.y, transform.eulerAngles.z),
            Time.deltaTime * speed
        );
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