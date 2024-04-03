using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_Sett : Weapon_Set
{
    public override void CustomUpdate()
    {
        switch (weaponManager.explosive_Set)
        {
            case 2:
                texts[0].color = Color.white;
                break;
            case 3:
                texts[1].color = Color.white;
                break;
            case 4:
                texts[2].color = Color.white;
                break;
            case 5:
                texts[3].color = Color.white;
                break;
            case 6:
                texts[4].color = Color.white;
                break;
            default:
                for (int i = 0; i < texts.Length; i++)
                {
                    texts[i].color = Color.gray;
                }
                break;
        }
    }
}
