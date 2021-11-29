using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;
    public int NextIndex = 0;
    public int RhythmIndex = 1;
    [SerializeField] Transform tfNoteAppear = null;

    GameManager gameManager;
    TimingManager theTimingManager;
    EffectManager theEffectManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

            if (currentTime >= 60 / bpm)
            {
            GameObject t_Note = ObjectPool.instance.noteQueue.Dequeue();

            t_Note.transform.position = tfNoteAppear.position;
            t_Note.SetActive(true);


            theTimingManager.boxNoteList.Add(t_Note);

            currentTime -= 60d / bpm;
            }

        if (NextIndex == RhythmIndex)
        {
            gameManager.stageIndex++;
            SceneManager.LoadScene(gameManager.stageIndex);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                theEffectManager.JudgementEffect(4);  // 놓쳤을 때 miss 연출
                gameManager.AllHp_Index--;   //체력 깎기
            }
            NextIndex++;
            theTimingManager.boxNoteList.Remove(collision.gameObject);

            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);

            collision.gameObject.SetActive(false);
        }
    }
}
