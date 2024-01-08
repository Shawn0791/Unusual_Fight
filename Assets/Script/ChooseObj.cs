using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseObj : MonoBehaviour
{
    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void RightChange()
    //{
    //    //激活选中物块
    //    if (GameManager.instance.playerNum < objects.Length-1)
    //        GameManager.instance.playerNum++;
    //    else if (GameManager.instance.playerNum == objects.Length-1)
    //        GameManager.instance.playerNum = 0;
        
    //    for (int i = 0; i < objects.Length; i++)
    //    {
    //        objects[i].SetActive(false);
    //    }
    //    objects[GameManager.instance.playerNum].SetActive(true);
    //}

    //public void LeftChange()
    //{
    //    //激活选中物块
    //    if (GameManager.instance.playerNum > 0)
    //        GameManager.instance.playerNum--;
    //    else if (GameManager.instance.playerNum == 0)
    //        GameManager.instance.playerNum = objects.Length-1;

    //    for (int i = 0; i < objects.Length; i++)
    //    {
    //        objects[i].SetActive(false);
    //    }
    //    objects[GameManager.instance.playerNum].SetActive(true);
    //}
}
