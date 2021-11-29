using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{
    GameManager gameManager;
    Animator anim;

    public bool isDie = false;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.AllHp_Index < 1 || isDie)
        {
            Restart_BtnShow();
            Invoke("StopTime", 1f);
        }
    }
    public void OnClick_Restart()
    {
        Time.timeScale = 1;
        gameManager.AllHp_Index = gameManager.nowHP;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isDie = false;
    }

    void Restart_BtnShow()
    {
        anim.SetBool("isRestart", true);
    }

    void StopTime()
    {
        Time.timeScale = 0;
    }
}
