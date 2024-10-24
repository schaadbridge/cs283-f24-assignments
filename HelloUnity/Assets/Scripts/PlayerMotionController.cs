using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    public Animator m_Animator;
    public float movementSpeed;
    public float visionSpeed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Get the animator, which you attach to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // W or up -- move forward
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            movementSpeed = 0.1f;
            transform.position += Time.deltaTime * movementSpeed * transform.forward;
        }
        // A or left -- turn left
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            movementSpeed = -0.1f;
            Vector3 newRotation = Time.deltaTime * visionSpeed * transform.up;
            transform.eulerAngles -= newRotation;
        }
        // S or down -- move back
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            movementSpeed = 0.1f;
            transform.position -= Time.deltaTime * movementSpeed * transform.forward;
        }
        // D or right -- turn right
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            movementSpeed = -0.1f;
            Vector3 newRotation = Time.deltaTime * visionSpeed * transform.up;
            transform.eulerAngles += newRotation;
        }
        else {
            movementSpeed = -0.1f;
        }
        
        //Sends the value from the horizontal axis input to the animator. Change the settings in the
        //Animator to define when the character is walking or running
        m_Animator.SetFloat("movementSpeed", movementSpeed);
    }
}
