using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public ThirdController thirdController;
    void Start()
    {
        thirdController = GetComponent<ThirdController>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", thirdController.axisHorizontal);
        animator.SetFloat("Vertical", thirdController.axisVertical);

    }
}
