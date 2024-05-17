using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForSettingPlayer : Player,ICustomUpdateMono
{
    public Character index;
    public GameObject playerPrefab;
    public Image icon;
    GameManager game;
    Image image;
    void Awake()
    {
        game = GameManager.instance;
        image = GetComponent<Image>();

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
    }

    public void ClickPlayer()
    {
        if(game.player != null)
        {
            Destroy(game.player_Obj);
        }

        game.player = this;

        GameObject obj = Instantiate(playerPrefab);
        game.player_Obj = obj;
        obj.transform.SetParent(MainSceneManager.instance.playerSetGroup);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.localScale = new Vector3(1, 1, 1);

        SelectableCharacter character = obj.GetComponent<SelectableCharacter>();
        character.TextSetting((int)index, icon.sprite);
    }
}
