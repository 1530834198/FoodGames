using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkButton : MonoBehaviour
{
    public GameObject button;
    public GameObject talkUI;

    void Update()
    {
        if (button.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            button.SetActive(false);
            talkUI.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        button.SetActive(true);
    }

    private void OnCollisionExit(Collision other)
    {
        button.SetActive(false);
    }
}
