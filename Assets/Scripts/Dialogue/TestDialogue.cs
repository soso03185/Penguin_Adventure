using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;

    private DialogueManager theDM;
    public bool talk = false;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if (talk && Input.GetKeyDown(KeyCode.Z))
        {
            talk = false;
            theDM.ShowDialogue(dialogue);          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {    
        if(collision.gameObject.name == "Player")        
            talk = true;

        if (collision.gameObject.tag == "EventStart")
        {
            Debug.Log("EventStart 닿음");
            theDM.ShowDialogue(dialogue);
            collision.gameObject.SetActive(false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")        
            talk = false;                  
    }
}
