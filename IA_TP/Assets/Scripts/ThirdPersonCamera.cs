using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform pivot;
    public float speed;
    public float rotSpeed;

    private Vector3 _offset;

    void Start()
    {
        _offset = (transform.position - pivot.position);
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void LateUpdate()
    {

        transform.position = Vector3.Lerp(transform.position, _offset + pivot.position, Time.deltaTime * speed);
        _offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotSpeed, Vector3.up) * _offset;
        
        transform.LookAt(pivot); 
    }
}
