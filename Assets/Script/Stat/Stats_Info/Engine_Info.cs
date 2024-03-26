using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine_Info : Stat_Info
{
    void OnEnable()
    {
        icon.sprite = scriptable.statImage;
        title.text = scriptable.statName;


        infoText.text = scriptable.statPlusText[0] + " " + scriptable.statPlusText[1] + scriptable.statPlusText[2];

    }
}
