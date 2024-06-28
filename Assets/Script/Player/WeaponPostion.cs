using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class WeaponPostion : MonoBehaviour, ICustomUpdateMono
{
    public Player_Action action;
    public Transform weaponMainPos;
    [HideInInspector] public Vector3 firstWeaponPos; //���Ⱑ 1���� ���
    [HideInInspector] public Vector3[] weaponPos;
    [HideInInspector] public Vector3[] weaponScannerPos;


    private void OnEnable()
    {
        CustomUpdateManager.customUpdates.Add(this);
    }

    private void OnDisable()
    {
         CustomUpdateManager.customUpdates.Remove(this);
    }

    /// <summary>
    /// ���� �����ʿ��� ������ ������� 0 ~ 17
    /// </summary>
    public void CustomUpdate()
    {
        Vector3 onePos = ConvertAngleToVector(-90, 1);
        firstWeaponPos = new Vector3(weaponMainPos.position.x + onePos.x, weaponMainPos.position.y + onePos.y, weaponMainPos.position.z + onePos.z);

        if (action.weapons.Count <= 6)
        {
            weaponPos = new Vector3[16];
            float[] deg = new float[weaponPos.Length];
            float dis;
            for (int i = 0; i < weaponPos.Length; i++)
            {
                deg[i] = i * 360f / weaponPos.Length;
                dis = 4;

                Vector3 pos = ConvertAngleToVector(-deg[i], dis);
                weaponPos[i] = new Vector3(weaponMainPos.position.x + pos.x, weaponMainPos.position.y + pos.y, weaponMainPos.position.z + pos.z);
            }

            weaponScannerPos = new Vector3[weaponPos.Length];
            float[] deg2 = new float[weaponScannerPos.Length];
            float dis2;
            for (int i = 0; i < weaponScannerPos.Length; i++)
            {
                deg2[i] = i * 360f / weaponScannerPos.Length;
                dis2 = 1;

                Vector3 pos = ConvertAngleToVector(-deg2[i], dis2);
                weaponScannerPos[i] = new Vector3(weaponMainPos.position.x + pos.x, weaponMainPos.position.y + pos.y, weaponMainPos.position.z + pos.z);
            }
        }
        else if (action.weapons.Count > 6)
        {
            weaponPos = new Vector3[action.weapons.Count];
            float[] deg = new float[weaponPos.Length];
            float dis;
            for (int i = 0; i < weaponPos.Length; i++)
            {
                deg[i] = i * 360f / weaponPos.Length;
                dis = 4;

                Vector3 pos = ConvertAngleToVector(-deg[i], dis);
                weaponPos[i] = new Vector3(weaponMainPos.position.x + pos.x, weaponMainPos.position.y + pos.y, weaponMainPos.position.z + pos.z);
            }

            weaponScannerPos = new Vector3[weaponPos.Length];
            float[] deg2 = new float[weaponScannerPos.Length];
            float dis2;
            for (int i = 0; i < weaponScannerPos.Length; i++)
            {
                deg2[i] = i * 360f / weaponScannerPos.Length;
                dis2 = 1;

                Vector3 pos = ConvertAngleToVector(-deg2[i], dis2);
                weaponScannerPos[i] = new Vector3(weaponMainPos.position.x + pos.x, weaponMainPos.position.y + pos.y, weaponMainPos.position.z + pos.z);
            }
        }
        
    }
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (firstWeaponPos != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(firstWeaponPos, 0.3f);
        }

        if (weaponPos.Length > 0)
        {
            for (int i = 0; i < weaponPos.Length; i++)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(weaponPos[i], 0.3f);
            }
        }

        if (weaponScannerPos.Length > 0)
        {
            for (int i = 0; i < weaponScannerPos.Length; i++)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(weaponScannerPos[i], 0.3f);
            }
        }
    }
#endif

    private Vector3 ConvertAngleToVector(float _deg, float dis)//������ ��ǥ ���ϱ�
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * dis, Mathf.Sin(rad) * dis, 0);
    }
}
