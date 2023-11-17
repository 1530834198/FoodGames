using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;//物品
    public Inventory playerInventory;//背包

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }

    public void AddNewItem()
    {
        //如果物品不在背包里面，就增加进去
        if (!playerInventory.itemsList.Contains(thisItem))
        {
            playerInventory.itemsList.Add(thisItem);
            // Debug.Log("456");
            InventoryManager.CreateNewItem(thisItem);
        }
    }
    
    public void AddNewItem(Item item)
    {
        //如果物品不在背包里面，就增加进去
        if (!playerInventory.itemsList.Contains(item))
        {
            playerInventory.itemsList.Add(item);
            InventoryManager.CreateNewItem(item);
        }
    }
}
