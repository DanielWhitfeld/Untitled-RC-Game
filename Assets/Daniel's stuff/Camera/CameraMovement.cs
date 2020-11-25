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

    //smoothing delay for camera movement
    private float m_moveDampTime = 0.2f;

    //Rotation vectors for the camera
    private float xRotation = 0f;
    private float yRotation = 0f;

    //Movement vectors for the camera
    private Vector3 m_moveVelocity;
    private Vector3 m_desiredPosition;

    private void Awake()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //TODO: Add in cursor lock options depending on the game state (reference the game manager)
        Rotate();
        Move();
        Zoom();
        ChangeFOV();
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

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
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
