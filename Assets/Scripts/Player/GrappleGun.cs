using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrappleGun : MonoBehaviour
{
    [Header("Grapple Gun")]

    private LineRenderer lineRenderer;
    private Vector3 GrapplePoint;
    public LayerMask whatIsGround;
    public Transform gunTip, camera, player;
    public float maxDistance = 100f;
    private SpringJoint springJoint;
    public bool StartGrappling = false;
    public float spring, damper, massScale;
    public GameObject MuzzleFlash;

    Rigidbody grabbedRb;


    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {

    }


    void Update()
    {


        if (Input.GetButtonDown("Fire2") && grabbedRb == null)
        {
            StartGrapple();
            MuzzleFlash.SetActive(true);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            StopGrapple();
            MuzzleFlash.SetActive(false);
        }

    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(origin: camera.position, direction: camera.forward, out hit, maxDistance, whatIsGround))
        {
            GrapplePoint = hit.point;
            springJoint = player.gameObject.AddComponent<SpringJoint>();
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.connectedAnchor = GrapplePoint;

            float distanceFromPoint = Vector3.Distance(a: player.position, b: GrapplePoint);
            springJoint.maxDistance = distanceFromPoint * 0.8f;
            springJoint.minDistance = distanceFromPoint * 0.25f;

            springJoint.spring = spring;
            springJoint.damper = damper;
            springJoint.massScale = massScale;

            lineRenderer.positionCount = 2;

        }
    }

    void DrawRope()
    {
        if (!springJoint) return;
        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, GrapplePoint);
    }

    void StopGrapple()
    {
        lineRenderer.positionCount = 0;
        Destroy(springJoint);
    }

    public bool isGrappling()
    {
        return springJoint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return GrapplePoint;
    }
}
