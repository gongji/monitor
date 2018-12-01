﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentTestPoint  {

    public string id;
    //测点编号
    public List<TestPointItem> data;
}


public  class TestPointItem
{
    public string number;
    //测点名称
    public string name;
    public string value;
    public string unit;
    //状态
    public string state;
}
