﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Inventory",menuName = "Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> itemsList = new List<Item>();
    
}
