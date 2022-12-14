using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //Player movement variables
    private CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    private Vector3 direction;
    private Vector3 moveDir;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    //Animation variables
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            //controller.Move(direction * speed * Time.deltaTime);
            Debug.Log(moveDir);
        }

        if(moveDir.x != 0 || moveDir.z != 0)
        {
            animator.SetBool("Running", true);
        }
        else if(moveDir.x == 0 || moveDir.z == 0)
        {
            animator.SetBool("Running", false);
        }

    }
}
