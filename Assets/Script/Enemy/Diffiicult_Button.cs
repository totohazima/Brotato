using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diffiicult_Button : MonoBehaviour, ICustomUpdateMono
{
    public GameObject difficult_Prefab;
    MainSceneManager main;
    Outline line;
    void Awake()
    {
        main = MainSceneManager.instance;
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
        if (main.selectDifficult == gameObject)
        {
            line.effectColor = Color.white;
        }
        else if (main.selectDifficult != gameObject)
        {
            line.effectColor = Color.black;
        }
    }

    public void ClickDifficult()
    {
        if (main.selectDifficult != null)
        {
            Destroy(main.selectedDifficult);
            main.selectedDifficult = null;
        }

        main.selectDifficult = gameObject;

        GameObject obj = Instantiate(difficult_Prefab);
        main.selectedDifficult = obj;
        obj.transform.SetParent(main.difficultSetGroup);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.localScale = new Vector3(1, 1, 1);
    }
}
