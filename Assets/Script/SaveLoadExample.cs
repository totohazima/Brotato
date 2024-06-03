using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ES3Types;

public class SaveLoadExample : MonoBehaviour, UI_Upadte
{
    public static SaveLoadExample instance;
    public PlayerInfo playerInfo;
    public bool isSaved;
    public bool isLoaded;
    [HideInInspector] public string saveFilePath = "SceneSaveData.es3";

    void Awake()
    {
        instance = this;
        UIUpdateManager.uiUpdates.Add(this);
    }
    public void UI_Update()
    {
        if (ES3.KeyExists("isSave", saveFilePath) && isSaved == false)
        {
            isSaved = true;
        }
        else if (ES3.KeyExists("isSave", saveFilePath) == false && isSaved == true)
        {
            isSaved = false;
        }
    }

    /// <summary>
    /// 씬 데이터 저장
    /// </summary>
    public void SaveScene()
    {
        ES3.Save("isSave", true, saveFilePath);
        ES3.Save("PlayerInfo", playerInfo, saveFilePath);
        ES3.Save("Wave", StageManager.instance.waveLevel, saveFilePath);
        ES3.Save("Character", GameManager.instance.character, saveFilePath);
        /// <summary>
        /// 무기 데이터 저장
        /// </summary>
        ES3.Save("WeaponCount", GameManager.instance.player_Info.weapons.Count, saveFilePath);
        for (int i = 0; i < GameManager.instance.player_Info.weapons.Count; i++)
        {
            Weapon_Action weapon = GameManager.instance.player_Info.weapons[i].GetComponent<Weapon_Action>();

            ES3.Save($"Weapon_{i}_Code", weapon.index, saveFilePath);
            ES3.Save($"Weapon_{i}_Tier", weapon.weaponTier, saveFilePath);
        }

        /// <summary>
        /// 아이템 데이터 저장
        /// </summary>
        ES3.Save("ItemCount", GameManager.instance.player_Info.itemInventory.Count, saveFilePath);
        for (int i = 0; i < GameManager.instance.player_Info.itemInventory.Count; i++)
        {
            Item item = GameManager.instance.player_Info.itemInventory[i];

            ES3.Save($"Item_{i}_Code", item.itemType, saveFilePath);
            ES3.Save($"Item_{i}_Count", item.curCount, saveFilePath);
        }

        Debug.Log("Scene Data Saved");
    }

    /// <summary>
    /// 씬 데이터 로드
    /// </summary>
    public void LoadScene()
    {
        if (ES3.KeyExists("isSave", saveFilePath))
        {
            isLoaded = true;
            GameManager.instance.playerInfo = ES3.Load<PlayerInfo>("PlayerInfo", saveFilePath);
            Player.Character code = ES3.Load<Player.Character>("Character", saveFilePath);
            GameManager.instance.character = code;
            LoadingSceneManager.LoadScene("Stage");

            Debug.Log("Scene Data Loaded");
        }
        else
        {
            Debug.LogWarning("No saved scene data found.");
        }
    }

    public void DeleteData()
    {
        if (ES3.FileExists("SceneSaveData.es3"))
        {
            ES3.DeleteFile("SceneSaveData.es3");
        }
    }
}


