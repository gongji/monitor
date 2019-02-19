using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAreaColorSet : BaseSet
{
    protected void SetCamera()
    {
        Object3dItem item = SceneData.FindObjUtilityect3dItemById(SceneContext.areaBuiderId);
        GameObject root = SceneUtility.GetGameByRootName(item.number, item.number);
        GameObject cameraObject = FindObjUtility.GetTransformChildByName(root.transform, "camera");
        if (cameraObject != null)
        {
            // CameraInitSet.SetRotationCamera(box.transform, true);
            Camera.main.transform.position = cameraObject.transform.position;
            Camera.main.transform.rotation = cameraObject.transform.rotation;

        }

    }
}
