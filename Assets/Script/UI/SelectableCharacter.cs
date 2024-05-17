using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectableCharacter : MonoBehaviour
{
    public Image characterIcon;
    public Text characterName;
    public TextMeshProUGUI characterInfo;

    public void TextSetting(int index, Sprite icon)
    {
        characterIcon.sprite = icon;

        PlayerSelectTextInfoTable.Data import = GameManager.instance.gameDataBase.playerSelectTextInfoTable.table[index];

        characterName.text = import.characterName;

        string infoText = null;
        for (int i = 0; i < import.characterInfo.Length; i++)
        {
            infoText += import.characterInfo[i];
        }
        characterInfo.text = infoText;
    }
}
