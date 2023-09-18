using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory myBag;//背包
    public GameObject slotGrid;//格子
    public Slot slotPrefab;
    public Text ItemInfromation;//详情

    private void Awake()
    {
        if (instance !=null)
        {
            Destroy(this);
        }

        instance = this;
    }

    private void OnEnable()
    {
        // RefreshItem();
        instance.ItemInfromation.text = "";
    }

    /**
     * 物品信息
     */
    public static void UpdateItemInfo(string itemDescription)
    {
        instance.ItemInfromation.text = itemDescription;
    }
    /**
     * 创建新的物品
     */
    public static void CreateNewItem(Item item)
    {
        //在slotGrid里面生成
        Slot newItem = Instantiate(instance.slotPrefab,instance.slotGrid.transform.position,Quaternion.identity);
        //设置父类，把物品挂到Grid下面成为子集
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        Debug.Log("123");
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;//传输图片
    }

    // /**
    //  * 增加物品数量
    //  */
    // public static void RefreshItem()
    // {
    //     for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
    //     {
    //         if (instance.slotGrid.transform.childCount == 0)
    //         {
    //             break;
    //         }
    //         Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
    //     }
    //
    //     for (int i = 0; i < instance.myBag.itemsList.Count; i++)
    //     {
    //         CreateNewItem(instance.myBag.itemsList[i]);
    //     }
    // }
}
