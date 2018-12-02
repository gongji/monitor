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
    /// <summary>
    /// 模型id，和模型的字典
    /// </summary>
    public static Dictionary<string, GameObject> modelPrefebDic = new Dictionary<string, GameObject>();

    public static Dictionary<string, GameObject> GetmodelPrefebDic
    {
        get
        {
            return modelPrefebDic;
        }
    }

    //保存所有的设备数据
    public static Dictionary<string, GameObject> allEquipmentDataDic = new Dictionary<string, GameObject>();

    /// 当前场景对象的设备数据

    private static List<EquipmentItem> currentEquipmentData = null;

    public static List<EquipmentItem> GetCurrentEquipment
    {
        get
        {
            return currentEquipmentData;
        }
    }
  
    private static List<EquipmentItem> equipmentItemList;
    public static void SearchCurrentEquipmentData(System.Action callBack)
    {
        string sql = GetEquipmentSqlByParent();
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("result", sql);

        Equipment3dProxy.SearchEquipmentData((result) =>
        {
            if(string.IsNullOrEmpty(result)&& callBack!=null)
            {
                callBack.Invoke();
                return;
            }
            currentEquipmentData = CollectionsConvert.ToObject<List<EquipmentItem>>(result);

            //获取模型列表
            string[] modelids =  GetModelList(currentEquipmentData);
            if(modelids.Length>0)
            {
                DownLoader.Instance.StartModelDownLoad(modelids,()=> {

                   if(callBack!=null)
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
        }, dic);
    }
    /// <summary>
    /// 通过父节点得到设备查询的sql语句 
    /// </summary>
    /// <param name="parentid"></param>
    /// <param name="callBack"></param>
    private static string  GetEquipmentSqlByParent()
    {

        Object3dItem object3dItem = SceneContext.currentSceneData;
        IState curerState = AppInfo.GetCurrentState;
        List<Object3dItem> list = new List<Object3dItem>();
        string sql = "";
        //全景模式不需要得到设备
        if(curerState is FullAreaState)
        {
            sql =  "";
        }
        else if(curerState  is AreaState)
        {
            sql =  "parentsId is null";
        }
        else if(curerState is RoomState)
        {
            sql =  "parentsId is " + object3dItem.id;
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

        return "parentsId in(" + connectStr + ")";

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
            if(!string.IsNullOrEmpty(a.modelId) && !modelPrefebDic.ContainsKey(a.modelId))
            {
                modelist.Add(a.modelId);
            }
           
        }
        return modelist.ToArray();
       
    }
    /// <summary>
    /// 下载完成后，更新model字典
    /// </summary>
    /// <param name="abTask"></param>
    public static void  UpdateModelDic(Dictionary<string, ABModelDownloadTask> abTask)
    {
        if(abTask.Count==0)
        {
            return;
        }
        foreach(string key in abTask.Keys)
        {
            if(!modelPrefebDic.ContainsKey(key))
            {
                modelPrefebDic.Add(key, (GameObject)abTask[key].Data);
            }
        }
        
    }
    /// <summary>
    /// 更新字典
    /// </summary>
    /// <param name="key"></param>
    /// <param name="abTask"></param>
    public static void UpdateModelDic(string key, ABModelDownloadTask abTask)
    {
        if (!modelPrefebDic.ContainsKey(key))
        {
            modelPrefebDic.Add(key, (GameObject)abTask.Data);
        }
    }
    
    
    public static Dictionary<string, GameObject> GetEquipmentDic
    {
        get
        {
            return allEquipmentDataDic;
        }
    }

    public static GameObject FindGameObjectById(string id)
    {

        GameObject result = null;
        allEquipmentDataDic.TryGetValue(id, out result);

        return result;
    }




}
