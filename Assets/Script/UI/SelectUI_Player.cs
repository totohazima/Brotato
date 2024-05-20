using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUI_Player : SelectUI
{
    private ForSettingPlayer[] forSettingPlayers;
    void Awake()
    {
        forSettingPlayers = new ForSettingPlayer[selectables.Length];
        for (int i = 0; i < selectables.Length; i++)
        {
            forSettingPlayers[i] = selectables[i].GetComponent<ForSettingPlayer>();
        }
    }

    public override void RandomSelect() //랜덤으로 플레이어 선택
    {
        int i = Random.Range(1, forSettingPlayers.Length);
        ForSettingPlayer setting = forSettingPlayers[i];
        setting.ClickPlayer();
    }
    public override void NextMenu()
    {
        if (GameManager.instance.player == null) //경고
        {
            Debug.Log("캐릭터 선택 필요");
        }
        else if (GameManager.instance.player != null)
        {
            //황소: 무기 탑재 불가
            if (GameManager.instance.character == Player.Character.BULL)
            {
                GameManager.instance.player_Obj.transform.SetParent(MainSceneManager.instance.difficultSetGroup);
                MainSceneManager.instance.difficultSettingMenu.SetActive(true);
                MainSceneManager.instance.playerSettingMenu.SetActive(false);
            }
            else
            {
                GameManager.instance.player_Obj.transform.SetParent(MainSceneManager.instance.weaponSetGroup);
                MainSceneManager.instance.weaponSettingMenu.SetActive(true);
                MainSceneManager.instance.playerSettingMenu.SetActive(false);
            }
        }
    }
    public override void BeforeMenu()
    {
        Destroy(GameManager.instance.player_Obj);
        GameManager.instance.player = null;
        GameManager.instance.player_Obj = null;
        MainSceneManager.instance.playerSettingMenu.SetActive(false);
    }

}
