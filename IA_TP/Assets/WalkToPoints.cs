using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToPoints : MonoBehaviour
{
    public GameObject StartMove;
    public GameObject EndMove;
    private bool movement;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        StartMove.transform.parent = null;
        EndMove.transform.parent = null;

        if (!movement)
        {
            transform.position = StartMove.transform.position;
        }
        else
        {
            transform.position = EndMove.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!movement)
        {
           transform.position = Vector3.MoveTowards(transform.position, EndMove.transform.position, speed * Time.deltaTime);
           
            if (transform.position == EndMove.transform.position)
            {
                
                movement = true;
                
            }
        }

        if (movement)
        {
            transform.position = Vector3.MoveTowards(transform.position, StartMove.transform.position, speed * Time.deltaTime);
            if (transform.position == StartMove.transform.position)
            {
                
                movement = false;
               
            }
        }
    }
}
