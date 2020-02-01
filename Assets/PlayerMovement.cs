using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10;
    public float rotationSpeed = 100;
    public float uprightingTorque = 75.0f;
    public Rigidbody rb;

    public Vector3 movement = new Vector3();

    // Start is called before the first frame update 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.y = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, movement.y * rotationSpeed * Time.fixedDeltaTime, 0));

        Vector3 forwardForce = transform.forward * movement.z * moveSpeed * Time.fixedDeltaTime;
        rb.AddForce(forwardForce, ForceMode.Impulse);

        // Keep the character standing upright.
        var rot = Quaternion.FromToRotation(transform.up, Vector3.up);
        rb.AddTorque(new Vector3(rot.x, rot.y, rot.z) * uprightingTorque);
    }
}
