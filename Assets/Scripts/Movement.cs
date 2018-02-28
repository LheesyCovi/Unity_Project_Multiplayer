using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMoter))]
[RequireComponent(typeof(ConfigurableJoint))]
public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public float lookSensitivity;
    private float thrusterForce = 1000;
    [Header("joint options:")]
    public float jointSpring = 20;
    public float jointMaxForce = 40;

    private PlayerMoter moter;
    private ConfigurableJoint joint;
    void Start()
    {
        moter = GetComponent<PlayerMoter>();
        joint = GetComponent<ConfigurableJoint>();
        SetJointSettings(jointSpring);
    }

    void FixedUpdate()
    {
        // Udregn og send spillerens bevægelse
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 MoveHorizon = transform.right * moveX;
        Vector3 MoveVertical = transform.forward * moveY;
        Vector3 movement = (MoveHorizon + MoveVertical).normalized * moveSpeed;
        moter.Move(movement);

        // Udregn og send rotationen omkring spilleren y-akse
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0, yRot, 0) * lookSensitivity;
        moter.Rotate(rotation);

        // Udregn og send rotationen omkring kameraets x-akse
        float xRot = Input.GetAxisRaw("Mouse Y");
        float camRotationX = xRot * lookSensitivity;
        moter.CameraRotation(camRotationX);

        // Udregn og send opadgående kraft vector3
        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0);
        }
        else
        {
            SetJointSettings(jointSpring);
        }
        moter.ThrustMove(_thrusterForce);
    }
    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive { positionSpring = _jointSpring, maximumForce = jointMaxForce };
    }
}
