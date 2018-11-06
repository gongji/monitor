using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IiSet  {

    void Enter(List<Object3dItem> currentData,System.Action callBack);
    //带动画的退出
    void Exit(string nextid, System.Action callBack);
    //不带动画，直接退出
    void Exit(string nextid);
}
