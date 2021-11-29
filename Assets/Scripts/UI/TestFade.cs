using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFade : MonoBehaviour
{
    private FadeScript fade;

    void Awake()
    {
        fade = FindObjectOfType<FadeScript>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            fade.Fade();
        }
    }
}
