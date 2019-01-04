using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 设备控制点
/// </summary>
public class EquipmentControlPointItem
{

    //测点编号
    public string id;

    //名称
    public string name;

    //描述
    public string describe;

    //类型,0为字符串，1为枚举类型，2 为数值型
    public int type = 0;

    //type=1 枚举类型时候，显示枚举的值。
    public string[] values;


    //开始范围和结束范围值，仅当type =2 ，数值型的时候有效
    public float StartValue=0.0f;

    public float endValue = 1000.0f;


}
