using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    public Animator m_Animator;
    public CharacterController m_controller;
    public float movementSpeed = 0.0f;
    public float visionSpeed = 10.0f;
    public bool collision = true;
    public int _score = 0;
    public Boolean _exploreMode = false;
    private UIManager _UIManager;
    // Start is called before the first frame update
    void Start()
    {
        //Get the animator, which you attach to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
        m_controller = gameObject.GetComponent<CharacterController>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_UIManager == null) {
            Debug.Log("UIManager is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // apply gravity
        Vector3 moveVector = movementSpeed * transform.forward;
        if (!m_controller.isGrounded)
        {
            moveVector += Physics.gravity;
        }
        // W or up -- move forward
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            movementSpeed = 1.0f;
            if (collision) {
                m_controller.Move(moveVector * Time.deltaTime);
            } else {
                transform.position += Time.deltaTime * movementSpeed * transform.forward;
            }
        }
        // A or left -- turn left
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            movementSpeed = -0.1f;
            Vector3 newRotation = Time.deltaTime * visionSpeed * transform.up;
            transform.eulerAngles -= newRotation;
        }
        // S or down -- move back
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            movementSpeed = 1.0f;
            if (collision) {
                m_controller.Move(movementSpeed * (-transform.forward + Physics.gravity) * Time.deltaTime);
            } else {
                transform.position -= Time.deltaTime * movementSpeed * transform.forward;
            }
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
    public void addPoint(int increment) {
        _score += increment;
        Debug.Log("Adding a point. Total points: " + _score);
        _UIManager.UpdateScore(_score);
    }
}
