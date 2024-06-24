using UnityEngine;
using UnityEngine.UIElements;
using static Weapon;

public class RectangleGenerator : MonoBehaviour
{
    public GameObject weaponPrefab;
    public int weaponNum;

    private void Start()
    {
        WeaponAngleSetting();
    }
    private void WeaponAngleSetting()
    {
        float[] deg = new float[0];
        switch (weaponNum)
        {
            case 1:
                deg = new float[] { 90 };
                break;
            case 2:
                deg = new float[] { 45, 135 };
                break;
            case 3:
                deg = new float[] { 45, 135, 270 };
                break;
            case 4:
                deg = new float[] { 45, 135, 225, 315 };
                break;
            case 5:
                deg = new float[] { 45, 135, 200, 270, 340 };
                break;
            case 6:
                deg = new float[] { 45, 135, 180, 225, 315, 360 };
                break;
            default:
                deg = new float[weaponNum];
                for(int i = 0; i < weaponNum; i++)
                {
                    deg[i] = i * 360f / weaponNum;
                }
                break;
        }
        for (int i = 0; i < deg.Length; i++)
        {
            Vector3 pos = ConvertAngleToVector(-deg[i]);

            GameObject weapon = Instantiate(weaponPrefab, transform.position + pos, Quaternion.identity);
            weapon.transform.SetParent(transform);
        }

    }
    private Vector3 ConvertAngleToVector(float _deg)//각도로 좌표 구하기
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * 8f, Mathf.Sin(rad) * 8f, 0);
    }
}
