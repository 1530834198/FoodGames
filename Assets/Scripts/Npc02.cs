using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc02 : MonoBehaviour
{
    private int listCount;
    private int index;
    private bool isOverTalk;

    public GameObject AnswerSystem;
    // Update is called once per frame
    void Update()
    {
        // isOverTalk = gameObject.GetComponent<DialogueSystem>().isOverTalk;
        // if (isOverTalk)
        // {
        //     
        // }
        // else
        // {
        //     AnswerSystem.SetActive(false);
        //     //鼠标隐藏
        //     Cursor.visible = false;
        //     //鼠标锁定
        //     Cursor.lockState = CursorLockMode.Locked;
        // }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // listCount = gameObject.GetComponent<DialogueSystem>().textList.Count;
        }
    }
}
