using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luck_Info : Stat_Info
{
    void OnEnable()
    {
        icon.sprite = scriptable.statImage;
        title.text = scriptable.statName;

        if (StageManager.instance.playerInfo.lucky >= 0)
        {
            infoText.text = scriptable.statPlusText[0] + " " + StageManager.instance.playerInfo.lucky.ToString("F0") + scriptable.statPlusText[1] + " " + scriptable.statPlusText[2];
        }
        else
        {
            float luck = -(StageManager.instance.playerInfo.lucky);
            infoText.text = scriptable.statMinusText[0] + " " + luck.ToString("F0") + scriptable.statMinusText[1] + " " + scriptable.statMinusText[2];
        }
    }
}
