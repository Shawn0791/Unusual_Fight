using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjImage : MonoBehaviour
{
    public GameObject image;

    public void RefreshImage(int num)
    {
        if (num == 1)//进入，亮起
        {
            image.GetComponent<TeamButton>().LightUpImage();
        }
        else//离开，熄灭
        {
            image.GetComponent<TeamButton>().ExtinguishImage();
        }
    }
}
