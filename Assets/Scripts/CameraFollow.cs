using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 0.125f;
    public float minX = -5f; 
    public float maxX = 7f;  
    public float minZ = -1f; 
    public float maxZ = -10f;  


    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(
                Mathf.Clamp(target.position.x, minX, maxX),
                target.position.y + 41f, /*target.position.z - 20*/ transform.position.z);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
