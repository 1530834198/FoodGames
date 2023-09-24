using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Examples;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    public Item item;

    private bool isFlag;

    private Inventory playerInventory;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        { 
            if (!playerInventory.itemsList.Contains(item))
            {
                playerInventory.itemsList.Add(item);
                // Debug.Log("456");
                InventoryManager.CreateNewItem(item);
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
