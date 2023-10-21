using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cookerTest : MonoBehaviour
{
    
    [Header("UI组件")] public Text textLable;//textUI-显示文本用的
    public Image FaceImage;//显示当前讲话的人物的头像的
    public GameObject textPanel;//对话框
    public GameObject button;
    
    // [Header("文本文件")] public TextAsset textFile;//文本文件
    public List<string> talkList = new List<string>();//存放文本数据

    private string[] textString;
    // private bool hasCollided = false;//判断是否碰撞
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
            if (button.activeSelf && Input.GetKeyDown(KeyCode.F))
            {
                button.SetActive(false);
                textPanel.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.F) && index == talkList.Count)
            {
                textPanel.SetActive(false);
                index = 0;
            }
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
        }
    }

    private void OnCollisionExit(Collision other)
    {
        button.SetActive(false);
        isNpcCooker = false;
    }
}
