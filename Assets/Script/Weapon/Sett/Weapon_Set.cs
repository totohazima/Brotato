using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Weapon_Set : MonoBehaviour, ICustomUpdateMono
{
    public Text[] texts;
    [HideInInspector] public GameManager game;
    void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
        game = GameManager.instance;
    }

    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public virtual void CustomUpdate()
    {
        return;
    }
}
