using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThirdPersonCamera : MonoBehaviour
{
    public float rotation_speed = 1;
    public Transform target, player;
    float mouseX, mouseY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CamControl();
    }

    private void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotation_speed;
        mouseY -= Input.GetAxis("Mouse Y") * rotation_speed;
        //mouseY = Mathf.Clamp(mouseY, -35, 30);
        //transform.LookAt(target);
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
        
    }

    //private void OpenMenu()
    //{
    //    if (Input.GetKeyDown(KeyCode.Tab))
    //    {
    //        _menu.SetActive(true);
    //        Cursor.visible = true;
    //        Cursor.lockState = CursorLockMode.None;
    //    }
    //    else if (Input.GetButtonDown("Cancel"))
    //    {
    //        _menu.SetActive(false);
    //        Cursor.visible = false;
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //}
}
