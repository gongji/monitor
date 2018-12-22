using DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public sealed class CameraViewData
{

    public static void SaveSceneCameraView()
    {
        CameraViewItem cvt = null;
        GetCurrentSceneCameraView((result) => {

            if(result!=null)
            {
                cvt = result;
                UpdateCameraView(ref cvt);
            }
            else
            {
                cvt = GetNewSceneCameraViewItem();
            }

            string reusltStr = CollectionsConvert.ToJSON(cvt);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("result", reusltStr);
            CameraViewProxy.SaveCameraview((rersult) =>
            {
                MessageBox.Show("信息提示", "保存成功");
            }, dic);
        });

    }
    public static void SaveEquipmentCameraView(string equipmentId)
    {
        CameraViewItem cvt = null;
        GetCurrentEquipmentCameraView((result) => {

            if(result==null)
            {
                cvt = GetNewEquipmentCameraViewItem(equipmentId);
            }
            else
            {
                cvt = result;
                UpdateCameraView(ref cvt);

            }

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("result", CollectionsConvert.ToJSON(cvt));
            Debug.Log(CollectionsConvert.ToJSON(dic));
            CameraViewProxy.SaveCameraview((rersult) =>
            {
                MessageBox.Show("信息提示", "保存成功");
            }, dic);



        }, equipmentId);
       
       
    }

    private static CameraViewItem GetNewSceneCameraViewItem()
    {
        CameraViewItem cvt = new CameraViewItem();
        cvt.x =  float.Parse( Camera.main.transform.position.x.ToString("f2"));
        cvt.y = float.Parse(Camera.main.transform.position.y.ToString("f2"));
        cvt.z = float.Parse(Camera.main.transform.position.z.ToString("f2"));
        cvt.rotationX = float.Parse(Camera.main.transform.eulerAngles.x.ToString("f2"));
        cvt.rotationY = float.Parse(Camera.main.transform.eulerAngles.y.ToString("f2"));
        cvt.rotationZ = float.Parse(Camera.main.transform.eulerAngles.z.ToString("f2"));
        if (SceneContext.currentSceneData!=null)
        {
            cvt.sceneId = SceneContext.currentSceneData.id;
        }
        else
        {
            cvt.sceneId = "-1";
        }
        return cvt;
    }


    private static CameraViewItem GetNewEquipmentCameraViewItem(string equipmentId)
    {
        CameraViewItem cvt = new CameraViewItem();
        cvt.x = float.Parse(Camera.main.transform.position.x.ToString("f2"));
        cvt.y = float.Parse(Camera.main.transform.position.y.ToString("f2"));
        cvt.z = float.Parse(Camera.main.transform.position.z.ToString("f2"));
        cvt.rotationX = float.Parse(Camera.main.transform.eulerAngles.x.ToString("f2"));
        cvt.rotationY = float.Parse(Camera.main.transform.eulerAngles.y.ToString("f2"));
        cvt.rotationZ = float.Parse(Camera.main.transform.eulerAngles.z.ToString("f2"));

        cvt.equipId = equipmentId;


        return cvt;
    }

    public static void UpdateCameraView(ref CameraViewItem cvt)
    {

        cvt.x = FormatUtil.FloatFomart(Camera.main.transform.position.x, 2);
        cvt.y = FormatUtil.FloatFomart(Camera.main.transform.position.y,2);
        cvt.z = FormatUtil.FloatFomart(Camera.main.transform.position.z,2);
        cvt.rotationX = FormatUtil.FloatFomart(Camera.main.transform.eulerAngles.x,2);
        cvt.rotationY = FormatUtil.FloatFomart(Camera.main.transform.eulerAngles.y,2);
        cvt.rotationZ = FormatUtil.FloatFomart(Camera.main.transform.eulerAngles.z,2);
        
    }

    public static void GetCurrentSceneCameraView(System.Action<CameraViewItem> callBack,System.Action error=null)
    {
        string sql = "";
        if (SceneContext.currentSceneData!=null)
        {
            sql = "sceneId = " + SceneContext.currentSceneData.id + " and (equipId is null or equipId = 0)";
        }
        else
        {
            sql = "sceneId = -1 and (equipId is null or equipId = 0)";
        }


        CallProxyGetViewData(sql, callBack);
    }

    public static void GetCameraViewBySceneId(System.Action<CameraViewItem> callBack, string sceneId,System.Action error = null)
    {
        
         string  sql = "sceneId = " + sceneId + " and (equipId is null or equipId = 0)";
        CallProxyGetViewData(sql, callBack);
    }

    public static void GetCurrentEquipmentCameraView(System.Action<CameraViewItem> callBack,string equipmentId)
    {
        string sql = "equipId = " + equipmentId + " and (sceneId is null or sceneId = -1)";
        CallProxyGetViewData(sql, callBack);
    }

    public static void CallProxyGetViewData(string sql, System.Action<CameraViewItem> callBack)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("result", sql); ;

        CameraViewProxy.GetCameraView(dic, (result) =>
        {
            CameraViewItem resultCameraViewItem = null;
            if (!string.IsNullOrEmpty(result))
            {
               // Debug.Log(result);
                List<CameraViewItem>  list = Utils.CollectionsConvert.ToObject<List<CameraViewItem>>(result);
                if(list.Count ==1)
                {
                    resultCameraViewItem = list[0];
                }
            }
            if (callBack != null)
            {
                callBack.Invoke(resultCameraViewItem);
            }

        }, (error) => { });
    }

}
