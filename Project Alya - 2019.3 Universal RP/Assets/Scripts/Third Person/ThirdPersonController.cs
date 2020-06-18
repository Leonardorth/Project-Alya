using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    #region Movement
    [Header("Movement")]
    public float walkSpeed = 3;
    public float runSpeed = 6;
    public float gravity = -12;
    public float jumpHeight = 1;

    [Range(0,1)]
    public float airControlPercent = 1; //if 1 then full control in air jumping

    public float turnSmoothTime = 0.05f; //rotationSpeed
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f; //blend strength between states (idle, walk, run)
    float speedSmoothVelocty;
    float currentSpeed;
    float velocityY; //players height velocity

    Animator animator;
    Transform cameraT;
    CharacterController controller;
    #endregion

    void Start()
    {
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();

    }

    
    void Update()
    {
        // input section
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 
        Vector2 inputDir = input.normalized; 
        bool running = Input.GetKey(KeyCode.LeftShift);

        Move(inputDir, running);

        if (Input.GetKeyDown (KeyCode.Space))
        {
            Jump();
        }


        // animator section
        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * 0.5f);
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

        if (velocityY < -6 && controller.isGrounded == false)
        {
            animator.SetBool("isFalling", true);
        }


    }

    void Move(Vector2 inputDir, bool running)
    {
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

        
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocty, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;

        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
        
    }

    void Jump()
    {
        if(controller.isGrounded == true) 
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
            animator.SetBool("isJumping", true);
        }
        
    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if(controller.isGrounded)
        {
            return smoothTime;
        }

        if(airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }
}
