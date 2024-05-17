using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Sett : Weapon_Set
{
    public override void CustomUpdate()
    {
        if (game.gun_Set == 2)
        {
            texts[0].color = Color.white;
        }
        else if (game.gun_Set == 3)
        {
            texts[1].color = Color.white;
        }
        else if (game.gun_Set == 4)
        {
            texts[2].color = Color.white;
        }
        else if (game.gun_Set == 5)
        {
            texts[3].color = Color.white;
        }
        else if (game.gun_Set >= 6)
        {
            texts[4].color = Color.white;
        }
        else
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color = Color.gray;
            }
        }
    }
}
