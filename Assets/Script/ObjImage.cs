using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjImage : MonoBehaviour
{
    public GameObject image;

    public void RefreshImage(int num)
    {
        if (num == 1)//���룬����
        {
            image.GetComponent<TeamButton>().LightUpImage();
        }
        else//�뿪��Ϩ��
        {
            image.GetComponent<TeamButton>().ExtinguishImage();
        }
    }
}
