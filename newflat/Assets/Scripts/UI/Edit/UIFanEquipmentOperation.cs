using UnityEngine;
using System.Collections;
using DataModel;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// no use
/// </summary>
public class UIFanEquipmentOperation : MonoBehaviour
{
    public float newAngle;
    public float distance;
    public float hoverDistanceOffest;
    public float p = -90;

    /// <summary>大小变化速度</summary>
    public float increase = 0.18f;
    /// <summary>中继的最大的变化幅度，最后会默认回最大值1或最小值0</summary>
    public float turn = 1.5f;

    //	private bool isRotation=false;
    //	private float RotationSpeed=0.5f;

    private float changeTime = 0.3f;

    void Awake()
    {
        CalcAngle();

    }

    public void CalcAngle()
    {
        for (int a = 0; a < transform.childCount; a++)
        {
            float x1 = Mathf.Cos(newAngle * a * Mathf.Deg2Rad) * distance;
            float y1 = Mathf.Sin(newAngle * a * Mathf.Deg2Rad) * distance;
            Vector3 v = new Vector3(x1, y1, 0);
            transform.GetChild(a) .localPosition = v;
            Vector3 r = new Vector3(0, 0, p + a * 60);
            transform.GetChild(a).localEulerAngles = r;
            //if (go [a].GetComponent<UIButtonOffset>())
            //    Destroy(go [a].GetComponent<UIButtonOffset>());
            //UIButtonOffset offest = go [a].gameObject.AddComponent<UIButtonOffset>();
            //offest.hover = Calc(a * 60);
        }
    }

    /// <summary> 计算hover状态下的位置偏移 </summary>
    public Vector3 Calc(float hoverAngle)
    {
        Vector3 v = Vector3.zero;
        float x1 = Mathf.Cos(hoverAngle * Mathf.Deg2Rad) * hoverDistanceOffest;
        float y1 = Mathf.Sin(hoverAngle * Mathf.Deg2Rad) * hoverDistanceOffest;
        v.x = x1;
        v.y = y1;
        return v;
        
    }
}
