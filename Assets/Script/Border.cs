using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Border : MonoBehaviour
{
    public GameObject followCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().isDead = true;
            //enemyAI中移除
            GameManager.instance.playerSurvive.Remove(other.gameObject);
            SoundService.instance.Play("PlayerDead");
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().isDead = true;
            //enemyAI中移除
            GameManager.instance.enemySurvive.Remove(other.gameObject);
            SoundService.instance.Play("EnemyDead");
        }

        //如果摄像头跟随则换视角
        if (followCam.GetComponent<CinemachineVirtualCamera>().Follow == other.transform) 
        {
            followCam.GetComponent<CinemachineVirtualCamera>().Follow =
                GameManager.instance.playerSurvive[0].transform;
        }
        //死亡图标
        other.GetComponent<ObjImage>().image.GetComponent<TeamButton>().ObjDead();
    }
}
