using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    Vector3 startPos;
    void OnEnable()
    {
        StartCoroutine(TextAnim());
    }

    private IEnumerator TextAnim()
    {
        yield return new WaitForSeconds(0.05f);
        startPos = transform.position;
        Vector3 endPos = new Vector3(startPos.x, startPos.y - 7);
        LeanTween.move(gameObject, endPos, 0.35f).setEase(LeanTweenType.easeOutBounce);
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }
}
