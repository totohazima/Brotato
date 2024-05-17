using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class forSettingDifficult : MonoBehaviour, ICustomUpdateMono
{
    public GameObject difficult_Prefab;
    GameManager game;
    Outline line;
    void Awake()
    {
        game = GameManager.instance;
        line = GetComponent<Outline>();
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
        if (game.difficult_Obj == difficult_Prefab)
        {
            line.effectColor = Color.white;
        }
        else if (game.difficult_Obj != difficult_Prefab)
        {
            line.effectColor = Color.black;
        }
    }

    public void ClickDifficult()
    {
        if (game.diffiicult != null)
        {
            Destroy(game.difficult_Obj);
        }

        game.diffiicult = difficult_Prefab.GetComponent<Difficult>();

        GameObject obj = Instantiate(difficult_Prefab);
        game.difficult_Obj = obj;
        obj.transform.SetParent(MainSceneManager.instance.difficultSetGroup);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.localScale = new Vector3(1, 1, 1);
    }
}
