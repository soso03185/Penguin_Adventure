using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour
{
    public Text HP;
    public PlayerControl Player;
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        Player = FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        HP.text = "HP : " + gameManager.AllHp_Index;        
    }
}
