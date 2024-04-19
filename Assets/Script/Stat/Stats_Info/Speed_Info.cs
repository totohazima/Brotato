using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Info : Stat_Info
{
    void OnEnable()
    {
        icon.sprite = scriptable.statImage;
        title.text = scriptable.statName;

        if (StageManager.instance.playerInfo.speed >= 0)
        {
            infoText.text = StageManager.instance.playerInfo.speed.ToString("F0") + scriptable.statPlusText[0];
        }
        else
        {
            infoText.text = StageManager.instance.playerInfo.speed.ToString("F0") + scriptable.statMinusText[0];
        }
    }
}
