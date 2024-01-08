using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAreaManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject roundLight1;
    public GameObject roundLight2;
    public GameObject roundLight3;
    public GameObject areaRedLight;
    public GameObject areaBlueLight;
    public GameObject normalSnow;
    public GameObject redSnow;
    public GameObject blueSnow;

    public List<GameObject> players;
    public List<GameObject> enemys;

    private bool playerOccupy;
    private bool enemyOccupy;
    private int playerNum;
    private int enemyNum;
    private int playerOccupyNum;
    private int enemyOccupyNum;

    public static WinAreaManager instance;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNum++;
            players.Add(other.gameObject);
            playerOccupy = true;
        }

        if (other.CompareTag("Enemy"))
        {
            enemyNum++;
            enemys.Add(other.gameObject);
            enemyOccupy = true;
        }

        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            other.GetComponent<ObjImage>().RefreshImage(1);

        RefreshOccupyLight();
        RefreshSnowColor();
        RefreshRoundLight();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNum--;
            players.Remove(other.gameObject);
            if (playerNum == 0)
            {
                playerOccupy = false;
                playerOccupyNum = 0;
            }
        }

        if (other.CompareTag("Enemy"))
        {
            enemyNum--;
            enemys.Remove(other.gameObject);
            if (enemyNum == 0)
            {
                enemyOccupy = false;
                enemyOccupyNum = 0;
            }
        }

        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            other.GetComponent<ObjImage>().RefreshImage(0);

        RefreshOccupyLight();
        RefreshSnowColor();
        RefreshRoundLight();

    }

    public void AddPlayerOccupyRound()
    {
        if (playerOccupy && !enemyOccupy) 
        {
            //player占领回合+1
            playerOccupyNum++;
            //清空enemy回合数
            enemyOccupyNum = 0;
        }
        else if (!playerOccupy && !enemyOccupy)
        {
            //清空双方回合数
            enemyOccupyNum = 0;
            playerOccupyNum = 0;
        }

        CheckWinCondition();
        RefreshRoundLight();
        RefreshSnowColor();
    }

    public void AddEnemyOccupyRound()
    {
        if (enemyOccupy && !playerOccupy) 
        {
            //enemy占领回合+1
            enemyOccupyNum++;
            //清空player回合数
            playerOccupyNum = 0;
        }
        else if (!playerOccupy && !enemyOccupy)
        {
            //清空双方回合数
            enemyOccupyNum = 0;
            playerOccupyNum = 0;
        }

        CheckWinCondition();
        RefreshRoundLight();
        RefreshSnowColor();
    }

    private void CheckWinCondition()
    {
        if (enemyOccupyNum == 3 || GameManager.instance.playerSurvive.Count == 0)
            GameManager.instance.FailGame();
        else if (playerOccupyNum == 3 || GameManager.instance.enemySurvive.Count == 0) 
            GameManager.instance.WinGame();
    }

    private void RefreshRoundLight()
    {
        ParticleSystem.MainModule light1 = roundLight1.GetComponent<ParticleSystem>().main;
        ParticleSystem.MainModule light2 = roundLight2.GetComponent<ParticleSystem>().main;
        ParticleSystem.MainModule light3 = roundLight3.GetComponent<ParticleSystem>().main;

        switch (enemyOccupyNum)
        {
            case 1:
                light1.startColor = Color.red;
                break;
            case 2:
                light1.startColor = Color.red;
                light2.startColor = Color.red;
                break;
            case 3:
                light1.startColor = Color.red;
                light2.startColor = Color.red;
                light3.startColor = Color.red;
                break;
        }

        switch (playerOccupyNum)
        {
            case 1:
                light1.startColor = Color.blue;
                break;
            case 2:
                light1.startColor = Color.blue;
                light2.startColor = Color.blue;
                break;
            case 3:
                light1.startColor = Color.blue;
                light2.startColor = Color.blue;
                light3.startColor = Color.blue;
                break;
        }

        if (!playerOccupy && !enemyOccupy)
        {
            light1.startColor = Color.white;
            light2.startColor = Color.white;
            light3.startColor = Color.white;
        }
    }

    private void RefreshOccupyLight()
    {
        if (enemys.Count != 0)
            areaRedLight.SetActive(true);
        else
            areaRedLight.SetActive(false);

        if (players.Count != 0)
            areaBlueLight.SetActive(true);
        else
            areaBlueLight.SetActive(false);
    }

    private void RefreshSnowColor()
    {
        if (enemyOccupyNum > 1)
        {
            normalSnow.SetActive(false);
            blueSnow.SetActive(false);
            redSnow.SetActive(true);
        }
        else if (playerOccupyNum > 1)
        {
            normalSnow.SetActive(false);
            redSnow.SetActive(false);
            blueSnow.SetActive(true);
        }
    }
}
