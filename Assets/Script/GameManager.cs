using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("队伍信息")]
    public GameObject playerTeam;
    public GameObject enemyTeam;
    public List<GameObject> playerSurvive;
    public List<GameObject> enemySurvive;
    [Header("UI")]
    public GameObject victoryMenu;
    public GameObject failMenu;
    public GameObject mainMenu;
    public GameObject teamMenu;
    public GameObject pauseMenu;
    public GameObject miniMap;
    public GameObject helpPage;
    [Header("摄像机")]
    public GameObject folllowCam;
    public GameObject StartCam;

    private bool pause;
    private GameMode lastMode;

    public GameMode gameMode;
    public enum GameMode
    {
        Menu,
        Player,
        Enemy,
        Waiting,
    }

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

    private void Start()
    {
        //开局等待
        gameMode = GameMode.Menu;
        Time.timeScale = 0;

        FindAllObj();
    }

    private void Update()
    {
        PauseGame();
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        StartCam.SetActive(false);
        teamMenu.SetActive(true);
        miniMap.SetActive(true);

        //玩家操作模式
        gameMode = GameMode.Player;
        Time.timeScale = 1;
    }

    public void GameHelp()
    {
        helpPage.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameMode != GameMode.Menu)
        {
            lastMode = gameMode;
            gameMode = GameMode.Menu;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            pause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            gameMode = lastMode;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            pause = false;
        }
    }

    //public void ChangeTable()
    //{
    //    if (tableNum < tables.Length - 1)
    //        tableNum++;
    //    else if (tableNum == tables.Length - 1)
    //        tableNum = 0;
    //    //换桌子
    //    for (int i = 0; i < tables.Length; i++)
    //    {
    //        tables[i].SetActive(false);
    //    }
    //    tables[tableNum].SetActive(true);
    //    //结算界面关闭
    //    victoryMenu.SetActive(false);
    //    failMenu.SetActive(false);

    //    //重新寻找人物和敌人
    //    findObject();

    //    //人物和敌人激活
    //    player.SetActive(true);
    //    enemy.SetActive(true);
    //    //人物和敌人速度清零
    //    player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    //人物和敌人位置归正
    //    player.transform.position = playerBorn[tableNum].position;
    //    enemy.transform.position = enemyBorn[tableNum].position;
    //    player.transform.rotation = Quaternion.Euler(0, 0, 0);
    //    enemy.transform.rotation = Quaternion.Euler(0, 0, 0);

    //    Debug.Log("change");
    //}

    //public void RestartTable()
    //{
    //    //重新寻找人物和敌人
    //    findObject();
    //    //人物和敌人激活
    //    player.SetActive(true);
    //    enemy.SetActive(true);
    //    //人物和敌人速度清零
    //    player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    //人物和敌人位置归正
    //    player.transform.position = playerBorn[tableNum].position;
    //    enemy.transform.position = enemyBorn[tableNum].position;
    //    player.transform.rotation = Quaternion.Euler(0, 0, 0);
    //    enemy.transform.rotation = Quaternion.Euler(0, 0, 0);

    //    //结算界面关闭
    //    victoryMenu.SetActive(false);
    //    failMenu.SetActive(false);

    //    Debug.Log("restart");
    //}

    public void WinGame()
    {
        victoryMenu.SetActive(true);
        gameMode = GameMode.Menu;
        SoundService.instance.Play("Victory");
    }

    public void FailGame()
    {
        failMenu.SetActive(true);
        gameMode = GameMode.Menu;
        SoundService.instance.Play("Fail");
    }

    public void PlayerAgain()
    {

    }

    public void PlayNext()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(0);
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        StartCam.SetActive(true);
        teamMenu.SetActive(false);
        miniMap.SetActive(false);
        pauseMenu.SetActive(false);
        pause = false;

        //玩家操作模式
        gameMode = GameMode.Menu;
        Time.timeScale = 0;
    }

    private void FindAllObj()
    {
        for (int i = 0; i < enemyTeam.transform.childCount; i++)
        {
            enemySurvive.Add(enemyTeam.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < playerTeam.transform.childCount; i++)
        {
            playerSurvive.Add(playerTeam.transform.GetChild(i).gameObject);
        }
    }
}
