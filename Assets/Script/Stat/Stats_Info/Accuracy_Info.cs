using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accuracy_Info : Stat_Info
{
    void OnEnable()
    {
        icon.sprite = scriptable.statImage;
        title.text = scriptable.statName;

        if (StageManager.instance.playerInfo.accuracy >= 0)
        {
            infoText.text = StageManager.instance.playerInfo.accuracy.ToString("F0") + scriptable.statPlusText[0];
        }
        else
        {
            infoText.text = StageManager.instance.playerInfo.accuracy.ToString("F0") + scriptable.statMinusText[0];
        }
    }
}
