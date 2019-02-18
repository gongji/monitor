using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BrowserUI  {

    public static void Create()
    {
        //创建报警事件
       GameObject alarmEvent =   TransformControlUtility.CreateItem("AlarmEvent", UIUtility.GetRootCanvas());
       alarmEvent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        //创建工具栏

        GameObject browsertoolBar = TransformControlUtility.CreateItem("browsertoolBar", UIUtility.GetRootCanvas());
        browsertoolBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-63.39f);
        browsertoolBar.transform.localScale = Vector3.zero;
    }
}
