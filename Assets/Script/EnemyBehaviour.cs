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
        //ռ���������player
        if (WinAreaManager.instance.players.Count != 0)
        {
            //���������������enemyС����������-1
            if (WinAreaManager.instance.enemys.Count < GameManager.instance.enemySurvive.Count - 1) 
            {
                int rand = Random.Range(0, 2);
                if (rand == 0)//50%���ʹ���������Ŀ��
                {
                    FindNearestObjAttack(WinAreaManager.instance.players[0].transform, GameManager.instance.enemySurvive);
                    Debug.Log("random attack player inside");
                }
                else//50%��һ��enemy��������
                {
                    EnterArea();
                    Debug.Log("random occupy");
                }
            }
            else
            {
                //����������Ŀ��
                FindNearestObjAttack(WinAreaManager.instance.players[0].transform, GameManager.instance.enemySurvive);
                Debug.Log("attack player inside");
            }
        }
        else
        {
            //������player������enemy
            if (WinAreaManager.instance.enemys.Count != 0)
            {
                FindObjOutside();
                //�����Ϊ�գ������һ��Ŀ��
                if (outOfArea.Count == 0)
                {
                    int rand = Random.Range(0, GameManager.instance.playerSurvive.Count);
                    FindNearestObjAttack(GameManager.instance.playerSurvive[rand].transform, GameManager.instance.enemySurvive);
                }
                else
                {
                    //����鲻Ϊ�գ������һ��Ŀ��������obj����
                    int rand = Random.Range(0, GameManager.instance.playerSurvive.Count);
                    FindNearestObjAttack(GameManager.instance.playerSurvive[rand].transform, outOfArea);
                }
                Debug.Log("attack player outside");
            }
            else    //�������κ�obj
            {
                EnterArea();
                Debug.Log("occupy");
            }
        }
    }
    //�ҵ����������ڵ�enemy
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
        //�������ҵ���Area�����enemy��ռ������
        FindNearestObjAttack(areaPos, outOfArea);
    }

    //�ҵ���ѡ��Ŀ�������enemy������Ŀ��
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

        //����ײ��
        selectedObj.GetComponent<Enemy>().target = target.gameObject;
        selectedObj.GetComponent<Enemy>().force *= 0.5f;
        selectedObj.GetComponent<Enemy>().Attack();
        selectedObj.GetComponent<Enemy>().force *= 2;
    }
}
