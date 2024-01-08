using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform areaPos;

    private float distance;
    private GameObject selectedObj;
    private List<GameObject> outOfArea = new List<GameObject>();

    public void ChooseObjMove()
    {
        //占领区域存在player
        if (WinAreaManager.instance.players.Count != 0)
        {
            //如果区域内区域内enemy小于最大存在数-1
            if (WinAreaManager.instance.enemys.Count < GameManager.instance.enemySurvive.Count - 1) 
            {
                int rand = Random.Range(0, 2);
                if (rand == 0)//50%概率攻击区域内目标
                {
                    FindNearestObjAttack(WinAreaManager.instance.players[0].transform, GameManager.instance.enemySurvive);
                    Debug.Log("random attack player inside");
                }
                else//50%再一个enemy进入区域
                {
                    EnterArea();
                    Debug.Log("random occupy");
                }
            }
            else
            {
                //攻击区域内目标
                FindNearestObjAttack(WinAreaManager.instance.players[0].transform, GameManager.instance.enemySurvive);
                Debug.Log("attack player inside");
            }
        }
        else
        {
            //不存在player但存在enemy
            if (WinAreaManager.instance.enemys.Count != 0)
            {
                FindObjOutside();
                //如果组为空，则随机一个目标
                if (outOfArea.Count == 0)
                {
                    int rand = Random.Range(0, GameManager.instance.playerSurvive.Count);
                    FindNearestObjAttack(GameManager.instance.playerSurvive[rand].transform, GameManager.instance.enemySurvive);
                }
                else
                {
                    //如果组不为空，则随机一个目标用组内obj攻击
                    int rand = Random.Range(0, GameManager.instance.playerSurvive.Count);
                    FindNearestObjAttack(GameManager.instance.playerSurvive[rand].transform, outOfArea);
                }
                Debug.Log("attack player outside");
            }
            else    //不存在任何obj
            {
                EnterArea();
                Debug.Log("occupy");
            }
        }
    }
    //找到不在区域内的enemy
    private void FindObjOutside()
    {
        outOfArea.Clear();

        foreach (var obj in GameManager.instance.enemySurvive)
        {
            if (!WinAreaManager.instance.enemys.Contains(obj))
                outOfArea.Add(obj);
        }
    }

    private void EnterArea()
    {
        FindObjOutside();
        //在组内找到离Area最近的enemy，占领区域
        FindNearestObjAttack(areaPos, outOfArea);
    }

    //找到离选定目标最近的enemy并攻击目标
    private void FindNearestObjAttack(Transform target,List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            float dis = Vector3.Distance(list[i].transform.position, target.position);
            if (i == 0)
            {
                distance = dis;
                selectedObj = list[i];
            }
            else
            {
                if (dis < distance)
                {
                    distance = dis;
                    selectedObj = list[i];
                }
            }
        }

        //进行撞击
        selectedObj.GetComponent<Enemy>().target = target.gameObject;
        selectedObj.GetComponent<Enemy>().force *= 0.5f;
        selectedObj.GetComponent<Enemy>().Attack();
        selectedObj.GetComponent<Enemy>().force *= 2;
    }
}
