using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_Info : Stat_Info
{
    void OnEnable()
    {
        icon.sprite = scriptable.statImage;
        title.text = scriptable.statName;

        if (GameManager.instance.playerInfo.range >= 0)
        {
            infoText.text = scriptable.statPlusText[0] + " " + GameManager.instance.playerInfo.range.ToString("F0") + scriptable.statPlusText[1] + " " +
                scriptable.statPlusText[2] + " " + scriptable.statPlusText[3];
        }
        else
        {
            infoText.text = scriptable.statMinusText[0] + " " + GameManager.instance.playerInfo.range.ToString("F0") + scriptable.statMinusText[1] + " " +
                scriptable.statMinusText[2] + " " + scriptable.statMinusText[3];
        }
    }
}
