using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmEventItem  {

    //设备id
    public string id;

    public string name;

    public string content;

    public string dateTime;

    //用于定位
    public string sceneId;

    //地点的名字
    public string stationName;

    public string level = "0";

    public string eventId = "";

    public string pointName;

    public string sceneName;

    //通道名字
    public string channelName;

    public string type;

    //动环原来的名称,在详情中显示
    public string eventSource;

    public string key;
}
