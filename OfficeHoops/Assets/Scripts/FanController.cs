using UnityEngine;
using UnityEngine.UIElements;

public class FanController : MonoBehaviour
{

    public Vector3 airDirection = Vector3.forward; // Dirección del viento
    public float forceStrength = 10f; // Intensidad de la corriente


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Debug.Log(rb);
        if (rb != null)
        {
            rb.AddForce(airDirection.normalized * forceStrength, ForceMode.Impulse);
        }
    }
}
