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
    private bool isTrigger;//是否接触，拾取
    public int isTriOrCon;
    public GameObject button;

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
        if (button.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            button.SetActive(false);
        }
        if (isTrigger || (Input.GetKeyDown(KeyCode.F) && isFlag))
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
            if (startTime <= coldTime)
            {
                //显示获得的物品
                itemPanel.SetActive(true);
                //接触改为false
                isTrigger = false;
            }
            else
            {
                isFill = false;
                startTime = 0;
                itemPanel.SetActive(false);
                Destroy(itembgm);
                if (isTriOrCon == 1)
                {
                    Destroy(gameObject);
                    //要把update停掉,不然一直执行。
                    enabled = false;
                }
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isFlag = true;
            button.SetActive(true);
            playerInventory = collision.gameObject.GetComponent<Player>().playerInventory;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isFlag = false;
        button.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
            playerInventory = collider.gameObject.GetComponent<Player>().playerInventory;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        isTrigger = false;
    }
}
