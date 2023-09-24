﻿using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Examples;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveButton : MonoBehaviour
{
    public GameObject button;
    public GameObject talkUI;
    private bool isNpc;
    private string goods = "";

    void Update()
    {
        //如果是NPC就显示对话弹窗
        if (button.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            if (isNpc)
            {
                button.SetActive(false);
                talkUI.SetActive(true);
            }
            else
            {
                switch (goods)
                {
                    case "haili":
                        // PlayerPrefs.SetFloat("PlayerPositionX",transform.position.x);
                        // PlayerPrefs.SetFloat("PlayerPositionY",transform.position.y);
                        // PlayerPrefs.SetFloat("PlayerPositionZ",transform.position.z);
                        // PlayerPrefs.Save();
                        // DontDestroyOnLoad(GameObject.FindWithTag("Player"));
                        // SceneManager.LoadScene(2);
                        break;
                }
            }
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //判断碰撞是是否是NPC
        isNpc = gameObject.CompareTag("NPC");
        button.SetActive(true);
        goods = gameObject.tag;
    }

    private void OnCollisionExit(Collision other)
    {
        isNpc = false;
        button.SetActive(false);
    }
}