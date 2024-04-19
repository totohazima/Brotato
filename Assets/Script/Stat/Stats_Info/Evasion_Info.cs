using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evasion_Info : Stat_Info
{
    void OnEnable()
    {
        icon.sprite = scriptable.statImage;
        title.text = scriptable.statName;

        if (StageManager.instance.playerInfo.evasion >= 0)
        {
            infoText.text = StageManager.instance.playerInfo.evasion.ToString("F0") + scriptable.statPlusText[0] + " " + scriptable.statPlusText[1];
        }
        else
        {
            infoText.text = scriptable.statMinusText[0] + " " + scriptable.statMinusText[1];
        }
    }
}
