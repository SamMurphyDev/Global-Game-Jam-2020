using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 20;

    // The amount of torque to apply to keep the player standing upright.
    public float uprightingTorque = 75.0f;
    // The amount of torque to apply while moving to make the player face in the direction they're moving.
    public float bearingTorque = 50.0f;

    public Rigidbody rb;

    public Vector3 movement = new Vector3();

    public string horizontalInputAxis;
    public string verticalInputAxis;

    public string interactButtonAxis;
    public GameObject tagHolder;

    private LevelManagement levelManagement;

    public float playerKillY = -4;

    // Start is called before the first frame update 
    void Start()
    {
        if(rb == null) {
            rb = GetComponent<Rigidbody>();
        }

        levelManagement = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelManagement>();
    }

    public void setChildTag(string tag) {
        tagHolder.tag = tag;

        char[] childTag = tagHolder.tag.ToCharArray();
        char playerNum = childTag[childTag.Length - 1];

        horizontalInputAxis = "Horizontal P" + playerNum;
        Debug.Log("horizontalInputAxis: " + horizontalInputAxis);

        verticalInputAxis = "Vertical P" + playerNum;
        Debug.Log("verticalInputAxis: " + verticalInputAxis);

        interactButtonAxis = "X P" + playerNum;
        Debug.Log("interactButtonAxis: " + interactButtonAxis);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = -Input.GetAxisRaw(verticalInputAxis);
        movement.z = -Input.GetAxisRaw(horizontalInputAxis);

        if(transform.position.y <= playerKillY) {
            rb.velocity = Vector3.zero;
            transform.position = levelManagement.GetGameObjectSpawnLocation();
        }
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
        //Camera.main.transform.TransformDirection(movementCameraRelative);

        // Move the player.
        Vector3 impulse = movementCameraRelative * moveSpeed * Time.fixedDeltaTime;
        rb.AddForce(impulse, ForceMode.Impulse);

        // Turn the player to face the direction they're moving in.
        Quaternion rot = Quaternion.FromToRotation(transform.forward, movementCameraRelative);
        rb.AddTorque(new Vector3(rot.x, rot.y, rot.z) * movementCameraRelative.magnitude * bearingTorque);
    }
}
