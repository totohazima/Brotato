using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemGroup", menuName = "ItemGroup/item_Groups")]
public class ItemGroup_Scriptable : ScriptableObject
{
    public ItemScrip[] items;
}
