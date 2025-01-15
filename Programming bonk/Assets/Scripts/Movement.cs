using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private float walkspeed = 6;
    void Start()
    {
        
    }
    void Update()
    {
        // While holding A down
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * walkspeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * walkspeed * Time.deltaTime);
        }

        transform.Translate(Vector3.forward * walkspeed * Time.deltaTime);
    }
}
