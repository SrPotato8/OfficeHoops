using Unity.VisualScripting;
using UnityEngine;

public class BossZoneController : MonoBehaviour
{
    public CameraScript CameraScript;
    
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
        if (CameraScript.movingUp)
        {
            
            NPCMovement nPCMovement = other.GetComponent<NPCMovement>();
            nPCMovement.ChangeDestination();
        }
    }
    
   
}
