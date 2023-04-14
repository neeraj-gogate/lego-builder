using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool colorMode;
    public KeyCode colorToggle;

    public float moveSpeed;

    public float groundDrag;
    public float airDrag;
    public float airMultiplier;

    public float flyingForce;

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
    }

    // Update is called once per frame
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        PlayerInput();
        LimitSpeed();
        rb.drag = groundDrag;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey))
        {
            FlyUp();
        }
        if (Input.GetKey(flyDownKey))
        {
            FlyDown();
        }
        if (Input.GetKey(colorToggle)){
            colorMode = !colorMode;
        }
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    private void LimitSpeed(){
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
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
        rb.AddForce(transform.up * -flyingForce*10, ForceMode.Impulse);
    }
}
