using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Weapon_Info : MonoBehaviour
{
    public Weapon.Weapons weaponCode;   
    public Transform weaponInfoUI;
    private WeaponScrip weaponScrip;
    private Weapon_Action weaponInfo;
    private RectTransform rectTrans;

    public Image backGround;
    public Outline Frame;
    public Image weaponImage;
    public Text weaponName;
    public Text weaponType;

    private Vector3 itemPos;

    public void Init(WeaponScrip scrip, Weapon_Action weapon_Action, Vector3 pos)
    {
        weaponScrip = scrip;
        weaponInfo = weapon_Action;

        weaponCode = weaponScrip.weaponNickNames;
        weaponImage.sprite = weaponScrip.weaponImage;
        itemPos = pos;
        StartCoroutine(WeaponInfoSetting());
    }

    public IEnumerator WeaponInfoSetting()
    {
        switch(weaponInfo.weaponTier)
        {
            case 0:
                backGround.color = Color.black;
                Frame.effectColor = Color.black;
                weaponName.color = Color.white;
                break;
            case 1:
                backGround.color = new Color(5 / 255f, 25 / 255f, 40 / 255f);
                Frame.effectColor = new Color(90 / 255f, 175 / 255f, 250 / 255f);
                weaponName.color = new Color(90 / 255f, 175 / 255f, 250 / 255f);
                break;
            case 2:
                backGround.color = new Color(20 / 255f, 10 / 255f, 45 / 255f);
                Frame.effectColor = new Color(185 / 255f, 90 / 255f, 250 / 255f);
                weaponName.color = new Color(185 / 255f, 90 / 255f, 250 / 255f);
                break;
            case 3:
                backGround.color = new Color(45 / 255f, 10 / 255f, 10 / 255f);
                Frame.effectColor = new Color(250 / 255f, 60 / 255f, 75 / 255f);
                weaponName.color = new Color(250 / 255f, 60 / 255f, 75 / 255f);
                break;
        }




        rectTrans = weaponInfoUI.GetComponent<RectTransform>();
        yield return new WaitForSeconds(0.01f);
        float x = 0;
        float y = 0;
        if (Camera.main.ScreenToWorldPoint(itemPos).x >= 0)
        {
            x = itemPos.x - 120;
        }
        else if (Camera.main.ScreenToWorldPoint(itemPos).x < 0)
        {
            x = itemPos.x + 120;
        }

        if (Camera.main.ScreenToWorldPoint(itemPos).y >= 0)
        {
            y = itemPos.y - 160 - rectTrans.rect.height;
        }
        else if (Camera.main.ScreenToWorldPoint(itemPos).y < 0)
        {
            y = itemPos.y + 160 + rectTrans.rect.height;

        }
        transform.position = new Vector3(x, y);
    }
}
