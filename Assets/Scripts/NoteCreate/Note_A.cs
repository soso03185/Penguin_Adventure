using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_A : MonoBehaviour
{
    public float noteSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.down * noteSpeed * Time.deltaTime;
    }

}