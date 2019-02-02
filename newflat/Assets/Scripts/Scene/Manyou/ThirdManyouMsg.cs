using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ThirdManyouMsg
{

    public static GameObject GetManYouPoint()
    {
        Object3dItem object3dItem = SceneContext.currentSceneData;

        if (object3dItem == null)
        {
            return null;
        }

        GameObject result = SceneUtility.GetGameByRootName(object3dItem.number, object3dItem.number);

        if (result == null)
        {
            return null;
        }

        GameObject personPoint = FindObjUtility.GetTransformChildByName(result.transform, Constant.Person_Point.ToLower());
        return personPoint;
    }
}
