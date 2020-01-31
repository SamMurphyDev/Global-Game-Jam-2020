using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10;
    public float rotationSpeed = 1;
    public Rigidbody rb;

    public Vector3 movemet = new Vector3();

    // Start is called before the first frame update 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movemet.y = Input.GetAxisRaw("Horizontal");
        movemet.z = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() 
    {
        transform.Rotate(new Vector3(0, movemet.y * rotationSpeed * Time.fixedDeltaTime, 0));
        rb.AddForce(transform.forward * movemet.z * moveSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
    }
}
