using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMock : MonoBehaviour
{
    private const float angularVelocity = 3.5f;
    private Vector3 previousPosition;

    private void Awake()
    {
        previousPosition = new Vector3(transform.position.x -1, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (previousPosition != transform.position)
        {
            Vector3 direction = (transform.position - previousPosition).normalized;

            float angle = Vector3.SignedAngle(transform.right, direction, Vector3.forward) - 90;

            transform.Rotate(angle * Time.deltaTime * angularVelocity * Vector3.forward);
        }

        previousPosition = transform.position;
    }
}
