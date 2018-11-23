using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DataModel;
using SystemCore.Task;
using Core.Common.Logging;


public sealed class EquipmentData {

    private static ILog log = LogManagers.GetLogger("EquipmentData");
    /// <summary>
    /// 模型路径，和模型的字典
    /// </summary>
    public static Dictionary<string, GameObject> modelPrefebDic = new Dictionary<string, GameObject>();

    public static Dictionary<string, GameObject> GetmodelPrefebDic
    {
        get
        {
            return modelPrefebDic;
        }
    }
    /// 当前场景对象的设备数据

    private static List<EquipmentItem> currentEquipmentData = null;

    public static List<EquipmentItem> GetCurrentEquipment
    {
        get
        {
            return currentEquipmentData;
        }
    }
    /// <summary>
    /// 根据当前的场景ID获取模型的列表
    /// </summary>
    /// <param name="curentid"></param>
    /// <param name="currentState"></param>
    /// <returns></returns>
    public static void GetCurrentEquipmentModelData(string parentid, System.Action<string[]> callBack)
    {
        //Object3dProxy.GetEquipmentData(parentid, (list) => {
        //    Object3dItem object3dItem = SceneData.FindObjUtilityect3dItemById(parentid);
        //    IState currentState = Main.instance.stateMachineManager.mCurrentState;

        //    if(list==null|| list.Count==0)
        //    {
        //        callBack.Invoke(null);
        //        return;
        //    }
        //    IEnumerable<EquipmentItem> equipmentList = null;
        //    //园区或者房间
        //    if (currentState is AreaState)
        //    {
        //        equipmentList = from item in list
        //                        where  string.IsNullOrEmpty( item.parentid)
        //                        select item;
        //    }
        //    else if (currentState is RoomState)
        //    {
        //        equipmentList = from item in list
        //                        where item.parentid != null && item.parentid.Equals(parentid)
        //                        select item;
        //    }
        //    //建筑
        //    else if (currentState is BuilderState)
        //    {

        //        if (object3dItem.childs != null && object3dItem.childs.Count > 0)
        //        {
        //            equipmentList = GetFoorEquipmentList(list, object3dItem.childs);

        //        }
        //    }
        //    //楼层
        //    else if (currentState is FloorState)
        //    {
        //        List<Object3dItem> floorlist = new List<Object3dItem>();
        //        floorlist.Add(object3dItem);
        //        equipmentList = GetFoorEquipmentList(list, floorlist);
        //    }

        //    ///通过设备数据获取下载模型
        //    if (equipmentList != null)
        //    {
        //        currentEquipmentData = equipmentList.ToList<EquipmentItem>();
        //        string[] modelList = GetModelList(currentEquipmentData);
        //        callBack.Invoke(modelList);
        //    }


        //});
    }

    private static List<EquipmentItem> equipmentItemList;
    /// <summary>
    /// 通过父id获取3d对象列表
    /// </summary>
    /// <param name="parentid"></param>
    /// <param name="callBack"></param>
    public static void GetEquipmentListByParentId(string parentid, System.Action<List<Object3dItem>> callBack)
    {
        if (equipmentItemList == null)
        {
            Object3dProxy.GetEquipmentData(string.Empty, (list) =>
            {
                equipmentItemList = list;
                GetEquipmentByParent(parentid, callBack);
            });
        }
        else
        {
            GetEquipmentByParent(parentid, callBack);
        }


    }
   


    /// <summary>
    /// 通过父节点查找设备
    /// </summary>
    /// <param name="parentid"></param>
    /// <param name="callBack"></param>
    private static void GetEquipmentByParent(string parentid, System.Action<List<Object3dItem>> callBack)
    {
        if (equipmentItemList == null || equipmentItemList.Count == 0)
        {
            if (callBack != null)
            {
                callBack.Invoke(null);
            }
            return;
        }
        IEnumerable<EquipmentItem> equipmentList = null;
        if(!string.IsNullOrEmpty(parentid))
        {
            equipmentList = from item in equipmentItemList
                            where  !string.IsNullOrEmpty(item.parentsId) &&  item.parentsId.Equals(parentid)
                            select item;
        }
        else
        {
            equipmentList = from item in equipmentItemList
                            where string.IsNullOrEmpty(item.parentsId)
                            select item;
        }
       

        List<Object3dItem> objList = new List<Object3dItem>();
        foreach (var item in equipmentList)
        {
            Object3dItem object3dItem = new Object3dItem();
            object3dItem.name = item.name;
            object3dItem.id = item.id;
            object3dItem.type = Type.Equipment;
            object3dItem.parentsId = item.parentsId;
            objList.Add(object3dItem);
        }

        if (callBack != null)
        {
            callBack.Invoke(objList);
        }
    }

    /// <summary>
    /// 获取楼层和大楼在的设备列表
    /// </summary>
    /// <param name="eList"></param>
    /// <param name="floorList"></param>
    /// <returns></returns>
    private static IEnumerable<EquipmentItem> GetFoorEquipmentList(List<EquipmentItem> eList,List<Object3dItem> floorList)
    {

        List<string> ids = new List<string>();
        foreach(Object3dItem floor in floorList)
        {
            ids.Add(floor.id);
            if(floor.childs!=null && floor.childs.Count>0)
            {
                foreach(Object3dItem room in floor.childs)
                {
                    ids.Add(room.id);
                }
            }
        }
        if(ids.Count>0)
        {
            IEnumerable<EquipmentItem> equipmentList =
               from item in eList
               where ids.Contains(item.parentsId)
               select item;

            return equipmentList;
        }
       
        return null;
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
            if(!modelPrefebDic.ContainsKey (key))
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
    /// <summary>
    /// 设备id，设备对象
    /// </summary>
    private static Dictionary<string, GameObject> equipmentDic = new Dictionary<string, GameObject>();
    
    public static Dictionary<string, GameObject> GetEquipmentDic
    {
        get
        {
            return equipmentDic;
        }
    }

    public static GameObject FindGameObjectById(string id)
    {

        GameObject result = null;
        equipmentDic.TryGetValue(id, out result);

        return result;
    }




}
