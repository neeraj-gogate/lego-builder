using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        //cursor invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        //mouse input
       float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
       float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
       
       
       yRotation += mouseX;
       xRotation -= mouseY;

       //make it so you can't go 180 by pulling your mouse down or up
       xRotation = Mathf.Clamp(xRotation, -90f, 90f);

       //apply to object
       transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
       orientation.rotation = Quaternion.Euler(0,yRotation, 0);
    }
}
