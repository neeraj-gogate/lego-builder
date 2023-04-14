using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public float groundDrag;
    public float airDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    public float flyingForce;
    public float flyCooldown;
    public bool holdingJump;
    public bool isFlying;

    public float playerHeight;
    public LayerMask ground;
    public bool grounded;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode flyDownKey = KeyCode.LeftShift;
    public KeyCode stopFlying = KeyCode.F;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    private void Start()
    {
        rb =  GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        holdingJump = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isFlying)
        {
            Fly();
        }
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        PlayerInput();
        LimitSpeed();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else if (isFlying){
            rb.drag = airDrag;
        }
        else{
            rb.drag = 0;
        }
        if (isFlying)
        {
            Fly();
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyUp(jumpKey) && holdingJump){
            holdingJump = false;
        }
        if (Input.GetKey(jumpKey))
        {
            if (readyToJump && grounded)
            {
                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
            else if (!holdingJump){
                if ((readyToJump != grounded) || (!readyToJump)){
                    isFlying = true;
                    FlyUp();
                }
            }
            else if (isFlying)
            {
                FlyUp();
            }
        }
        if (Input.GetKey(flyDownKey))
        {
            if (isFlying)
            {
                FlyDown();
            }
        }
        if (Input.GetKey(stopFlying))
        {
            if (isFlying)
            {
                isFlying = false;
                rb.AddForce(transform.up * -flyingForce, ForceMode.Impulse);
            }
        }
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
        {
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }
    private void LimitSpeed(){
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        holdingJump = true;
    }
    private void Fly(){
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }
    private void FlyUp()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * flyingForce, ForceMode.Impulse);
    }
    private void FlyDown()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * -flyingForce, ForceMode.Impulse);
        if (grounded)
        {
            isFlying = false;
        }
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
