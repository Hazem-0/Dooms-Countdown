using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float movementSpeed = 20f;
    public float sprint = 5f;
    public float gravity = 100f;
    public float jump = 40f;

    float verticalSpeed = 0f;
    int jumpCounter = 0;
    



    PlayerStatus status;
    CharacterController characterController;
    Animator animator ;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        status = GetComponent<PlayerStatus>();
    }
    // Update is called once per frame
    void Update()
    {
        float horizontalDir = Input.GetAxis("Horizontal");
        float verticalDir = Input.GetAxis("Vertical");
        bool needSprint = Input.GetKey(KeyCode.E);


        bool isMove = verticalDir != 0 || horizontalDir != 0;
        Vector3 motion = new(horizontalDir, 0, verticalDir);
        float moveWithAnimation = characterController.isGrounded ? (Mathf.Clamp(motion.magnitude, 0, 0.5f) + (needSprint ? 0.5f : 0f)) : 0;



        animator.SetFloat("Speed", moveWithAnimation);

        // apply sprint 
        Sprint(isMove, needSprint);
        // prevent double jump, Need first to collesion with ground

        Jump();

        // Move & rotation with camera
        Move(motion);
    }

    private void  Move(Vector3 motion)
    {
        motion = RotationWithCamera(motion);
        motion = new(motion.x * movementSpeed * sprint, verticalSpeed, motion.z * movementSpeed * sprint);

        characterController.Move(Time.deltaTime * motion);
    }

    private Vector3 RotationWithCamera(Vector3 motion)
    {
        if (motion.magnitude > 0.1)
        {

            float angle = Mathf.Atan2(motion.x, motion.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, angle, 0);

        }
        motion = Camera.main.transform.TransformDirection(motion);
        return motion;
    }

    void Jump()
    {
        int maxJump = 2; 

        if (characterController.isGrounded)
        {
            jumpCounter = 0;
            animator.SetBool("IsJump", false);
            animator.SetBool("IsDoubleJump", false);

            if (Input.GetKeyDown(KeyCode.Space))
            { 
                jumpCounter++;
                verticalSpeed = jump;
                animator.SetBool("IsJump", true);
            }
           
        }
        else
        {

            if (Input.GetKeyDown(KeyCode.Space) &&( jumpCounter < maxJump)) {
                jumpCounter++;
                verticalSpeed += jump;

                animator.SetBool("IsDoubleJump", true);

            }
            verticalSpeed -= (gravity * Time.deltaTime);
        }
    }
    void Sprint(bool isMove , bool needSprint)
    {
        if (needSprint && isMove && characterController.isGrounded)
        {
            sprint = 5f;
        }
        else
        {
            sprint = 1f;
        }
    }
   
    
    
    
    //bool IsGround()
    //{
    //    return Physics.CheckSphere(groundChecker.position, 0.1f , groundLayer);
    //}
}
/**/