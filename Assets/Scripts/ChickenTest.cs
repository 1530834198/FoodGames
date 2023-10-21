using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenTest : MonoBehaviour
{
    [Header("UI组件")] public Text textLable;//textUI-显示文本用的
    public Image FaceImage;//显示当前讲话的人物的头像的
    public GameObject textPanel;//对话框
    public GameObject button;//F键交互UI
    public List<string> talkList = new List<string>();//存放文本数据
    private bool isact;

    private string[] textString;
    private int index;
    private bool isNpcChicken;
    public GameObject weilan;//草丛
    [Header("头像")] public Sprite player, Npc;//角色头像

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
        index = 0;
        string filePath = "Assets/File/NpcChicken.txt";
        if (File.Exists(filePath))
        {
            string text = File.ReadAllText(filePath);
            textString = text.Split('\n');
        }

        foreach (var VARIABLE in textString)
        {
            talkList.Add(VARIABLE);
        }
    }

    void Update()
    {
        //文本对话
        if (isNpcChicken &&talkList.Count != 0 && GameObject.FindWithTag("egg")!=null)
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
                //草丛碰撞体可接触
                weilan.GetComponent<Collider>().isTrigger = true;
            }
        }

        if (GameObject.FindWithTag("egg")==null)
        {
            //背包获取鸡蛋
            if (textPanel.activeSelf && Input.GetKeyDown(KeyCode.F))
            {
                textPanel.SetActive(false);
                isact = true;
                if (!playerInventory.itemsList.Contains(item))
                {
                    playerInventory.itemsList.Add(item);
                    InventoryManager.CreateNewItem(item);
                    isFill = true;
                    itemImage.sprite = gameObject.GetComponent<ChickenTest>().item.itemImage;
                    itemText.text = "恭喜您获得了" + gameObject.GetComponent<ChickenTest>().item.itemName;
                    itembgm = Instantiate(itemAudio);
                    itembgm.GetComponent<AudioSource>().Play();
                }
            }
            //如果捡了所有鸡蛋
            if (isNpcChicken && Input.GetKeyDown(KeyCode.F) && !textPanel.activeSelf && !isact)
            {
                textLable.text = "你实在是在厉害了！恭喜你获得鸡蛋。";
                FaceImage.sprite = Npc;
                textPanel.SetActive(true);
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
        if (gameObject.CompareTag("NpcChicken"))
        {
            isNpcChicken = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        button.SetActive(false);
        isNpcChicken = false;
    }
}
