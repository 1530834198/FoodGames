using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskUI : MonoBehaviour
{
    public Inventory playerInventory;//背包
    public List<string> listName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInventory.itemsList.Exists(i => i.name == "egg")) {

        }
          

    }
}
