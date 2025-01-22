using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float sidewaySpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Collider col;

    private float distToGround;

    // bools
    private bool pressedA = false;
    private bool pressedD = false;
    private bool pressedSpace = false;

    private void Start()
    {
        distToGround = col.bounds.extents.y;
    }

    void Update()
    {
        // Input:
        GetInput();
    }

    private void FixedUpdate()
    {
        // physics
        DoPhysics();

        IsGrounded();

        // reset input
        ResetInput();
    }
    private void DoPhysics()
    {
        //jumping
        if (pressedSpace && IsGrounded()) rb.AddForce(transform.up * jumpForce);

        //move left
        if (pressedA) rb.AddForce(-transform.right * sidewaySpeed);

        //move right
        if (pressedD) rb.AddForce(transform.right * sidewaySpeed);

        //constant movement forward
        if (rb.velocity.z < maxSpeed)
        {
            rb.AddForce(transform.forward * forwardSpeed);
            Debug.Log(rb.velocity.z);
        }
    }
    private void GetInput()
    {
        if (Input.GetKey(KeyCode.A)) pressedA = true;
        if (Input.GetKey(KeyCode.D)) pressedD = true;
        if (Input.GetKeyDown(KeyCode.Space)) pressedSpace = true;
    }
    private void ResetInput()
    {
        pressedA = false;
        pressedD = false;
        pressedSpace = false;
    }


    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0, 1);
    }
}
