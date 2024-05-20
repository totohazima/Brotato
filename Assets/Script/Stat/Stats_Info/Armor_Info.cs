using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor_Info : Stat_Info
{
    void OnEnable()
    {
        icon.sprite = scriptable.statImage;
        title.text = scriptable.statName;

        float armor = Mathf.Abs(GameManager.instance.player_Info.armor);
        float enduce = 1 / (1 + (armor / 15));
        enduce = 100 - (enduce * 100);
        if (StageManager.instance.playerInfo.armor >= 0)
        {
            infoText.text = scriptable.statPlusText[0] + " " + enduce.ToString("F0") + scriptable.statPlusText[1];
        }
        else
        {
            infoText.text = scriptable.statMinusText[0] + " " + enduce.ToString("F0") + scriptable.statMinusText[1];
        }
    }
}
