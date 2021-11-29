using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private AudioManager theAudio;

    public string startSound;
    public string UISound;
    public string X_ButtonSound;
    public GameManager gameManager;
    Animator anim;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        theAudio = FindObjectOfType<AudioManager>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    public void OnClickStart()
    {
        anim.SetBool("isStart",true);
        theAudio.Play(startSound);
        Invoke("LoadScene", 2f);
        gameManager.stageIndex = 1;
    }

    public void OnClickExplain()
    {
        anim.SetBool("isExplain", true);
        theAudio.Play(UISound);
    }
    public void OnClick_X()
    {
        anim.SetBool("isExplain", false);
        theAudio.Play(X_ButtonSound);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(1);
    }


}
