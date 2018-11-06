using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class FlyTextMeshModel : MonoBehaviour,IFontModel
{
    // Use this for initialization
    private GameObject fontModelGo;
    private Material fontMaterial;
    //是否缩放 
    public bool yRotate = true;
    public bool xRotate = true;
    public bool zRotate = true;

    private Vector3 defaultScale = Vector3.zero;
   
    void FixedUpdate()
    {
        LookAtCamera();
    }

    /// <summary>
    /// 生成的模型
    /// </summary>
    /// <param name="text"></param>
    /// <param name="postion"></param>
    /// <returns></returns>
    public void Create(string text,  Vector3 postion,Transform parentBox)
    {
       
    }

  
    private void DestroyModel()
    {
        GameObject.Destroy(fontModelGo);
        fontMaterial = null;
    }

    private MeshRenderer[] meshRenders;
    /// <summary>
    /// 始终面向相机
    /// </summary>
    private void LookAtCamera()
    {
        //if(!isRoom)
        //{
        //    if (CameraController.currentCamera.transform.position - transform.position != Vector3.zero)
        //    {
        //        transform.forward = CameraController.currentCamera.transform.position - transform.position;
        //        Vector3 eulerAngles = transform.eulerAngles;
        //        float scaleX = transform.parent.localScale.x;
        //        float scaleY = transform.parent.localScale.y;
        //        float scaleZ = transform.parent.localScale.z;

        //        int transformOffestX;
        //        transformOffestX = transform.localScale.x < 0 ? -1 : 1;
        //        int transformOffestY;
        //        transformOffestY = transform.localScale.y < 0 ? -1 : 1;
        //        int transformOffestZ;
        //        transformOffestZ = transform.localScale.z < 0 ? -1 : 1;
        //        if (scaleX < 0)
        //            eulerAngles.y = eulerAngles.y * -1 * transformOffestY;
        //        if (scaleY < 0)
        //            eulerAngles.z = eulerAngles.z * -1 * transformOffestZ;
        //        if (scaleZ < 0)
        //            eulerAngles.x = eulerAngles.x * -1 * transformOffestX;
        //        transform.eulerAngles = eulerAngles;
        //    }
           
        //}
        //else
        //{
        //    Object3dUtil.LookAtCamera(transform.gameObject, xRotate, yRotate, zRotate, CameraController.currentCamera);
        //}

        //if (meshRenders == null || (meshRenders != null && meshRenders.Length == 0))
        //{
        //    meshRenders = gameObject.GetComponentsInChildren<MeshRenderer>(true);
        //}
        //float distance = Vector3.Distance(gameObject.transform.position, CameraController.currentCamera.transform.position);

        //if (element!=null && element.modelCategory == Object3DElement.Type.Syou)
        //{
            
        //    bool isShow = true;
        //    if (distance < 6.0f || Main.instance.stateManager.CurrentStatus is Map3rdBrowseStates)
        //    {
        //        isShow = false;
        //    }

        //    for (int i = 0; i < meshRenders.Length; i++)
        //    {
        //        meshRenders[i].enabled = isShow;
        //    }
        //}
        
        //distance /= 1000.0f;
        //distance = Mathf.Clamp01(distance);
        // transform.localScale = Vector3.Lerp(FontModelMgr.Instance.MinLoacalScale, FontModelMgr.Instance.MaxLocalScale, distance);
    }

    /// <summary>
    /// 获取三个相量的绝对值的最小值
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    private float getMixValue(Vector3 v)
    {
        float minValue = Mathf.Abs(v.x);

        if (Mathf.Abs(v.y) < minValue)
        {
            minValue = Mathf.Abs(v.y);
        }


        if (Mathf.Abs(v.z) < minValue)
        {
            minValue = Mathf.Abs(v.z);
        }
        return minValue;

    }

    //private FlashingController flashing;
    /// <summary>
    /// 设置悬停效果
    /// </summary>
    public void PlayScaleEffection()
    {
        if (defaultScale==Vector3.zero)
        {
            defaultScale = transform.localScale;
        }
        transform.localScale = defaultScale * 1.5f;
        
    }

    /// <summary>
    /// 停止悬停效果
    /// </summary>
    public void StopScaleEffection()
    {
        if (defaultScale == Vector3.zero)
        {
            defaultScale = transform.localScale;
        }

        transform.localScale = defaultScale;
    }

    /// <summary>
    /// 进行定位
    /// </summary>
    public void Locate()
    {
        //if (element!=null && element.ModelData != null && !string.IsNullOrEmpty(element.ModelData.id))
        //{
        //    DataModel.LocateBedCard locateObject = new LocateBedCard();
        //    locateObject.id = element.ModelData.id;
        //    locateObject.parentId = element.ModelData.parentId;
        //    locateObject.isExitSystem = false;
        //    locateObject.url = "";
        //    EventMgr.Instance.SendEvent(EventName.BROWSE_LOCATE, locateObject);
        //}
        
    }

    
}
