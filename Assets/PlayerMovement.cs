using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string horizontalInputAxis = "Horizontal P1";
    public string verticalInputAxis = "Vertical P1";

    public float moveSpeed = 20;

    // The amount of torque to apply to keep the player standing upright.
    public float uprightingTorque = 75.0f;
    // The amount of torque to apply while moving to make the player face in the direction they're moving.
    public float bearingTorque = 50.0f;

    public Rigidbody rb;

    public Vector3 movement = new Vector3();

    // Start is called before the first frame update 
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw(horizontalInputAxis);
        movement.z = Input.GetAxisRaw(verticalInputAxis) * -1;
    }

    void FixedUpdate()
    {
        MoveCharacter();

        // Keep the player standing upright.
        Quaternion rot = Quaternion.FromToRotation(transform.up, Vector3.up);
        rb.AddTorque(new Vector3(rot.x, rot.y, rot.z) * uprightingTorque);
    }

    // Move the player in response to user input.
    private void MoveCharacter()
    {
        // Transform the input to camera-relative.
        Vector3 movementCameraRelative = movement;
        Camera.main.transform.TransformDirection(movementCameraRelative);

        // Move the player.
        Vector3 impulse = movementCameraRelative * moveSpeed * Time.fixedDeltaTime;
        rb.AddForce(impulse, ForceMode.Impulse);

        // Turn the player to face the direction they're moving in.
        Quaternion rot = Quaternion.FromToRotation(transform.forward, movementCameraRelative);
        rb.AddTorque(new Vector3(rot.x, rot.y, rot.z) * movementCameraRelative.magnitude * bearingTorque);
    }
}
