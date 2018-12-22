using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SubsystemMsg {

    private static GameObject systemUI = null;
    private static ILog log = LogManagers.GetLogger("StateMachineManager");

    public static void Create(string sceneid)
    {
        if(AppInfo.Platform == BRPlatform.Editor)
        {
            return;
        }
        SubSystemProxy.GetSubSystemByScene((result) => {
            if(string.IsNullOrEmpty(result))
            {
                log.Debug("sceneid="+ sceneid  + "is empty");
                return;
            }
            Debug.Log("resultSubSystem=" + result);

            List<SubSystemItem> data = Utils.CollectionsConvert.ToObject<List<SubSystemItem>>(result);
            systemUI = TransformControlUtility.CreateItem("UI/UISubSystem/SubjetSystemTree", UIUtility.GetRootCanvas());
            systemUI.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            if (systemUI!=null && systemUI.GetComponent<TreeManager>()!=null)
            {
                systemUI.GetComponent<TreeManager>().Init(data);
            }

        }, sceneid);
    }

    public static void Delete()
    {
        if(systemUI!=null)
        {
            GameObject.DestroyImmediate(systemUI);
        }
    }
}
