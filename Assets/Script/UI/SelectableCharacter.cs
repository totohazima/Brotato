using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectableCharacter : MonoBehaviour
{
    [Header("캐릭터 해금")]
    public GameObject unLock_UI;
    public Image characterIcon;
    public Text characterName;
    public TextMeshProUGUI characterInfo;
    [Header("캐릭터 미 해금")]
    public GameObject lock_UI;
    public Text archivement_Text;

    public void TextSetting(int index, Sprite icon, bool isLocked)
    {
        unLock_UI.SetActive(false);
        lock_UI.SetActive(false);

        if(isLocked == true)
        {
            lock_UI.SetActive(true);

            PlayerAchivementInfoTable.Data import = GameManager.instance.gameDataBase.playerAchivementInfoTable.table[index];

            archivement_Text.text = import.characterAchivement_Text;
        }
        else
        {
            unLock_UI.SetActive(true);

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
}
