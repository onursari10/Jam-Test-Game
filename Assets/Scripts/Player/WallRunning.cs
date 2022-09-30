using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    public Transform playerCam;

    public Transform orientation;

    public Rigidbody rb;

    public GameObject player;

    public LayerMask whatIsWall;

    public float wallRunForce, wallRunTime, maxWallSpeed;

    public bool isWallRight, isWallLeft;

    public bool isWallRunning;

    public float maxWallRunCameraTilt, wallRunCameraTilt;

    void Start()
    {

    }


    void Update()
    {
        WallRunInput();
        CheckForWall();
    }

    private void WallRunInput()
    {
        if (Input.GetKey(KeyCode.D) && isWallRight)
        {
            StartWallRun();
        }

        if (Input.GetKey(KeyCode.A) && isWallLeft)
        {
            StartWallRun();
        }
    }

    private void StartWallRun()
    {
        rb.useGravity = false;
        isWallRunning = true;

        if (rb.velocity.magnitude <= maxWallSpeed)
        {
            rb.AddForce(orientation.forward * wallRunForce * Time.deltaTime);

            if (isWallRight)
            {
                rb.AddForce(orientation.right * wallRunForce / 5 * Time.deltaTime);
            }

            else if (isWallLeft)
            {
                rb.AddForce(-orientation.right * wallRunForce / 5 * Time.deltaTime);
            }


        }
    }

    private void StopWallRun()
    {
        rb.useGravity = true;
        isWallRunning = false;
    }

    private void CheckForWall()
    {
        isWallRight = Physics.Raycast(transform.position, orientation.right, 1f, whatIsWall);
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, whatIsWall);

        if (!isWallLeft && !isWallRight)
        {
            StopWallRun();
        }

    }


}
