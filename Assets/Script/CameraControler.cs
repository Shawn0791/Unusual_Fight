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
            Cursor.lockState = CursorLockMode.None;//���ָ������
            Cursor.visible = true;//��ʾָ��
        }
    }

    private void CameraSpin()
    {
        //ʹ�����������������ӽǵ���ת
        Cursor.lockState = CursorLockMode.Locked;//����ָ�뵽��ͼ����
        Cursor.visible = false;//����ָ��

        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        this.transform.eulerAngles = new Vector3(0, mouseX, 0);
        mouseY = Mathf.Clamp(mouseY, rotateY1, rotateY2); //���������y���ϵ���ת�Ƕ�
        followCamera.transform.eulerAngles = new Vector3(mouseY, mouseX, 0);
        //�����z�ᱣ�ֲ��䣬��ֹ�����б
        if (followCamera.transform.localEulerAngles.z != 0)
        {
            float rotX = followCamera.transform.localEulerAngles.x;
            float rotY = followCamera.transform.localEulerAngles.y;
            followCamera.transform.localEulerAngles = new Vector3(rotX, rotY, 0);
        }
    }
}
