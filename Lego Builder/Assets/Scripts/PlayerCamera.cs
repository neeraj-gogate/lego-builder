using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCamera : MonoBehaviour
{
    public Slider slider;
    public float sens;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        //cursor invisible
        //sens = PlayerPrefs.GetFloat("currentSens", 400);
        slider.value = sens/10;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        //PlayerPrefs.SetFloat("currentSens", sens);
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens;
        yRotation += mouseX;
        xRotation -= mouseY;

       //make it so you can't go 180 by pulling your mouse down or up
       xRotation = Mathf.Clamp(xRotation, -90f, 90f);

       //apply to object
       transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
       orientation.rotation = Quaternion.Euler(0,yRotation, 0);
    }
    public void AdjustSpeed()
    {
        sens = slider.value * 10;
    }
}
