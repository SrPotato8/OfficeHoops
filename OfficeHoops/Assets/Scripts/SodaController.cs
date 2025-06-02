using UnityEngine;
using System.Collections;

public class SodaController : MonoBehaviour
{
    public float detectionRadius = 2f;

    public Transform targetPoint;
    private bool triggered = false;

    private bool isPlayerTarget = false;

    public AudioClip playerHitSound;
    public AudioClip glassSound;
    public AudioClip PeopleScreamSound; 

    private AudioSource audioSource;
    private AudioSource screamAudioSource; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No se encontró AudioSource en la máquina expendedora.");
        }

        // Añadir un segundo AudioSource para el grito
        screamAudioSource = gameObject.AddComponent<AudioSource>();

        GameObject destinationObj = GameObject.FindGameObjectWithTag("SodaObjective");
        if (destinationObj != null)
        {
            targetPoint = destinationObj.transform;
            isPlayerTarget = false;
        }
        else
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                targetPoint = playerObj.transform;
                isPlayerTarget = true;
            }
            else
            {
                Debug.LogWarning("La lata no encontró ningún objeto con los tags 'Destination' ni 'Player'.");
            }
        }
    }

    private void Update()
    {
        if (triggered || targetPoint == null) return;

        float distance = Vector3.Distance(transform.position, targetPoint.position);
        if (distance < detectionRadius)
        {
            if (isPlayerTarget)
            {
                if (audioSource != null && playerHitSound != null)
                {
                    audioSource.clip = playerHitSound;
                    audioSource.volume = 1f;
                    audioSource.spatialBlend = 0f;
                    audioSource.Play();
                }

                CameraShake shake = Camera.main.GetComponent<CameraShake>();
                if (shake != null)
                {
                    shake.Shake(3f);
                }

                CameraScript camControl = Camera.main.GetComponent<CameraScript>();
                if (camControl != null)
                {
                    camControl.movingUp = false;
                }
            }
            else
            {
                PlayDestinationSequence();
            }

            triggered = true;
            Destroy(gameObject, 30f);
        }
    }

    private void PlayDestinationSequence()
    {
        if (audioSource != null && glassSound != null)
        {
            audioSource.PlayOneShot(glassSound);
            StartCoroutine(PlayExtraAfterDelay(glassSound.length - 1f));
        }
    }

    private IEnumerator PlayExtraAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (screamAudioSource != null && PeopleScreamSound != null)
        {
            screamAudioSource.PlayOneShot(PeopleScreamSound);
        }
    }
}
