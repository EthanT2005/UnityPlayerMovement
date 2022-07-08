using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    PlayerMovement basicMovementScript;

    [Header("Movment")]
    public float speed = 11f;
    public float walkingSpeed = 22f;
    public float runningSpeed = 44f;
    //public float speedMultiplier = 5f;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public KeyCode jumpKey = KeyCode.Space;
    
    [Header("Gravity")]
    public float gravity = -9.81f;

    [Header("Ground Varibles")]
    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;

    [Header("Roof Varibles")]
    public Transform roofCheck;
    public float roofDistance = .4f;
    public LayerMask roofMask;

    [Header("Crouching")]
    public float crouchSpeed;
    public float uncrouchYScale;
    public float crouchYScale;
    private float startYScale;
    public KeyCode crouchKey = KeyCode.LeftControl;
    
    Vector3 velocity;
    bool isGrounded;
    bool isRoof;

    void Start() 
    {

        basicMovementScript = GetComponent<PlayerMovement>(); 
        startYScale = transform.localScale.y;  

    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isRoof = Physics.CheckSphere(roofCheck.position, roofDistance, roofMask);

        if(isGrounded && velocity.y < 0){

            velocity.y = -2f;

        }

        //Jumping
        if(Input.GetKeyDown(jumpKey) && isGrounded)
        {

            velocity.y = jumpForce;

        }

        //Sprinting
        if(Input.GetKeyDown(sprintKey)){

            basicMovementScript.speed = runningSpeed;

        }
        else if(Input.GetKeyUp(sprintKey)){

            basicMovementScript.speed = walkingSpeed;

        }
        

        //Crouching
        if(Input.GetKeyDown(crouchKey))
        {

            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);

        }
        else if(Input.GetKeyUp(crouchKey))
        {

            transform.localScale = new Vector3(transform.localScale.x, uncrouchYScale, transform.localScale.z);

        }

        //crouching speed
        if(Input.GetKeyDown(crouchKey))
        {

            basicMovementScript.speed = crouchSpeed;

        }
        // else if(localScale == crouchYScale && Input.GetKeyDown(sprintKey))
        // {

        //     basicMovementScript.speed = crouchSpeed;

        // }
        else if(Input.GetKeyUp(crouchKey))
        {

            basicMovementScript.speed = walkingSpeed;

        }

        //roof detection
        if(isRoof && Input.GetKeyUp(crouchKey))
        {

            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            basicMovementScript.speed = crouchSpeed;

        }
        else if(Input.GetKeyUp(crouchKey))
        {
            
            transform.localScale = new Vector3(transform.localScale.x, uncrouchYScale, transform.localScale.z);

        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }


}

