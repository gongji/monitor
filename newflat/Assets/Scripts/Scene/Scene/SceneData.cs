using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using System.Linq;
using State;
using System.Text.RegularExpressions;

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
    public static void Init3dAllObjectData(System.Action callBack)
    {
        Object3dProxy.GetAll3dObjectData((result) =>
        {

            //Debug.Log(result);
            object3dList = CollectionsConvert.ToObject<List<Object3dItem>>(result);

           // Debug.Log(object3dList.Count);
            objectDataDic.Clear();
            ParseDataToDic(object3dList);
           
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
                objectDataDic.Add(object3dItem.number, object3dItem);
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


    
    /// <summary>
    /// 得到园区所有的外构的名称列表
    /// </summary>
    /// <returns></returns>
    public static List<Object3dItem> GetAllWq()
    {
        IEnumerable<Object3dItem> result =
            from object3dItem in object3dList
            where object3dItem.number.EndsWith(Constant.WQName)
            select object3dItem;


        return result.ToList<Object3dItem>();
    }

    /// <summary>
    /// 整理Tree的父子结构
    /// </summary>
    /// <param name="wqList"></param>
    public static void SetTreeParent(List<Object3dItem> wqList)
    {

        Regex flooRegex = new Regex("f\\d");
        Regex roomRegex = new Regex("fj\\d");
        //查找外构下的楼层
        foreach (Object3dItem wq in wqList)
        {
            string wqNumber = wq.number.Split('_')[0];
            IEnumerable<Object3dItem> floorResult =
            from object3dItem in object3dList
            where flooRegex.IsMatch(object3dItem.number) && object3dItem.number.StartsWith(wqNumber) && object3dItem.number.Split('_').Length==3
            select object3dItem;

            //外构下的楼层
            List<Object3dItem> floorList = floorResult.ToList<Object3dItem>();
            wq.childs = floorList;

            foreach(Object3dItem floor in floorList)
            {
                IEnumerable<Object3dItem> roomResult =
               from object3dItem in object3dList
               where roomRegex.IsMatch(object3dItem.number) && object3dItem.number.Contains(floor.number) && object3dItem.number.Split('_').Length == 4
               select object3dItem;

                //外构下的楼层
                List<Object3dItem> roomList = roomResult.ToList<Object3dItem>();
                floor.childs = roomList;
            }


        }
    }



    #region SetCurentData

    //public static Object3dItem currentScene;
    /// <summary>
    /// 设置当前加载的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    public static void SetCurrentData<T>(string id, int FloorGroup,string buiderId)
    {

        SceneContext.currentSceneData  = FindObjUtilityect3dItemById(id);
        System.Type type = typeof(T);

        //园区
        if (type.Name.Equals(typeof(AreaState).Name))
        {
            currentobject3dList = GetObject3dItemByParent("0");
        }
        //房间
        else if ( type.Name.Equals(typeof(RoomState).Name))
        {
            currentobject3dList = GetRoomObject3dItem(id);
        }
        //楼层
        else if(type.Name.Equals(typeof(FloorState).Name))
        {
            currentobject3dList = GetFloorObject3dItem(id);
        }
        //建筑
        else if(type.Name.Equals(typeof(BuilderState).Name))
        {
            currentobject3dList = GetBuilderObject3dItem(id, FloorGroup);
        }
        //全景
        else if(type.Name.Equals(typeof(FullAreaState).Name))
        {
            GetFullAreaObject3dItem(id, buiderId);
        }

        //打印输出的场景信息
        foreach (Object3dItem temp in currentobject3dList)
        {
            Debug.Log(temp.number);
        }
    }

    /// <summary>
    /// 根据父id和类型查找3d对象列表,用于查询建筑的层
    /// 
    /// </summary>
    /// <param name="parentid"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<Object3dItem> GetObject3dItemByParent(string parentid)
    {

        IEnumerable<Object3dItem> result =
              from object3dItem in object3dList
              where object3dItem.parentsId.Equals(parentid)
              select object3dItem;


        return result.ToList<Object3dItem>();
    }

    /// <summary>
    /// 获取楼层下的3d对象
    /// </summary>
    /// <param name="parentid"></param>
    /// <returns></returns>
    public static List<Object3dItem> GetFloorObject3dItem(string currentid)
    {

        IEnumerable<Object3dItem> roomResult =
              from object3dItem in object3dList
                  //不包含管网
              where object3dItem.parentsId.Equals(currentid) && !object3dItem.number.Contains(Constant.GuanDao)
              select object3dItem;


        List<Object3dItem> result = new List<Object3dItem>();
        //查找门禁
        foreach (Object3dItem room in roomResult)
        {
            string[] strs = room.number.Split('_');
            string prefix = strs[0] + "_" + strs[1] + "_" + strs[2];
            string endStr = strs[strs.Length - 1];
            IEnumerable<Object3dItem> doorList =
              from object3dItem in object3dList
              where object3dItem.number.EndsWith(endStr) && object3dItem.number.Contains(Constant.Door) && object3dItem.number.StartsWith(prefix)
              select object3dItem;
            if (doorList.Count() == 1)
            {
                result.Add(doorList.ToList<Object3dItem>()[0]);
            }

        }

        //查找当前的层
        IEnumerable<Object3dItem> floorResult =
                from object3dItem in object3dList
                    //不包含管网
                where object3dItem.id.Equals(currentid) && !object3dItem.number.Contains(Constant.GuanDao)
                select object3dItem;



        if (floorResult.Count() == 1)
        {
            result.Add(floorResult.ToList<Object3dItem>()[0]);
        }

        return result;
    }


    /// <summary>
    /// 建筑
    /// </summary>
    /// <param name="currentid"></param>
    /// <param name="FloorGroup">当前楼层的组编号</param>
    /// <returns></returns>
    public static List<Object3dItem> GetFullAreaObject3dItem(string currentid, string buiderId)
    {
        IEnumerable<Object3dItem> floorResult =
             from object3dItem in object3dList
                 //不包含管网
              where object3dItem.parentsId.Equals(buiderId) && !object3dItem.number.Contains(Constant.GuanDao)
             select object3dItem;


        return floorResult.ToList<Object3dItem>();
    }

        /// <summary>
        /// 建筑
        /// </summary>
        /// <param name="currentid"></param>
        /// <param name="FloorGroup">当前楼层的组编号</param>
        /// <returns></returns>
        public static List<Object3dItem> GetBuilderObject3dItem(string currentid,int FloorGroup)
    {

        //楼层
        IEnumerable<Object3dItem> floorResult =
              from object3dItem in object3dList
                  //不包含管网
              where object3dItem.parentsId.Equals(currentid) && !object3dItem.number.Contains(Constant.GuanDao)
              select object3dItem;

        //按照范围过滤

        List<Object3dItem> floorFiter = new List<Object3dItem>();
        for(int i=0;i< floorResult.Count();i++)
        {
            if(i>= FloorGroup * 3 && i<(FloorGroup+1) * 3)
            {
                floorFiter.Add(floorResult.ToList<Object3dItem>()[i]);
            }
        }

        //楼层下的门禁和管网
        List<Object3dItem> result = new List<Object3dItem>();
        //查找门禁
        foreach (Object3dItem floor in floorResult)
        {
           
            IEnumerable<Object3dItem> roomList =
              from object3dItem in object3dList
              where object3dItem.number.Split('_').Length ==5 && object3dItem.number.StartsWith(floor.number) && !object3dItem.number.Contains(Constant.GuanDao)
              select object3dItem;

            result.AddRange(roomList);

        }
        result.AddRange(floorFiter);

        return result;
    }
    public static List<Object3dItem> GetRoomObject3dItem(string currentid)
    {

        Object3dItem room = FindObjUtilityect3dItemById(currentid);
        string[] sts = room.number.Split('_');
        string endstr = sts[sts.Length - 1];

        Regex roomRegex = new Regex("fj\\d");
        string prefix = sts[0] + "_" + sts[1] + "_" + sts[2];

        IEnumerable<Object3dItem> roomResult =
              from object3dItem in object3dList
                  //不包含管网
              where roomRegex.IsMatch(object3dItem.number) && object3dItem.number.EndsWith(endstr) && object3dItem.number.StartsWith(prefix)
              select object3dItem;

        return roomResult.ToList<Object3dItem>();

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


    #endregion 

    #region  sceneUpdate
    /// <summary>
    /// 更新场景数据
    /// </summary>
    public static void UpdateSceneData(System.Action callBack)
    {
        Object3dProxy.GetAll3dObjectData((result) =>
        {
            List<Object3dItem> object3dList = CollectionsConvert.ToObject<List<Object3dItem>>(result);
            SetRoomParent(object3dList);
            SetFloorParent(object3dList);
            SetType(object3dList);

            //  CollectionsConvert.ToJSON(object3dList);
            Dictionary<string, string> saveDic = new Dictionary<string, string>();
            saveDic.Add("result", CollectionsConvert.ToJSON(object3dList));
            Object3dProxy.PostUpdateScene((postResult) =>
            {

                callBack.Invoke();

            }, saveDic);

        });
    }

    private static void SetType(List<Object3dItem> object3dList)
    {
        foreach (Object3dItem item in object3dList)
        {
            if (item.number.EndsWith(Constant.WQName.ToLower()))
            {
                item.type = Type.Builder;
            }
        }
    }

    private static void SetRoomParent(List<Object3dItem> object3dList)
    {
        Regex fjRegex = new Regex("fj\\d");
        IEnumerable<Object3dItem> roomList =
            from object3dItem in object3dList
            where fjRegex.IsMatch(object3dItem.number)
            select object3dItem;

        foreach (Object3dItem item in roomList)
        {
            string[] str = item.number.Split('_');

            //处理房间，juliusuo_sn_f1_fj1
            string floorPrefix = string.Empty;
            if (item.number.Split('_').Length==4)
            {
                floorPrefix = str[0] + "_" + str[1] + "_" + str[2];
            }
            //juliusuo_sn_f1_guanndao_fj2,处理房间的管网和门禁
            else
            {
                floorPrefix = str[0] + "_" + str[1] + "_" + str[2]+"_"+ str[4];
            }

            IEnumerable<Object3dItem> floorList =
              from object3dItem in object3dList
              where object3dItem.number.ToString().Equals(floorPrefix)
              select object3dItem;

            if (floorList.Count() == 1)
            {
                item.parentsId = floorList.ToArray<Object3dItem>()[0].id;
                item.type = Type.Room;

            }

        }
    }

    private static void SetFloorParent(List<Object3dItem> object3dList)
    {
        Regex flooRegex = new Regex("f\\d");
        IEnumerable<Object3dItem> floorList =
            from object3dItem in object3dList
            where flooRegex.IsMatch(object3dItem.number) && !object3dItem.number.Contains(Constant.FJ.ToLower())
            select object3dItem;

        //Debug.Log(floorList.Count());
        foreach (Object3dItem item in floorList)
        {
            string[] str = item.number.Split('_');
            string floorPrefix = str[0] + "_wq";

            IEnumerable<Object3dItem> wqList =
            from object3dItem in object3dList
            where object3dItem.number.ToString().Equals(floorPrefix)
            select object3dItem;

            if (wqList.Count() == 1)
            {
                item.parentsId = wqList.ToArray<Object3dItem>()[0].id;
                item.type = Type.Floor;
            }
        }
    }


    /// <summary>
    /// 是否有新的场景，需要更新
    /// </summary>
    /// <param name="callBack"></param>
    public static void IsExistNewScene(System.Action<bool> callBack)
    {
        Object3dProxy.IsExistNewScene((result) =>
        {
            //有新的场景
            if (result.Equals("1"))
            {
                callBack.Invoke(true);
            }
            else
            {
                callBack.Invoke(false);
            }
        });
    }

    #endregion


    /// <summary>
    /// 获取当前层下边是否有管网
    /// </summary>
    /// <returns></returns>
    public static List<Object3dItem> GetCurrentGuangWang()
    {
       
        if (SceneContext.currentSceneData != null)
        {
            string[] strs = SceneContext.currentSceneData.number.Split('_');
            string endNumber = strs[strs.Length - 1];
            string parentid = SceneContext.currentSceneData.parentsId;

            IEnumerable<Object3dItem> result =
             from object3dItem in object3dList
             where object3dItem.parentsId.Equals(parentid) && object3dItem.number.Contains(Constant.GuanDao) && object3dItem.number.EndsWith(endNumber)
             select object3dItem;

            //foreach(Object3dItem temp in result)
            //{
            //    Debug.Log(temp.number);
            //}

            return result.ToList<Object3dItem>();

        }
        return null;
    }
}
