using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForSettingPlayer : Player,ICustomUpdateMono
{
    public Character index;
    public GameObject playerPrefab;
    public Image icon;
    public Image lockIcon;
    public bool isLock;
    GameManager game;
    Image image;
    void Awake()
    {
        game = GameManager.instance;
        image = GetComponent<Image>();

        PlayerAchivementInfoTable.Data import = GameManager.instance.gameDataBase.playerAchivementInfoTable.table[(int)index];
        switch(import.isEarlyUnlock)
        {
            case true:
                isLock = false;
                break;
            case false:
                isLock = true;
                break;
        }

        if (isLock == true)
            StatSetting(0);
        else
            StatSetting((int)index);
    }
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public void CustomUpdate()
    {
        if (game.player == this)
        {
            image.color = new Color(200 / 255f, 200 / 255f, 200 / 255f);
        }
        else if (game.player != this)
        {
            image.color = new Color(70 / 255f, 70 / 255f, 70 / 255f);
        }

        if(isLock == true && lockIcon.gameObject.activeSelf == false)
        {
            lockIcon.gameObject.SetActive(true);
            icon.gameObject.SetActive(false);
        }
        else if(isLock == false && icon.gameObject.activeSelf == false)
        {
            lockIcon.gameObject.SetActive(false);
            icon.gameObject.SetActive(true);
        }
    }

    public void ClickPlayer()
    {
        //if(game.player != null)
        //{
        //    Destroy(game.player_Obj);
        //}
        Destroy(game.player_Obj);
        //캐릭터 해금된 경우
        if (isLock == false)
        {
            game.player = this;

            GameObject obj = Instantiate(playerPrefab);
            game.player_Obj = obj;
            obj.transform.SetParent(MainSceneManager.instance.playerSetGroup);

            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.localScale = new Vector3(1, 1, 1);

            SelectableCharacter character = obj.GetComponent<SelectableCharacter>();
            character.TextSetting((int)index, icon.sprite, isLock);
        }
        //캐릭터 해금되지 않은 경우
        else
        {
            game.player = null;

            GameObject obj = Instantiate(playerPrefab);
            game.player_Obj = obj;
            obj.transform.SetParent(MainSceneManager.instance.playerSetGroup);

            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.localScale = new Vector3(1, 1, 1);

            SelectableCharacter character = obj.GetComponent<SelectableCharacter>();
            character.TextSetting((int)index, icon.sprite, isLock);
        }
    }
}
