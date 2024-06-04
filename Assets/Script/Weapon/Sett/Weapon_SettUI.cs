using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Weapon_SettUI : MonoBehaviour, UI_Upadte
{
    public Weapon.SettType settType;
    public Transform textGroups;
    [HideInInspector] public Text[] texts;
    [HideInInspector] public GameManager game;
    void Awake()
    {
        game = GameManager.instance;

        texts = new Text[textGroups.childCount];
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i] = textGroups.GetChild(i).GetComponent<Text>();
        }
    }
    void OnEnable()
    {       
        UIUpdateManager.uiUpdates.Add(this); 
    }

    void OnDisable()
    {
        UIUpdateManager.uiUpdates.Remove(this);
    }

    public virtual void UI_Update()
    {
        switch (settType)
        {
            case Weapon.SettType.UNARMED:
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i == game.playerInfo.unArmed_Set - 2)
                    {
                        texts[i].color = Color.white;
                    }
                    else
                    {
                        texts[i].color = Color.gray;
                    }
                }
                break;
            case Weapon.SettType.TOOL:
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i == game.playerInfo.tool_Set - 2)
                    {
                        texts[i].color = Color.white;
                    }
                    else
                    {
                        texts[i].color = Color.gray;
                    }
                }
                break;
            case Weapon.SettType.GUN:
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i == game.playerInfo.gun_Set - 2)
                    {
                        texts[i].color = Color.white;
                    }
                    else
                    {
                        texts[i].color = Color.gray;
                    }
                }
                break;
            case Weapon.SettType.EXPLOSIVE:
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i == game.playerInfo.explosive_Set - 2)
                    {
                        texts[i].color = Color.white;
                    }
                    else
                    {
                        texts[i].color = Color.gray;
                    }
                }
                break;
            case Weapon.SettType.PRECISION:
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i == game.playerInfo.precision_Set - 2)
                    {
                        texts[i].color = Color.white;
                    }
                    else
                    {
                        texts[i].color = Color.gray;
                    }
                }
                break;
            case Weapon.SettType.NATIVE:
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i == game.playerInfo.native_Set - 2)
                    {
                        texts[i].color = Color.white;
                    }
                    else
                    {
                        texts[i].color = Color.gray;
                    }
                }
                break;
            case Weapon.SettType.ELEMENTALS:
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i == game.playerInfo.elemental_Set - 2)
                    {
                        texts[i].color = Color.white;
                    }
                    else
                    {
                        texts[i].color = Color.gray;
                    }
                }
                break;
        }
    }
}
