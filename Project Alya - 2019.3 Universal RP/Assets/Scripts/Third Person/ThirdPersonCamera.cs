using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor;
    public float mouseSensitivity = 10;
    public Transform target; //What the camera is to follow
    float dstFromTarget = 5;
    public float dstFromPlayer = 7; //Distance from which the camera is placed
    public float strengthOfZoom = 25; //Strength of camera zoom change depending on pitch
    public Vector2 pitchMinMax = new Vector2(-40, 85); //Clamping the pitch rotation of the camera. X is min value, Y is max value

    public float rotationSmoothTime = 0.10f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked; //Locks cursor in place
            Cursor.visible = false;
        }
    }

    void LateUpdate() //called after all the other update methods, the targets position will be set before we have to set the camera position
    {
        //Taking in mouse input and applying mouse sensitivity
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y); //Clamping the amount the camera can rotate up and down

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

        dstFromTarget = pitch / strengthOfZoom + dstFromPlayer; //Changing camera zoom distance depending on angle

        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * dstFromTarget;
    }


}
