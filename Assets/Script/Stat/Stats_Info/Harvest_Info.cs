using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest_Info : Stat_Info
{
    void OnEnable()
    {
        icon.sprite = scriptable.statImage;
        title.text = scriptable.statName;

        if (StageManager.instance.playerInfo.harvest >= 0)
        {
            infoText.text = scriptable.statPlusText[0] + " " + StageManager.instance.playerInfo.harvest.ToString("F0") + scriptable.statPlusText[1] + " " + scriptable.statPlusText[2] + " " + scriptable.statPlusText[3];
        }
        else
        {
            infoText.text = scriptable.statMinusText[0] + " " + StageManager.instance.playerInfo.harvest.ToString("F0") + scriptable.statMinusText[1];
        }
    }
}
