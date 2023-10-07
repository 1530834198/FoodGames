using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Examples;
using UnityEngine;
using UnityEngine.UI;

public class Wash : MonoBehaviour
{
    public GameObject button;
    public Item newItem;//物品
    public Item oldItem;//物品

    private bool isFlag;//是否碰撞

    private Inventory playerInventory;//背包
    public Image itemImage;//物品图片
    public Text itemText;//物品名称
    public GameObject itemPanel;//物品名称
    public GameObject itemAudio;//获得物品时的声音
    
    private float startTime;//开始显示时间
    private int coldTime = 3;//显示时间
    private bool isFill;//是否开始显示
    private GameObject itembgm;

    // Update is called once per frame
    void Update()
    {
        if (button.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            button.SetActive(false);
            
        }
        if (Input.GetKeyDown(KeyCode.F) && isFlag){
            if (playerInventory.itemsList.Contains(oldItem))
            {
                if (!playerInventory.itemsList.Contains(newItem))
                {
                    playerInventory.itemsList.Add(newItem);
                    InventoryManager.CreateNewItem(newItem);
                    isFill = true;
                    itemImage.sprite = gameObject.GetComponent<Wash>().newItem.itemImage;
                    itemText.text = "恭喜您获得了" + gameObject.GetComponent<Wash>().newItem.itemName;
                    itembgm = Instantiate(itemAudio);
                    itembgm.GetComponent<AudioSource>().Play();

                    playerInventory.itemsList.Remove(oldItem);
                }
            }
        }

        if (isFill)
        {
            startTime += Time.deltaTime;
            if (startTime < coldTime)
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
        button.SetActive(true);
        if (collision.gameObject.CompareTag("Player"))
        {
            isFlag = true;
            playerInventory = collision.gameObject.GetComponent<Player>().playerInventory;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        button.SetActive(false);
        isFlag = false;
    }
}
