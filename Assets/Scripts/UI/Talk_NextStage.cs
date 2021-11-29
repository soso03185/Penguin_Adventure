using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk_NextStage : MonoBehaviour
{
    public int PlusCount = 0;
    public bool Plus = false;
    public GameManager gameManager;
    public int LimitCount = 4;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlusCount == LimitCount)
        {
            Debug.Log("EventStart 닿음");
            this.gameObject.SetActive(false);
            gameManager.StageChangeInvoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EventStart")
        {
            PlusCount++;
        }
    }
}