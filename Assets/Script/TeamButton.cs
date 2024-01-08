using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TeamButton : MonoBehaviour
{
    public GameObject followCam;
    public GameObject thisObj;

    public void ChangeCameraFollow()
    {
        if (thisObj.tag == "Enemy")
        {
            if (thisObj.GetComponent<Enemy>().isDead == false)
                followCam.GetComponent<CinemachineVirtualCamera>().Follow = thisObj.transform;
        }
        else
        {
            if(thisObj.GetComponent<Player>().isDead==false)
                followCam.GetComponent<CinemachineVirtualCamera>().Follow = thisObj.transform;
        }
    }

    public void LightUpImage()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ExtinguishImage()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ObjDead()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
