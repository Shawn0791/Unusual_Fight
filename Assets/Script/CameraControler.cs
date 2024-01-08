using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Transform followCamera;
    public float rotateY1;
    public float rotateY2;

    private float mouseX;
    private float mouseY;

    private void Start()
    {
        mouseX = followCamera.transform.eulerAngles.y;
        mouseY = followCamera.transform.eulerAngles.x;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
            CameraSpin();
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;//解除指针锁定
            Cursor.visible = true;//显示指针
        }
    }

    private void CameraSpin()
    {
        //使用鼠标来控制相机的视角的旋转
        Cursor.lockState = CursorLockMode.Locked;//锁定指针到视图中心
        Cursor.visible = false;//隐藏指针

        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        this.transform.eulerAngles = new Vector3(0, mouseX, 0);
        mouseY = Mathf.Clamp(mouseY, rotateY1, rotateY2); //限制相机在y轴上的旋转角度
        followCamera.transform.eulerAngles = new Vector3(mouseY, mouseX, 0);
        //让相机z轴保持不变，防止相机倾斜
        if (followCamera.transform.localEulerAngles.z != 0)
        {
            float rotX = followCamera.transform.localEulerAngles.x;
            float rotY = followCamera.transform.localEulerAngles.y;
            followCamera.transform.localEulerAngles = new Vector3(rotX, rotY, 0);
        }
    }
}
