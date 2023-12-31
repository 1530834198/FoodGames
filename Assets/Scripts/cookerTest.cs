﻿using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class cookerTest : MonoBehaviour
{
    
    [Header("UI组件")] public Text textLable;//textUI-显示文本用的
    public Image FaceImage;//显示当前讲话的人物的头像的
    public GameObject textPanel;//对话框
    public GameObject button;
    public List<Item> oysterFoodList;//海蛎煎的食材列表
    public GameObject teachVideo;//教学视频对象
    public VideoPlayer video;//教学视频
    public GameObject taskSmallUI;//任务UI
    
    public List<string> talkList = new List<string>();//存放文本数据
    private string[] textString;
    private int index;
    private bool isNpcCooker;
    [Header("头像")]public Sprite player, Npc;//角色头像
    
    public Item item;//物品
    public Inventory playerInventory;//背包
    public Image itemImage;//物品图片
    public Text itemText;//物品名称
    public GameObject itemPanel;//物品名称
    public GameObject itemAudio;//获得物品时的声音

    private float startTime;//开始显示时间
    private int coldTime = 2;//显示时间
    private bool isFill;//是否开始显示
    private GameObject itembgm;
    private bool isOverVideo;//判断视频是否播放过一次完了

    void Start()
    {
        //获取对话文本
        index = 0;
        string filePath = "Assets/File/NpcCooker.txt";
        if (File.Exists(filePath))
        {
            string text = File.ReadAllText(filePath);
            textString = text.Split('\n');
        }

        foreach (var dialogue in textString)
        {
            talkList.Add(dialogue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //对话显示
        if (isNpcCooker && talkList.Count!=0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (button.activeSelf)
                {
                    button.SetActive(false);
                    textPanel.SetActive(true);
                }
                if(index == talkList.Count || index == -1)
                {
                    textPanel.SetActive(false);
                    index = 0;
                    taskSmallUI.SetActive(true);
                }
            }

            if (!playerInventory.itemsList.Contains(item))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //判断当前是谁的对话，并且切换头像
                    switch (talkList[index].Trim())
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
                    textLable.text = talkList[index];
                    index++;
                }
            }
            else
            {
                //已经做完海蛎煎了
                if (Input.GetKeyDown(KeyCode.F))
                {
                    textLable.text = "滚蛋";
                    FaceImage.sprite = Npc;
                    index=-1;
                }
            }
            
        }
        
        //如果收集器所有材料，开始制作
        if (playerInventory.itemsList.Count==oysterFoodList.Count && playerInventory.itemsList.All(oysterFoodList.Contains))
        {
            if (isNpcCooker && Input.GetKeyDown(KeyCode.F))
            {
                textLable.text = "你实在是在厉害了！竟然集齐了所有材料，那我们马上开始制作吧。按R键开始制作。";
                FaceImage.sprite = Npc;
                textPanel.SetActive(true);
            }
            
            if (isOverVideo && !teachVideo.activeSelf)
            {
                textLable.text = "喏，海蛎煎给你！快滚快滚！！！";
                textPanel.SetActive(true);
                teachVideo.SetActive(false);
            }
            
            if (isOverVideo && textPanel.activeSelf && Input.GetKeyDown(KeyCode.F))
            {
                textPanel.SetActive(false);
                if (!playerInventory.itemsList.Contains(item))
                {
                    // playerInventory.itemsList.Clear();
                    playerInventory.itemsList.Add(item);
                    InventoryManager.CreateNewItem(item);
                    isFill = true;
                    itemImage.sprite = gameObject.GetComponent<cookerTest>().item.itemImage;
                    itemText.text = "恭喜您获得了"+gameObject.GetComponent<cookerTest>().item.itemName;
                    itembgm = Instantiate(itemAudio);
                    itembgm.GetComponent<AudioSource>().Play();
                }
            }
            if (textPanel.activeSelf && Input.GetKeyDown(KeyCode.R))
            {
                if (!isOverVideo) {
                    textPanel.SetActive(false);
                    teachVideo.SetActive(true);
                    if (!video.isPrepared)
                    {
                        // 等待视频准备就绪
                        StartCoroutine(PrepareVideoAndPlay());
                    }
                    else
                    {
                        video.Play();
                    }
                    isOverVideo = true;
                }
            }
        }
        //计算显示时间
        if (isFill)
        {
            startTime += Time.deltaTime;
            if (startTime <= coldTime)
            {
                //显示获得的物品
                itemPanel.SetActive(true);
            }
            else
            {
                isFill = false;
                startTime = 0;
                itemPanel.SetActive(false);
                Destroy(itembgm);
                //要把update停掉,不然一直执行。
                enabled = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            button.SetActive(true);
        }
        if (gameObject.CompareTag("NpcCooker"))
        {
            isNpcCooker = true;
            enabled = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        button.SetActive(false);
        isNpcCooker = false;
        enabled = false;
    }

    // 等待视频准备就绪的协程
    private IEnumerator PrepareVideoAndPlay()
    {
        video.Prepare();

        while (!video.isPrepared)
        {
            yield return null;
        }

        video.Play();
    }
}
