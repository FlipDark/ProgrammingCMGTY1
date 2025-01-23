using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private Vector3 originalPosition;

    [Range(0, 10)] public float movementSpeed = 2;

    public Vector3 movementRange;

    private void Start()
    {
        originalPosition = transform.position;
    }
    void Update()
    {
        Vector3 positionOffset = 
            transform.TransformVector(movementRange) *
            Mathf.Sin(Time.time * movementSpeed * Mathf.PI);

        Vector3 newPosition = originalPosition + positionOffset;

        newPosition.y = Mathf.Max(newPosition.y, 1f);

        transform.position = newPosition;
    }
}
