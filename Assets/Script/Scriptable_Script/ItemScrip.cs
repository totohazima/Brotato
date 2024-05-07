using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class ItemScrip : ScriptableObject
{
    public Stat.Tier itemTier;
    public Sprite itemSprite;
    public Item.ItemType itemCode;
    //public string itemName;
    //public string[] infoText; 
}
