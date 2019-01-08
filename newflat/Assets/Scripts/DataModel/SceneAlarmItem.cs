using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAlarmItem  {

    public string id;

    //故障（0）（1） 正常
    public int state = 0;

    public string number;

    public SceneAlarmItem(string id,int state,string number)
    {
        this.id = id;
        this.state = state;
        this.number = number;
    }

    public SceneAlarmItem()
    {

    }


}
