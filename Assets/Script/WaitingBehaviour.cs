using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingBehaviour : MonoBehaviour
{
    public float waitTime;
    public GameObject EnemyBehaviour;
    public Text tips;

    private bool moving;
    private string lastTag;
    private Vector3 lastPos;
    private float lastTime;
    private GameObject lastObj;

    public static WaitingBehaviour instance;
    private void Awake()
    {
        //单例
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Update()
    {
        if(moving)
            WaitObjStop();
    }

    public void WaitingMove(GameObject obj)
    {
        lastPos = obj.transform.position;
        lastTag = obj.tag;
        lastObj = obj;
        lastTime = Time.time;

        GameManager.instance.gameMode = GameManager.GameMode.Waiting;

        moving = true;
    }

    private void WaitObjStop()
    {
        //进入等待回合
        tips.text = "Waiting";//设置提示

        if (Vector3.Distance(lastPos, lastObj.transform.position) > 0.5f) 
        {
            lastPos = lastObj.transform.position;
            lastTime = Time.time;
        }
        if (Time.time - lastTime > waitTime)
        {
            moving = false;
            lastTime = Time.time;
            if (lastTag == "Player")//enemy回合开始
            {
                GameManager.instance.gameMode = GameManager.GameMode.Enemy;
                WinAreaManager.instance.AddEnemyOccupyRound();
                EnemyBehaviour.GetComponent<EnemyBehaviour>().ChooseObjMove();
            }
            else//player回合开始
            {
                tips.text = "Player";//设置提示
                GameManager.instance.gameMode = GameManager.GameMode.Player;
                WinAreaManager.instance.AddPlayerOccupyRound();
            }
        }
    }
}
