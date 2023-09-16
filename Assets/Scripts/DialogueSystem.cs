using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI组件")] public Text textLable;//textUI-显示文本用的
    public Image FaceImage;//显示当前讲话的人物的头像的
    public GameObject textPanel;
    
    [Header("文本文件")] public TextAsset textFile;//文本文件

    List<string> textList = new List<string>();//存放文本数据
    private int index;
    private float textSpeed = 0.1f;//字体显示速度
    private bool textFinished;//判断一句话是否输出完了
    [Header("头像")]
    public Sprite player, Npc;//角色头像
    /**
     * Awake比start还早
     */
    void Awake()
    {
        GetTextFormFile(textFile);
    }

    /**
     * onEnable在start之前
     * 该方法是让对话框显示的时候显示第一条文本数据
     */
    private void OnEnable()
    {
        StartCoroutine(SetTextUI());
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && index == textList.Count)
        {
            textPanel.SetActive(false);
            index = 0;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && textFinished)
        {
            StartCoroutine(SetTextUI());
        }
    }

    /**
     * 把TextAsset数据文本存进数组
     */
    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();//置空
        index = 0;
        //分割每一行的数据存入集合
        var lineDate = file.text.Split('\n');
        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }

    /**
     * 协程-字体逐渐显示
     */
    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLable.text = "";

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
        for (int i = 0; i < textList[index].Length; i++)
        {
            textLable.text += textList[index][i];
            yield return new WaitForSeconds(textSpeed);
        }
        textFinished = true;
        index++;
    }
}
