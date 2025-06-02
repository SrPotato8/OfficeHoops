using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakePower = 0.02f;

    private float shakeDuration = 0f;
    private Vector3 shakeOffset = Vector3.zero;
    private Vector2 initialXZ;  
    private void Start()
    {
        initialXZ = new Vector2(transform.position.x, transform.position.z);
    }

    private void LateUpdate()
    {
        if (shakeDuration > 0)
        {
            shakeOffset = Random.insideUnitSphere * shakePower;
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeOffset = Vector3.zero;
        }

        float currentY = transform.position.y;

        transform.position = new Vector3(
            initialXZ.x + shakeOffset.x,
            currentY,
            initialXZ.y + shakeOffset.z
        );
    }

    public void Shake(float duration)
    {
        shakeDuration = duration;

        initialXZ = new Vector2(transform.position.x, transform.position.z);
    }
}
