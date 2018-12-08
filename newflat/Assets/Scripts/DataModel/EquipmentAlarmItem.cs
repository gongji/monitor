using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentAlarmItem  {

    public string id;

    //故障（0），报警（1），无效（3），正常（4）
    public int state = 0;

    //如果是漏水绳，漏水的段数。
    public int segments;
}
