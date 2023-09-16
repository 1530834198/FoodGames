using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkButton : MonoBehaviour
{
    public GameObject button;
    public GameObject talkUI;
    
    // void Start()
    // {
    //     
    // }

    // Update is called once per frame
    void Update()
    {
        if (button.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            button.SetActive(false);
            talkUI.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        button.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        button.SetActive(false);
    }
}
