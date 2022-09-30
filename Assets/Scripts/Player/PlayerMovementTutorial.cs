using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerMovementTutorial : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float firstMoveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    private Vector3 normalVector = Vector3.up;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public Transform orientation;


    [Header("Mecanic")]
    public int doorKey;
    public GameObject WallRunning;
    public float maxWallRunCameraTilt, wallRunCameraTilt;

    public Vector3 playerScale;
    public Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    public float slideForce = 400;


    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public GameObject gameoverPanel;
    public Text GameOverText;

    public string gameOverText;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;


        readyToJump = true;

        GameOverText.text = gameOverText;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();
        Look();

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            StartCrouch();
        if (Input.GetKeyUp(KeyCode.LeftShift))
            StopCrouch();


        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }


    }

    private void MovePlayer()
    {

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;


        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);


        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);


        if (WallRunning.GetComponent<WallRunning>().isWallRunning)
        {
            readyToJump = false;

            if (WallRunning.GetComponent<WallRunning>().isWallLeft && !Input.GetKey(KeyCode.D) || WallRunning.GetComponent<WallRunning>().isWallRight && !Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector2.up * jumpForce * 1.5f);
                rb.AddForce(normalVector * jumpForce * 0.5f);
            }


            if (WallRunning.GetComponent<WallRunning>().isWallRight || WallRunning.GetComponent<WallRunning>().isWallLeft && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) rb.AddForce(-orientation.up * jumpForce * 1f);
            if (WallRunning.GetComponent<WallRunning>().isWallRight && Input.GetKey(KeyCode.A)) rb.AddForce(-orientation.right * jumpForce * 3.2f);
            if (WallRunning.GetComponent<WallRunning>().isWallLeft && Input.GetKey(KeyCode.D)) rb.AddForce(orientation.right * jumpForce * 3.2f);


            rb.AddForce(orientation.forward * jumpForce * 1f);


            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Lava")
        {
            Time.timeScale = 0;
            gameoverPanel.SetActive(true);
        }
    }

    private void Look()
    {
        if (Math.Abs(wallRunCameraTilt) < maxWallRunCameraTilt && WallRunning.GetComponent<WallRunning>().isWallRunning && WallRunning.GetComponent<WallRunning>().isWallRight)
            wallRunCameraTilt += Time.deltaTime * maxWallRunCameraTilt * 2;
        if (Math.Abs(wallRunCameraTilt) < maxWallRunCameraTilt && WallRunning.GetComponent<WallRunning>().isWallRunning && WallRunning.GetComponent<WallRunning>().isWallLeft)
            wallRunCameraTilt -= Time.deltaTime * maxWallRunCameraTilt * 2;

        //Tilts camera back again
        if (wallRunCameraTilt > 0 && !WallRunning.GetComponent<WallRunning>().isWallRight && !WallRunning.GetComponent<WallRunning>().isWallLeft)
            wallRunCameraTilt -= Time.deltaTime * maxWallRunCameraTilt * 2;
        if (wallRunCameraTilt < 0 && !WallRunning.GetComponent<WallRunning>().isWallRight && !WallRunning.GetComponent<WallRunning>().isWallLeft)
            wallRunCameraTilt += Time.deltaTime * maxWallRunCameraTilt * 2;
    }

    private void StartCrouch()
    {
        readyToJump = false;
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rb.velocity.magnitude > 0.5f)
        {

            if (grounded)
            {
                rb.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    private void StopCrouch()
    {
        if (grounded)
        {
            readyToJump = true;
        }
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

}