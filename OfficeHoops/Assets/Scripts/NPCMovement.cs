using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCMovement : MonoBehaviour
{
    public Transform targetPoint;
    public Transform LosingPoint;
    private NavMeshAgent agent;
    public bool IsBoss;
    public bool Where2Go;
    public float RunningSpeed;
    public Animator animator;
    //public Canvas JumpScare;
    public GameObject CanvasPrefab;
    private bool JumpScareSpawned;

    public AudioClip angry;
    private AudioSource audioSource;

    void Start()
    {
       targetPoint = GameObject.Find("FinalPoint").transform;
        LosingPoint = GameObject.Find("LosingPoint").transform;
        animator =  GetComponent<Animator>();  
        agent = GetComponent<NavMeshAgent>();
        Where2Go = true;
        JumpScareSpawned = false;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void MoveToTarget()
    {
        if (targetPoint != null)
        {
            agent.SetDestination(targetPoint.position);
        }
    }

    void MoveToEnd()
    {
        if (LosingPoint != null)
        {
            agent.SetDestination(LosingPoint.position);
        }
    }
    private void Update()
    {

        if (IsBoss)
        {
            if (Where2Go == true)
            {
                MoveToTarget();
            }
            else if (Where2Go == false)
            {
                MoveToEnd();
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
                {
                    StopAudio(angry);
                    agent.speed = 0;
                    agent.acceleration = 0;
                    //JumpScare.gameObject.SetActive(true);
                    if (JumpScareSpawned == false)
                    {
                        
                        Instantiate(CanvasPrefab);
                        JumpScareSpawned = true;
                    }
                    Invoke("GoToMenu", 1f);
                }

            }

           
        }
        else
        {
            MoveToTarget() ;
        }
    }


    private void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ChangeDestination()
    {
        if (IsBoss)
        {
            PlayAudio(angry);
            animator.SetBool("IsRunning", true);
            agent.speed = RunningSpeed;
            Where2Go = false;
            
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

    public void StopAudio(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        TimerController timer = Object.FindAnyObjectByType<TimerController>();

        if (timer == null)
        {
            Debug.LogWarning("No se encontró TimerController en la escena.");
            return;
        }

        if (collision.gameObject.CompareTag("Ball"))
        {
            timer.AddTime(10f);
            Debug.Log("NPC golpeado por pelota. +10 segundos.");
        }
        else if (collision.gameObject.CompareTag("SodaCan"))
        {
            timer.AddTime(30f);
            Debug.Log("NPC golpeado por lata. +60 segundos.");
        }
    }
}
