using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regen_Info : Stat_Info
{
    void OnEnable()
    {
        icon.sprite = scriptable.statImage;
        title.text = scriptable.statName;

        float regen = StageManager.instance.playerInfo.regeneration;
        float regenPer = 0.09f * regen;
        float regenSecond = 0;

        if (regenPer > 1)
        {
            float i = 1;
            while (true)
            {
                if (regenPer * i < 1)
                {
                    i += 0.01f;
                    regenSecond = i;
                    break;
                }
                i -= 0.01f;
            }
        }
        else if (regenPer < 1)
        {
            float i = 1;
            while (true)
            {
                if (regenPer * i < 1)
                {
                    regenSecond = i;
                    break;
                }
                i += 0.01f;
            }
        }


        string txt = null;
        if(regen > 0)
        {
            txt = scriptable.statPlusText[0] + " " + regenSecond.ToString("F2") + scriptable.statPlusText[1] + regenPer + scriptable.statPlusText[2];
        }
        else if(regen < 0)
        {
            txt = scriptable.statMinusText[0] + " " + scriptable.statMinusText[1] + scriptable.statMinusText[2];
        }
        else
        {
            txt = scriptable.statPlusText[0] + " 99" + scriptable.statPlusText[1] + "0.01" + scriptable.statPlusText[2];
        }
        infoText.text = txt;
    }
}
