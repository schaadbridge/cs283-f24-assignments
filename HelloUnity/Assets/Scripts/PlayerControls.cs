using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    public float visionSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // W or up -- move forward
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            transform.position += (Time.deltaTime * movementSpeed * transform.forward);
        }
        // A or left -- turn left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            Vector3 newRotation = Time.deltaTime * visionSpeed * transform.up;
            transform.eulerAngles -= newRotation;
        }
        // S or down -- move back
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            transform.position -= (Time.deltaTime * movementSpeed * transform.forward);
        }
        // D or right -- turn right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            Vector3 newRotation = Time.deltaTime * visionSpeed * transform.up;
            transform.eulerAngles += newRotation;
        }
    }
}
