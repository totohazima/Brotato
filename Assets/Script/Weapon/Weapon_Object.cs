using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Object : MonoBehaviour, ICustomUpdateMono
{
    public Weapon_Action weapon_Object;
    public Image backGround;
    public Image weapon_Image;
    public GameObject combined_Mark;
    [SerializeField]
    WeaponScrip[] weaponData;
    [SerializeField]
    Outline frame;
    List<Weapon_Action> weaponList = new List<Weapon_Action>();

    void OnEnable() //������ Ƽ� ���Ѵ� (���� 1Ƽ� ����)
    {
        CustomUpdateManager.customUpdates.Add(this);
        for (int i = 0; i < GameManager.instance.playerInfo.weapons.Count; i++)
        {
            weaponList.Add(GameManager.instance.playerInfo.weapons[i].GetComponent<Weapon_Action>());
        }
    }
    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public void CustomUpdate()
    {
        switch(weapon_Object.weaponTier)
        {
            case 0:
                backGround.color = new Color(48 / 255f, 48 / 255f, 48 / 255f);
                break;
            case 1:
                backGround.color = new Color(80 / 255f, 120 / 255f, 150 / 255f);
                break;
            case 2:
                backGround.color = new Color(100 / 255f, 60 / 255f, 110 / 255f);
                break;
            case 3:
                backGround.color = new Color(125 / 255f, 60 / 255f, 60 / 255f);
                break;
        }
        weapon_Image.sprite = weaponData[(int)weapon_Object.index].weaponImage;

        for (int i = 0; i < weaponList.Count; i++)
        {
            Weapon_Action weapon = weaponList[i];
            if (weapon != weapon_Object)
            {
                if (weapon_Object.index == weapon.index && weapon_Object.weaponTier == weapon.weaponTier && weapon_Object.weaponTier < 3)
                {
                    combined_Mark.SetActive(true);
                    break;
                }
            }

            combined_Mark.SetActive(false);

        }
    }

    public void PointDown()
    {
        frame.effectColor = Color.white;
        ShowItemInfo();
    }
    public void PointUp()
    {
        //Destroy(infoObj.gameObject);
        frame.effectColor = Color.black;
    }
    public void PointClick()
    {
        int itemCount = transform.parent.childCount;
        Outline[] line = new Outline[itemCount];
        Transform content = transform.parent;
        for (int i = 0; i < itemCount; i++)
        {
            line[i] = content.GetChild(i).GetComponent<Outline>();
            line[i].effectColor = Color.black;
        }
        frame.effectColor = Color.white;
    }
    private void ShowItemInfo()//Ŭ�� �� ������ ������ �����ִ� �뵵
    {
        //Ŭ�� �ϰ� ���� �� �������� �Ͼ� �׵θ��� ���´�
        //Ŭ�� �߿��� itemGoods�� ������ UI�� ��Ÿ����(����, ��ݹ�ư ����)
        //UI�� �߽��� �������� x�� +�� �������� y�� +�� ������ �Ʒ��� �����Ѵ�. (�ݴ��� ��쿣 ���ݴ�� ����)
        //Ŭ�� ���� �� �Ͼ� �׵θ��� ���� UI�� ������.
        //infoObj = Instantiate(itemInfo, GameManager.instance.itemInfoManager);
        //infoObj.Init(itemType.ToString(), itemImage.sprite, (int)itemType, transform.position);
    }
}
