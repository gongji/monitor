using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentAnimationItem
{
    //设备的id
    public string id;

    public List<AnimationItem> animation;
}

public class AnimationItem
{
    //动画的名字
    public string name;

    //是否执行
    public bool isRun = false;
}


