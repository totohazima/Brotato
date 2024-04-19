using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Info : Stat_Info
{
    void OnEnable()
    {
        icon.sprite = scriptable.statImage;
        title.text = scriptable.statName;

        if (StageManager.instance.playerInfo.persentDamage >= 0)
        {
            infoText.text = scriptable.statPlusText[0] + " " + StageManager.instance.playerInfo.persentDamage.ToString("F0") + scriptable.statPlusText[1];
        }
        else
        {
            infoText.text = scriptable.statMinusText[0] + " " + StageManager.instance.playerInfo.persentDamage.ToString("F0") + scriptable.statMinusText[1];
        }
    }
}
