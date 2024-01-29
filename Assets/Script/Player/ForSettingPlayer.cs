using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForSettingPlayer : Player
{
    public GameObject playerPrefab;

    MainSceneManager main;
    WeaponStatImporter importer;
    void Awake()
    {
        main = MainSceneManager.instance;
        importer = WeaponStatImporter.instance;

    }


    void Update()
    {
        
    }

    public void PlayerOn()
    {
        if(main.selectPlayer != null)
        {
            Destroy(main.selectPlayer);
        }
        GameObject obj = Instantiate(playerPrefab);
        main.selectPlayer = obj;
        obj.transform.SetParent(main.playerSetGroup);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.localScale = new Vector3(1, 1, 1);
    }
}
