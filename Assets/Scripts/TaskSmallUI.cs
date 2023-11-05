using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSmallUI : MonoBehaviour
{
    public GameObject eggCheckmark;
    public GameObject flourCheckmark;
    public GameObject garlicCheckmark;
    public GameObject gingerCheckmark;
    public GameObject laverCheckmark;
    public GameObject onionCheckmark;
    public GameObject oysterCheckmark;
    public GameObject cleanOysterCheckmark;
    public GameObject pepperCheckmark;
    public Inventory playerInventory;//背包
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInventory.itemsList != null)
        {
            if (playerInventory.itemsList.Exists(i => i.name == "egg"))
            {
                eggCheckmark.SetActive(true);
            }
            if (playerInventory.itemsList.Exists(i => i.name == "flour"))
            {
                flourCheckmark.SetActive(true);
            }
            if (playerInventory.itemsList.Exists(i => i.name == "garlic"))
            {
                garlicCheckmark.SetActive(true);
            }
            if (playerInventory.itemsList.Exists(i => i.name == "ginger"))
            {
                gingerCheckmark.SetActive(true);
            }
            if (playerInventory.itemsList.Exists(i => i.name == "laver"))
            {
                laverCheckmark.SetActive(true);
            }
            if (playerInventory.itemsList.Exists(i => i.name == "onion"))
            {
                onionCheckmark.SetActive(true);
            }
            if (playerInventory.itemsList.Exists(i => i.name == "oyster"))
            {
                oysterCheckmark.SetActive(true);
            }
            if (playerInventory.itemsList.Exists(i => i.name == "CleanOysters"))
            {
                cleanOysterCheckmark.SetActive(true);
            }
            if (playerInventory.itemsList.Exists(i => i.name == "pepper"))
            {
                pepperCheckmark.SetActive(true);
            }
        }

    }
}
