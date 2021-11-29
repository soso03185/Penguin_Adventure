using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hpup : MonoBehaviour
{
    public int NowHp_Index = 0;
    public Text HP;
    public Animator anim;

    public PlayerControl Player;
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        Player = FindObjectOfType<PlayerControl>();
    }

    private void Update()
    {
        UI_Hp();
    }

    public void UI_Hp()
    {
        HP.text = "HP : " + gameManager.AllHp_Index + " + " + NowHp_Index;

        if (NowHp_Index != Player.OldHP_Index)  //포션 먹으면
        {
            NowHp_Index = Player.OldHP_Index;
            gameManager.ReHp_Index = NowHp_Index;

            anim.SetBool("isHPshow", true);
            Invoke("HpDisAppear", 1.5f);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
            anim.SetBool("isHPshow", true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            HpDisAppear();
    }

    void HpDisAppear()
    {
        anim.SetBool("isHPshow", false);
    }


}
