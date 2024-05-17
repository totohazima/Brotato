using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUI : MonoBehaviour
{
    public GameObject[] selectables;

    public virtual void RandomSelect()
    {
        return;
    }
    public virtual void NextMenu()
    {
        return;
    }

    public virtual void BeforeMenu()
    {
        return;
    }
}
