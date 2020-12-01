using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Sensitivity of the camera rotation
    public float m_mouseSensitivity = 100f;

    //Target and Camera location variables
    public Transform m_target;
    public Transform m_camera;

    //Max and min distance the camera can zoom
    public float m_zoomOutMax = -10f;
    public float m_zoomInMax = -3.5f;

    //Camera zoom speed
    public float m_zoomSpeed = 5f;

    //Camera FOV transition speed + default and maximum FOV
    public float m_FOVSpeed = 0.5f;
    public float m_maxFOV = 90f;
    public float m_defaultFOV = 60f;

    //reference for editing camera options (just FOV at the moment)
    public Camera m_cameraSettings;

    //ReCentering speed
    public float m_reCenterSpeed = 0.99f;

    //Timer + timer values to tell the camera when to recenter behind the player
    public float m_maxWaitTime = 3f;
    private float m_timer = 0f;

    //smoothing delay for camera movement
    private float m_moveDampTime = 0.2f;

    //Rotation vectors for the camera
    private float xRotation = 0f;
    private float yRotation = 0f;
    private float zRotation = 0f;

    //Movement vectors for the camera
    private Vector3 m_moveVelocity;
    private Vector3 m_desiredPosition;
    private Vector3 m_desiredRotation;

    private void Awake()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //TODO: Add in cursor lock options depending on the game state (reference the game manager)
        m_timer -= Time.deltaTime;

        //rotates camera back to default position on the z axis
        

        Rotate();
        Move();
        Zoom();
        ChangeFOV();
        ReCenter();
        //Cursor.lockState = CursorLockMode.Locked; //Locks + hide Cursor for gameplay

        //Cursor.lockState = CursorLockMode.None; //Unlocks Cursor for menus
    }

    //Script to move the camera with the player
    private void Move()
    {
        m_desiredPosition = m_target.position;
        transform.position = Vector3.SmoothDamp(transform.position, m_desiredPosition, ref m_moveVelocity, m_moveDampTime);
    }

    //Script that rotates the camera depending on the mouse movement
    private void Rotate()
    {
        //gets the distance the mouse has moved between updates and turns it into a rotation for the camera around the player
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * m_mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * m_mouseSensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -17.5f, 90f);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))//Rotates the camera left when the player turns left
        {
            zRotation += 0.2f;
            yRotation -= 0.5f;
            m_timer = 0;
           // Debug.Log("Left turn");
        }                                                                                                                                //BOTH KEYS CAN BE CHANGE IF NEED BE.
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))//Rotates the camera right when the player turns right
        {
            zRotation -= 0.2f;
            yRotation += 0.5f;
            m_timer = 0;
            // Debug.Log("Right turn");
        }
        else
        {
            zRotation = Mathf.Lerp(zRotation, 0, 0.05f);
        }
        //Clamps the z rotation
        zRotation = Mathf.Clamp(zRotation, -10f, 10f);

        if(mouseX > 0 && mouseY > 0)
        {
            m_timer = m_maxWaitTime;
        }

       // transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f); OLD ROTATION CODE
    }

    private void Zoom()
    {
        //Reads the input of the mouse scrollwheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if(scroll > 0 && m_camera.localPosition.z < m_zoomInMax)
        {
            m_camera.transform.Translate(Vector3.forward * Time.deltaTime * m_zoomSpeed);
        }
        else if(scroll < 0 && m_camera.localPosition.z > m_zoomOutMax)
        {
            m_camera.transform.Translate(Vector3.back * Time.deltaTime * m_zoomSpeed);
        }
    }

    private void ReCenter()
    {
        //slowly recenters the camera behind the player (has a cooldown while the camera is moving)
        if (m_timer <= 0)
        {
            m_timer = 0;
            m_desiredRotation = new Vector3(m_target.eulerAngles.x, m_target.eulerAngles.y, 0);

            xRotation *= m_reCenterSpeed;
            yRotation *= m_reCenterSpeed;
        }

        transform.eulerAngles = m_desiredRotation + new Vector3(xRotation, yRotation, zRotation);
    }

    private void ChangeFOV()
    {
        //changes the FOV when the player is moving (MIGHT NEED TO CHANGE KEY IF SOMETHING ELSE IS USED TO ACCELERATE)
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            m_cameraSettings.fieldOfView = Mathf.Lerp(m_cameraSettings.fieldOfView, m_maxFOV, m_FOVSpeed);
        }
        else
        {
            m_cameraSettings.fieldOfView = Mathf.Lerp(m_cameraSettings.fieldOfView, m_defaultFOV, m_FOVSpeed);
        }
    }
}