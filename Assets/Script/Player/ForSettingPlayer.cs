using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForSettingPlayer : Player,ICustomUpdateMono
{
    public Character index;
    public GameObject playerPrefab;

    MainSceneManager main;
    WeaponStatImporter importer;
    Image image;
    void Awake()
    {
        main = MainSceneManager.instance;
        //importer = WeaponStatImporter.instance;
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
        if (main.selectPlayer == gameObject)
        {
            image.color = new Color(200 / 255f, 200 / 255f, 200 / 255f);
        }
        else if (main.selectPlayer != gameObject)
        {
            image.color = new Color(70 / 255f, 70 / 255f, 70 / 255f);
        }
    }

    public void ClickPlayer()
    {
        if(main.selectedPlayer != null)
        {
            Destroy(main.selectedPlayer);
        }

        main.selectPlayer = gameObject;

        GameObject obj = Instantiate(playerPrefab);
        main.selectedPlayer = obj;
        obj.transform.SetParent(main.playerSetGroup);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.localScale = new Vector3(1, 1, 1);
    }
}
