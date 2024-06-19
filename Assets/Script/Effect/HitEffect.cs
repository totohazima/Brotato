using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitEffect : MonoBehaviour, ICustomUpdateMono
{
    private ParticleSystem hitEffect;

    void OnEnable()
    {
        hitEffect = GetComponent<ParticleSystem>();
        CustomUpdateManager.customUpdates.Add(this);
    }

    void OnDisable()
    {
        CustomUpdateManager.customUpdates.Remove(this);
    }

    public void CustomUpdate()
    {
        if(hitEffect.isPlaying == false)
        {
            gameObject.SetActive(false);
        }
    }
}
