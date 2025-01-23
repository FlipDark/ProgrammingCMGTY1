using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleRotation : MonoBehaviour
{
    private Quaternion originalRotation;
    [Range(0,10)] public float RotationSpeed = 2;
    public Vector3 RotationRange; 

    void Start()
    {
        originalRotation = transform.rotation; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationOffset = RotationRange * Mathf.Sin(Time.time * RotationSpeed * Mathf.PI);

        transform.rotation = originalRotation * Quaternion.Euler(rotationOffset);
    }
}
