using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 camOffset;
    public float smoothing;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        camOffset = transform.position - playerTransform.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = playerTransform.position + camOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
    }
}
