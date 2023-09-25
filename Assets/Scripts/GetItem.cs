using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Examples;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    public Item item;//物品

    private bool isFlag;//是否碰撞

    private Inventory playerInventory;//背包
    public Image itemImage;//物品图片
    public Text itemText;//物品名称
    public GameObject itemPanel;//物品名称
    public GameObject itemAudio;//获得物品时的声音
    
    private float startTime;//开始显示时间
    private int coldTime = 2;//显示时间
    private bool isFill;//是否开始显示
    private GameObject itembgm;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isFlag)
        { 
            if (!playerInventory.itemsList.Contains(item))
            {
                playerInventory.itemsList.Add(item);
                InventoryManager.CreateNewItem(item);
                isFill = true;
                itemImage.sprite = gameObject.GetComponent<GetItem>().item.itemImage;
                itemText.text = "恭喜您获得了"+gameObject.GetComponent<GetItem>().item.itemName;
                itembgm = Instantiate(itemAudio);
                itembgm.GetComponent<AudioSource>().Play();
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
        if (collision.gameObject.CompareTag("Player"))
        {
            isFlag = true;
            playerInventory = collision.gameObject.GetComponent<Player>().playerInventory;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isFlag = false;
    }
}
