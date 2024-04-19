using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Info : Stat_Info
{
    void OnEnable()
    {
        icon.sprite = scriptable.statImage;
        title.text = scriptable.statName;

        infoText.text = scriptable.statPlusText[0] + StageManager.instance.maxHp.ToString("F0") + " " + scriptable.statPlusText[1];
    }
}
