using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public sealed class CameraViewEdit  {

    public static void SaveSceneCameraView()
    {
        CameraViewItem cvt = GetCameraViewItem();
        cvt.sceneId = SceneContext.currentSceneData.id;
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("result", CollectionsConvert.ToJSON(cvt));
        CameraViewProxy.SaveCameraview((rersult) =>
        {
            MessageBox.Show("信息提示","保存成功");
        }, dic);
    }
    public static void SaveEquipmentCameraView(string equipmentId)
    {
        CameraViewItem cvt = GetCameraViewItem();
        cvt.equipId = equipmentId;
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("result", CollectionsConvert.ToJSON(cvt));
        CameraViewProxy.SaveCameraview((rersult) =>
        {
            MessageBox.Show("信息提示", "保存成功");
        }, dic);
    }

    private static CameraViewItem GetCameraViewItem()
    {
        CameraViewItem cvt = new CameraViewItem();
        cvt.x = Camera.main.transform.position.x;
        cvt.y = Camera.main.transform.position.y;
        cvt.z = Camera.main.transform.position.z;
        cvt.rotationX = Camera.main.transform.eulerAngles.x;
        cvt.rotationY = Camera.main.transform.eulerAngles.y;
        cvt.rotationZ = Camera.main.transform.eulerAngles.z;
        return cvt;
    }

    public static void GetCurrentSceneCameraView(System.Action<CameraViewItem> callBack)
    {
        string sql = "sceneId = "+SceneContext.currentSceneData.id;

        CallProxyGetData(sql, callBack);
    }

    private static void GetCurrentEquipmentCameraView(System.Action<CameraViewItem> callBack,string equipmentId)
    {
        string sql = "equipId = " + SceneContext.currentSceneData.id;
        CallProxyGetData(sql, callBack);
    }

    private static void CallProxyGetData(string sql, System.Action<CameraViewItem> callBack)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("result", sql); ;

        CameraViewProxy.GetCameraView(dic, (result) =>
        {
            CameraViewItem resultCameraViewItem = null;
            if (!string.IsNullOrEmpty(result))
            {
                resultCameraViewItem = Utils.CollectionsConvert.ToObject<CameraViewItem>(result);
            }
            if (callBack != null)
            {
                callBack.Invoke(resultCameraViewItem);
            }

        }, (error) => { });
    }

}
