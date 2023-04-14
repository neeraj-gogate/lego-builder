using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringUpSettings : MonoBehaviour
{
    public GameObject setting;
    public bool issettingactive;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (issettingactive == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        setting.SetActive(true);
        issettingactive = true;
        this.GetComponent<PlayerCamera>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Resume()
    {
        setting.SetActive(false);
        issettingactive = false;
        this.GetComponent<PlayerCamera>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ExitGame() {
     Application.Quit();
    }
}
