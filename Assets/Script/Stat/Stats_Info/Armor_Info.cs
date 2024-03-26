using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor_Info : Stat_Info
{
    void OnEnable()
    {
        icon.sprite = scriptable.statImage;
        title.text = scriptable.statName;

        if (GameManager.instance.playerInfo.armor >= 0)
        {
            infoText.text = scriptable.statPlusText[0] + " " + GameManager.instance.playerInfo.armor.ToString("F0") + scriptable.statPlusText[1];
        }
        else
        {
            infoText.text = scriptable.statMinusText[0] + " " + -(GameManager.instance.playerInfo.armor) + scriptable.statMinusText[1];
        }
    }
}
