using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int stageIndex = 1;
    public int AllHp_Index = 1;
    public int ReHp_Index = 1;
    public int nowHP = 1;
    UI_Hpup Hp;
    PlayerControl player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        Hp = FindObjectOfType<UI_Hpup>();
        player = FindObjectOfType<PlayerControl>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            AllHp_Index = nowHP;
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(stageIndex);
        }
    }

    public void StageChange()
    {
        AllHp_Index += ReHp_Index;
        nowHP = AllHp_Index;
        ReHp_Index = Hp.NowHp_Index;
        Debug.Log("Stage Change");
        stageIndex++;
        SceneManager.LoadScene(stageIndex);      
    }

    public void StageChangeInvoke()
    {
        Invoke("StageChange", 1f);
    }
}
