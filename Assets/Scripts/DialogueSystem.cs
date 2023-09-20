﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI组件")] public Text textLable;//textUI-显示文本用的
    public Image FaceImage;//显示当前讲话的人物的头像的
    public GameObject textPanel;//对话框
    
    [Header("文本文件")] public TextAsset textFile;//文本文件
    public List<string> textList = new List<string>();//存放文本数据

    private bool hasCollided = false;//判断是否碰撞
    private int index;
    [Header("头像")]public Sprite player, Npc;//角色头像

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && index == textList.Count && textList.Count != 0)
        {
            textPanel.SetActive(false);
            index = 0;
            hasCollided = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && hasCollided)
        {
            //判断当前是谁的对话，并且切换头像
            switch (textList[index].Trim())
            {
                case "A":
                    FaceImage.sprite = Npc;
                    index++;
                    break;
                case "Player":
                    FaceImage.sprite = player;
                    index++;
                    break;
            }
            //遍历每一条数据
                textLable.text = textList[index];
            index++;
        }
    }

    void GetTextFormFile(string text)
    {
        textList.Clear();//置空
        index = 0;
        //分割每一行的数据存入集合
        var lineDate = text.Split('\n');
        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasCollided)
        {
            string path = Application.dataPath+"\\File\\"+name+"_dialogue.txt";
            path=path.Replace("/","\\");
            //直接以文本的方式读取
            string content=File.ReadAllText(path);
            //文本加入List
            GetTextFormFile(content);
        }
        hasCollided = true;
    }
}
