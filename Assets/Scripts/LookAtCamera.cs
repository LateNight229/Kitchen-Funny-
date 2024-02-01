using UnityEngine;

public class LookAtCamera : MonoBehaviour
{   
    private Camera _camera;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();         
    }
    private void LateUpdate()
    {
        //transform.rotation =  _camera.transform.rotation;
        transform.forward = Camera.main.transform.forward;
        //Vector3 cameraDirection = _camera.transform.position - transform.position;
        //Quaternion lookRotation = Quaternion.LookRotation(cameraDirection, Vector3.up);
        //transform.rotation = lookRotation;
    }
}
