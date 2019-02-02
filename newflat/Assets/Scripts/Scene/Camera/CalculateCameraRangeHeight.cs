using DataModel;
using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CaluteCameraRangeHeight  {

    public static float multiple = 3.0f;
    public static float GetCameraHeight()
    {
        IState mCurrentState = Main.instance.stateMachineManager.mCurrentState;

        if (mCurrentState is AreaState || mCurrentState is ColorAreaState)
        {

            List<Object3dItem> list = SceneData.GetAllWq();
            return FindMaxWQ(list);

        }
        else if (mCurrentState is FloorState || mCurrentState is RoomState)
        {
            return SceneContext.sceneBox.GetComponent<BoxCollider>().bounds.size.y * multiple;

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
