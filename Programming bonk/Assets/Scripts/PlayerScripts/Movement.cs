using System.Collections;
using UnityEngine;
using UnityEngineInternal;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float sidewaySpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Collider col;

    // bools
    private bool pressedA = false;
    private bool pressedD = false;
    private bool pressedSpace = false;

    // floats
    private float distToGround;
    private void Start()
    {
        distToGround = col.bounds.extents.y + 0.1f;
    }
    void Update()
    {
        // Input:
        GetInput();

        //Debug.Log(IsGrounded());
    }

    private void FixedUpdate()
    {
        // physics
        DoPhysics();

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
        if (rb.velocity.z < maxSpeed) rb.AddForce(transform.forward * forwardSpeed);
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, distToGround) && hit.collider.tag == "Ground") return true;
        else return false;
    }
}
