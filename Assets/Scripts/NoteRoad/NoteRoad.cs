using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteRoad : MonoBehaviour
{
  
    // public bool isNote = false;
    public BoxCollider2D boxcollider;
    public Animator anim;

    void Awake()
    {
        boxcollider = GetComponent<BoxCollider2D>();
        anim = FindObjectOfType<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            boxcollider.enabled = true;
        }
        else
        {
            boxcollider.enabled = false;
            anim.SetBool("isNoteRoad", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Note_A")
        {
            Debug.Log("Note_A 닿음");
            collision.gameObject.SetActive(false);
            anim.SetBool("isNoteRoad", true);
            Invoke("NoNoteRoad", 0.2f);
        }
    }

    void NoNoteRoad()
    {
        anim.SetBool("isNoteRoad", false);

    }
}
