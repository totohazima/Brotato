using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkSpeed_Info : Stat_Info
{
    void OnEnable()
    {
        icon.sprite = scriptable.statImage;
        title.text = scriptable.statName;

        if (GameManager.instance.playerInfo.attackSpeed >= 0)
        {
            infoText.text = GameManager.instance.playerInfo.attackSpeed.ToString("F0") + scriptable.statPlusText[0] + " " + scriptable.statPlusText[1];
        }
        else
        {
            infoText.text = GameManager.instance.playerInfo.attackSpeed.ToString("F0") + scriptable.statMinusText[0] + " " + scriptable.statMinusText[1];
        }
    }
}
