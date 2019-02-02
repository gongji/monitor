using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DataModel;
using SystemCore.Task;
using Core.Common.Logging;
using Utils;

public sealed class EquipmentData {

    private static ILog log = LogManagers.GetLogger("EquipmentData");


    #region allEquipmentData
    //save equipment all data
    private static Dictionary<string, GameObject> allEquipmentDataDic = new Dictionary<string, GameObject>();
    public static Dictionary<string, GameObject> GetAllEquipmentData
    {
        get
        {
            return allEquipmentDataDic;
        }
    }


    public static  void RemoveDeleteEquipment(string id)
    {
        allEquipmentDataDic.Remove(id);
    }

    /// <summary>
    /// 
    /// </summary>
    public static void SetAllEquipmentParentEmpty()
    {
        foreach(GameObject item in allEquipmentDataDic.Values)
        {
            Object3DElement object3DElement = item.GetComponent<Object3DElement>();
            //
            if (item.activeSelf && object3DElement!=null && object3DElement.equipmentData!=null && 
                string.IsNullOrEmpty(object3DElement.equipmentData.modelId))
            {
                item.transform.SetParent(null);
                item.gameObject.SetActive(false);
            }
            
        }
    }

    #endregion allEquipmentData
    

    private static List<EquipmentItem> currentEquipmentData = null;

    public static List<EquipmentItem> GetCurrentEquipment
    {
        get
        {
            return currentEquipmentData;
        }
    }
  
   
    public static void SearchCurrentEquipmentDataDownModel(System.Action callBack)
    {
        string sql = GetEquipmentSqlByParent();
        if(string.IsNullOrEmpty(sql))
        {
            callBack.Invoke();
            return;
        }
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("result", sql);

       // Debug.Log(sql);
        Equipment3dProxy.SearchEquipmentData((result) =>
        {
            Debug.Log(result);
            if (string.IsNullOrEmpty(result) && callBack != null)
            {
                callBack.Invoke();
                return;
             }
           // Debug.Log(result);
            currentEquipmentData = CollectionsConvert.ToObject<List<EquipmentItem>>(result);

            
            DownLoadModel(currentEquipmentData, callBack);
        }, dic,null);
    }


    public static  void DownLoadModel(List<EquipmentItem> currentEquipmentData,System.Action callBack)
    {
        if(currentEquipmentData==null || currentEquipmentData.Count==0)
        {
            return;
        }
        string[] modelids = GetModelList(currentEquipmentData);
        if (modelids.Length > 0)
        {
            DownLoader.Instance.StartModelDownLoad(modelids, () => {

                if (callBack != null)
                {
                    callBack.Invoke();
                }
            });
        }
        else
        {
            if (callBack != null)
            {
                callBack.Invoke();
            }
        }
    }
  
    private static string  GetEquipmentSqlByParent()
    {

        Object3dItem object3dItem = SceneContext.currentSceneData;
        IState curerState = AppInfo.GetCurrentState;
        List<Object3dItem> list = new List<Object3dItem>();
        string sql = "";
        if(curerState is ColorAreaState || curerState is FullAreaState)
        {
            sql =  "";
        }
        else if(curerState  is AreaState)
        {
            string sceneid =SceneData.GetIdByNumber(Constant.Main_dxName);
            sql = "sceneId = " + sceneid;
        }
        else if(curerState is RoomState)
        {
            sql = "sceneId = " + object3dItem.id + " and type <>'De_Door'";
        }
        else if(curerState is FloorState)
        {
            list.Add(object3dItem);
            list.AddRange(object3dItem.childs);
            sql = GetParentStr(list);
        }
        else if(curerState is BuilderState)
        {
            list.AddRange(object3dItem.childs);
            foreach(Object3dItem floor in object3dItem.childs)
            {
                list.AddRange(floor.childs);
            }
            sql = GetParentStr(list); 

        }
        return sql;
        
    }

    private static string GetParentStr(List<Object3dItem> list)
    {
        List<string> ids = new List<string>();
        foreach(Object3dItem item in list)
        {
            ids.Add(item.id);
        }

        string connectStr =  Utils.StrUtil.ConnetString(ids, ",");

        return "sceneId in(" + connectStr + ")" + " and type<>'De_Door'";

    }

   
    /// <summary>
    /// 得到模型下载列表，通过equipmentData数据。
    /// </summary>
    /// <returns></returns>
    public static string[] GetModelList(List<EquipmentItem> equipmentDataList)
    {
       
        List<string> modelist = new List<string>();
        var list = equipmentDataList.GroupBy(x => new { x.modelId }).Select(group => new
        {
            group.Key.modelId
        }).ToList();

        foreach (var a in list)
            
        {
            if(!string.IsNullOrEmpty(a.modelId) && !ModelData.modelPrefebDic.ContainsKey(a.modelId) && !a.modelId.Equals("0"))
            {
                modelist.Add(a.modelId);
            }
           
        }
        return modelist.ToArray();
       
    }
   
   
    public static GameObject FindGameObjectById(string id)
    { 
        GameObject result = null;
        allEquipmentDataDic.TryGetValue(id, out result);

        return result;
    }




}
