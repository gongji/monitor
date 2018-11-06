using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using System.Linq;
using State;

public static class SceneData  {

    //所有的数据列表
    private static List<Object3dItem> object3dList = null;

    //3d对象列表
    private static Dictionary<string, Object3dItem> objectDataDic = new Dictionary<string, Object3dItem>();

    //当前的显示的3d对象
    public static List<Object3dItem> currentobject3dList = null;
    /// <summary>
    /// 初始化所有的3d对象数据
    /// </summary>
    public static void Init3dObjectData(System.Action callBack)
    {
        Object3dProxy.GetAll3dObjectData((a) =>
        {

            object3dList = CollectionsConvert.ToObject<List<Object3dItem>>(a);
            objectDataDic.Clear();
            ParseDataToDic(object3dList);
            Battlehub.UIControls.TreeViewControl.Instance.Init();
            if(callBack!=null)
            {
                callBack.Invoke();
            }
        });
    }

    /// <summary>
    /// 形成id对象的字典，方便查找
    /// </summary>
    /// <param name="object3dList"></param>
    private static void ParseDataToDic(List<Object3dItem> object3dList)
    {

        foreach (Object3dItem object3dItem in object3dList)
        {
            if (object3dItem.id == null)
            {
                objectDataDic.Add(object3dItem.code, object3dItem);
            }
            else
            {
                objectDataDic.Add(object3dItem.id, object3dItem);
            }

            if (object3dItem.childs != null && object3dItem.childs.Count > 0)
            {
                ParseDataToDic(object3dItem.childs);
            }
        }
    }
    /// <summary>
    /// 通过id获取对象的数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Object3dItem FindObjUtilityect3dItemById(string id)
    {

        if(string.IsNullOrEmpty(id))
        {
            return null;
        }
        if (objectDataDic.ContainsKey(id.Trim()))
        {
            return objectDataDic[id];
        }
        return null;
    }
    #region 根据父id和类型查找3d对象列表,用于查询建筑的层

    /// <summary>
    /// 根据父id和类型查找3d对象列表,用于查询建筑的层
    /// 
    /// </summary>
    /// <param name="parentid"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<Object3dItem> GetObject3dItemByCode(string parentid, string type)
    {

        if (string.IsNullOrEmpty(type))
        {

            return object3dList;
        }
        else

        {
            foreach (Object3dItem object3dItem in object3dList)
            {
                if (object3dItem.id.Equals(parentid))
                {
                    return object3dItem.childs;
                }

                if (object3dItem.childs != null && object3dItem.childs.Count > 0)
                {
                    GetChildObject3dItemByCode(object3dItem.childs, parentid);
                }
            }
        }
        return null;
    }



    private static List<Object3dItem> GetChildObject3dItemByCode(List<Object3dItem> list, string parentid)
    {
        foreach (Object3dItem object3dItem in list)
        {
            if (object3dItem.id!=null && object3dItem.id.Equals(parentid))
            {
                return object3dItem.childs;
            }

            if (object3dItem.childs != null && object3dItem.childs.Count > 0)
            {
                GetChildObject3dItemByCode(object3dItem.childs, parentid);
            }
        }

        return null;
    }

    #endregion

  
   
    
    /// <summary>
    /// 得到园区所有的外构的名称列表
    /// </summary>
    /// <returns></returns>
    public static List<Object3dItem> GetAllWq()
    {
        IEnumerable<Object3dItem> result =
            from object3dItem in object3dList
            where object3dItem.code.EndsWith(Constant.WQName)
            select object3dItem;


        return result.ToList<Object3dItem>();
    }

    /// <summary>
    /// 设置当前的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    public static void SetCurrentData<T>(string id)
    {
        System.Type type = typeof(T);


        if (type.Name.Equals(typeof(AreaState).Name))
        {
            currentobject3dList = GetObject3dItemByCode(string.Empty, string.Empty);
        }
        else if (type.Name.Equals(typeof(BuilderState).Name))
        {
            currentobject3dList =GetObject3dItemByCode(id, typeof(BuilderState).Name);
        }
        else
        {
            List<Object3dItem> list = new List<Object3dItem>();
            if (objectDataDic.ContainsKey(id))
            {
                list.Add(objectDataDic[id]);
            }
            currentobject3dList = list;
        }
    }

    public static List<Object3dItem> GetCurrentData()
    {
        return currentobject3dList;
    }


    public static System. Type GetCurrentState(string parentid)
    {
        if(string.IsNullOrEmpty(parentid))
        {

            return typeof(AreaState);
        }

        Type t =  SceneData.FindObjUtilityect3dItemById(parentid).type;
        
        if (t == Type.Floor)
        {
            return typeof(FloorState);
        }
        else
        {
            return typeof(RoomState);
        }
    }
}
