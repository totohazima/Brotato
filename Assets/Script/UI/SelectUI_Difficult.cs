using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUI_Difficult : SelectUI
{
    private forSettingDifficult[] diffiicult_Buttons;
    void Awake()
    {
        diffiicult_Buttons = new forSettingDifficult[selectables.Length];
        for (int i = 0; i < selectables.Length; i++)
        {
            diffiicult_Buttons[i] = selectables[i].GetComponent<forSettingDifficult>();
        }
    }

    public override void NextMenu()
    {
        if (GameManager.instance.diffiicult == null)
        {
            Debug.Log("난이도 선택 필요");
        }
        else if (GameManager.instance.diffiicult != null)
        {
            LoadingSceneManager.LoadScene("Stage");
        }
    }

    public override void BeforeMenu()
    {
        Destroy(GameManager.instance.difficult_Obj);
        GameManager.instance.diffiicult = null;
        GameManager.instance.difficult_Obj = null;
        GameManager.instance.player_Obj.transform.SetParent(MainSceneManager.instance.weaponSetGroup);
        MainSceneManager.instance.weaponSettingMenu.SetActive(true);
        MainSceneManager.instance.difficultSettingMenu.SetActive(false);
    }
}
