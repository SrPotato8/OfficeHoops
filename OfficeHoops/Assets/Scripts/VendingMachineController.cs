using UnityEngine;

public class VendingMachineController : MonoBehaviour
{
    public GameObject sodaPrefab;
    public Transform startPoint;
    public float force = 10f;
    public float launchDelay = 2f;

    private Transform endPoint;
    private AudioSource audioSource;

    private bool isWaiting = false;
    private bool hasLaunched = false;
    private float launchTimer = 0f;
    private Vector3 launchDirection;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No se encontró AudioSource en la máquina expendedora.");
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        GameObject destinationObj = GameObject.FindGameObjectWithTag("SodaObjective");

        if (destinationObj != null)
        {
            endPoint = destinationObj.transform;
        }
        else if (playerObj != null)
        {
            endPoint = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con los tags esperados.");
        }
    }

    private void Update()
    {
        if (isWaiting)
        {
            launchTimer -= Time.deltaTime;
            Debug.Log("Esperando para lanzar, tiempo restante: " + launchTimer);

            if (launchTimer <= 0f)
            {
                Debug.Log("Lanzando lata");
                LaunchSoda();
                isWaiting = false;
                Invoke(nameof(ResetLaunch), 1.1f);
            }
        }
    }

    private void ResetLaunch()
    {
        hasLaunched = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !isWaiting && !hasLaunched)
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }

            if (endPoint != null && startPoint != null)
            {
                launchDirection = (endPoint.position - startPoint.position).normalized;
                isWaiting = true;
                hasLaunched = true;
                launchTimer = launchDelay;
            }
        }
    }

    private void LaunchSoda()
    {
        GameObject soda = Instantiate(sodaPrefab, startPoint.position, Quaternion.identity);

        Rigidbody rb = soda.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(launchDirection * force, ForceMode.Impulse);
        }
    }
}

