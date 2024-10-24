using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flythrough : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    public float visionSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // W or up -- forward
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            transform.position += (Time.deltaTime * movementSpeed * transform.forward);
        }
        // A or left -- left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            transform.position -= (Time.deltaTime * movementSpeed * transform.right);
        }
        // S or down -- back
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            transform.position -= (Time.deltaTime * movementSpeed * transform.forward);
        }
        // D or right -- right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            transform.position += (Time.deltaTime * movementSpeed * transform.right);
        }
        // rotate according to mouse direction
        Vector2 mouseDelta = visionSpeed * new Vector2(Input.GetAxis("Mouse X"), - Input.GetAxis("Mouse Y"));
        Quaternion rotation = transform.rotation;
        Quaternion horiz = Quaternion.AngleAxis(mouseDelta.x, Vector3.up);
        Quaternion vert = Quaternion.AngleAxis(mouseDelta.y, Vector3.right);
        transform.rotation = horiz * rotation * vert;
    }
}
