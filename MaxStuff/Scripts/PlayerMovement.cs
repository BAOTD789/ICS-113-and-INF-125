using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller_1;

    public float speed = 12f;
    public float gravity = -35f;
    public float jumpHeight = 3f;
    public float sprintModifier = 1.5f;

    public Transform groundCheck;
    public float groundDistance = 0.6f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool isSprinting;

    // Start is called before the first frame update
    void Start()
    {
        isSprinting = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //feetLock
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -4f;
        }
        
        //WASD
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * X) + (transform.forward * Z);

        //shift
        if (Input.GetButton("Sprint") && !isGrounded && !isSprinting)
        {
            controller_1.Move(move * speed * Time.deltaTime);
        } else if (Input.GetButton("Sprint"))
        {
            controller_1.Move(move * speed * sprintModifier * Time.deltaTime);
            isSprinting = true;
        } else
        {
            isSprinting = false;
            controller_1.Move(move * speed * Time.deltaTime);
        }
        
        //space
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller_1.Move(velocity * Time.deltaTime); // fall
    }
}
