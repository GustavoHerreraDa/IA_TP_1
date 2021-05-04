using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdController : MonoBehaviour
{
    private Vector3 moveDirection;
    private Vector3 lookAtPos = new Vector3();
    private Animator animator;

    private Player player;

    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    float rotationX = 0;

    public float axisVertical;
    public float axisHorizontal;
    void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        Movement();
        CameraControl();
    }

    void FixedUpdate()
    {
    }

    private void LookAtMousePointer()
    {
    }

    private void Movement()
    {
        axisHorizontal = Input.GetAxis("Horizontal");
        axisVertical = Input.GetAxis("Vertical");


        transform.position += Time.deltaTime * player.speedMovement *
                                                (new Vector3(Camera.main.transform.forward.x, transform.forward.y, Camera.main.transform.forward.z) * axisVertical +
                                                new Vector3(Camera.main.transform.right.x, transform.forward.y, Camera.main.transform.right.z) * axisHorizontal);

        transform.forward = Vector3.Lerp(transform.forward, new Vector3(Camera.main.transform.forward.x, transform.forward.y, Camera.main.transform.forward.z),
                                       Mathf.Abs(Input.GetAxis("Vertical") + Input.GetAxis("Horizontal") * Time.deltaTime * player.rotationSpeed)); 

    }
    private void CameraControl()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        Debug.Log(rotationX.ToString());
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void Death()
    {
        animator.SetBool("IsDeath", true);
    }
}
