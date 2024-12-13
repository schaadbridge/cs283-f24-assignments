using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    public Animator m_Animator;
    public CharacterController m_controller;

    [SerializeField]
    private SkinnedMeshRenderer m_playerMesh;
    public float movementSpeed;
    [SerializeField]
    private float goSpeed = 3.0f;
    private static float stopSpeed = -0.1f;
    public float visionSpeed = 10.0f;
    public bool collision = true;
    public int _score = 0;
    public Boolean _exploreMode = false;
    private UIManager _UIManager;

    // Start is called before the first frame update
    void Start()
    {
        // Set customized player color
        if (MainManager.Instance != null)
        {
            m_playerMesh.GetComponent<SkinnedMeshRenderer>().material.color = MainManager.Instance.TeamColor;
        }
        movementSpeed = stopSpeed;

        // Get the animator, which you attach to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
        m_controller = gameObject.GetComponent<CharacterController>();
        _UIManager = GameObject.Find("HeadsUpDisplays").GetComponent<UIManager>();
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
        movementSpeed = stopSpeed;
        // W or up -- move forward
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            movementSpeed = goSpeed;
            if (collision) {
                m_controller.Move(moveVector * Time.deltaTime);
            } else {
                transform.position += Time.deltaTime * movementSpeed * transform.forward;
            }
        }
        // A or left -- turn left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            Vector3 newRotation = Time.deltaTime * visionSpeed * transform.up;
            transform.eulerAngles -= newRotation;
        }
        // S or down -- move back
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            movementSpeed = goSpeed;
            if (collision) {
                m_controller.Move(movementSpeed * (-transform.forward + Physics.gravity) * Time.deltaTime);
            } else {
                transform.position -= Time.deltaTime * movementSpeed * transform.forward;
            }
        }
        // D or right -- turn right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            Vector3 newRotation = Time.deltaTime * visionSpeed * transform.up;
            transform.eulerAngles += newRotation;
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

    public void OnCollisionEnter()
    {
        addPoint(-1);
        // drop mushroom
    }
}
