using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoter : MonoBehaviour
{

    public Camera cam;
    private Vector3 movement = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 thrusterForce = Vector3.zero;
    private float camRotationX = 0;
    private float currentCamRotation = 0;
    public float cameraRotationLimit = 85;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 _movement)
    {
        movement = _movement;
    }
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    public void ThrustMove(Vector3 _thrusterForcer)
    {
        thrusterForce = _thrusterForcer;
    }
    public void CameraRotation(float _camRotationX)
    {
        camRotationX = _camRotationX;
    }

    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();

    }

    void PerformMovement()
    {
        if (movement != Vector3.zero)
        {
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }
        if (thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            // Set roration og clamper det.
            currentCamRotation -= camRotationX;
            currentCamRotation = Mathf.Clamp(currentCamRotation, -cameraRotationLimit, cameraRotationLimit);

            cam.transform.localEulerAngles = new Vector3(currentCamRotation, 0, 0);
        }
    }
}