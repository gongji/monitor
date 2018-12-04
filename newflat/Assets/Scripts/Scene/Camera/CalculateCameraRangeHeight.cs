using DataModel;
using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 计算相机的范围的高度
/// </summary>
public static class CaluteCameraRangeHeight  {

    public static float multiple = 3.0f;
    public static float GetCameraHeight()
    {
        IState mCurrentState = Main.instance.stateMachineManager.mCurrentState;
        //园区和全景模式
        if (mCurrentState is AreaState || mCurrentState is FullAreaState)
        {

            List<Object3dItem> list =  SceneData.GetAllWq();
            return FindMaxWQ(list);

        }
        //房间和楼层
        else if(mCurrentState is FloorState || mCurrentState is RoomState)
        {
            return  SceneContext.sceneBox.GetComponent<BoxCollider>().bounds.size.y * multiple;
           
        }
        return 0.0f;
    }

    private static float FindMaxWQ(List<Object3dItem> list)
    {
        float maxY = 0.0f;
        foreach(Object3dItem item in list)
        {
            GameObject root = SceneUtility.GetGameByRootName(item.number, item.number);
            GameObject box = FindObjUtility.GetTransformChildByName(root.transform, Constant.ColliderName);

            if (box.GetComponent<BoxCollider>()!=null)
            {
                BoxCollider b = box.GetComponent<BoxCollider>();
                if(b.bounds.size.y> maxY)
                {
                    maxY = b.bounds.size.y;
                }
            }
        }

       // Debug.Log("maxY="+ maxY * multiple);
        return maxY*multiple;


    }
}
