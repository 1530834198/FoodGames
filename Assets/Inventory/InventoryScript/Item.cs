using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{

    public string itemName;//物品名称
    public Sprite itemImage;//物品图片
    // public int itemHeld;//物品数量
    [TextArea] public string itemInfo;//物品信息

    public bool equip;//是否可装备
}
