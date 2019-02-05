using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using System.Linq;
using State;
using System.Text.RegularExpressions;

public static class SceneData {

    //所有的数据列表
    private static List<Object3dItem> object3dList = null;

    //3d对象列表
    private static Dictionary<string, Object3dItem> objectDataDic = new Dictionary<string, Object3dItem>();

    //scenid和游戏对象数据的字典
    public static Dictionary<string, Object3DElement> gameObjectDic = new Dictionary<string, Object3DElement>();

    //场景id和scene的关系
    public static Dictionary<string, SceneAlarmBase> sceneAlarmDic = new Dictionary<string, SceneAlarmBase>();
    //当前的显示的3d对象
    public static List<Object3dItem> currentobject3dList = null;
    /// <summary>
    /// 初始化所有的3d对象数据
    /// </summary>
    public static void Init3dAllObjectData(System.Action callBack)
    {
        Scene3dProxy.GetAll3dObjectData((result) =>
        {

            // Debug.Log(result);
            object3dList = CollectionsConvert.ToObject<List<Object3dItem>>(result);
          
            // Debug.Log(object3dList.Count);
            objectDataDic.Clear();
            ParseDataToDic(object3dList);

            if (callBack != null)
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
            //sort use
            if(object3dItem.type == DataModel.Type.Floor)
            {
                string[]  strs =object3dItem.number.Split('_') ;
                string floorindex=  strs[strs.Length-1].Substring(1);
                object3dItem.sortIndex = int.Parse(floorindex);
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

        if (string.IsNullOrEmpty(id))
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
            if (object3dItem.id != null && object3dItem.id.Equals(parentid))
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
    /// all wq
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
    /// color and  map wq 
    /// </summary>
    public static  List<Object3dItem> GetAllBuilder(string number)
    {
        IEnumerable<Object3dItem> result =
            from object3dItem in object3dList
            where object3dItem.number.EndsWith(number) && !object3dItem.number.Contains(Constant.DX)
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
            where flooRegex.IsMatch(object3dItem.number) && object3dItem.number.StartsWith(wqNumber) && object3dItem.number.Split('_').Length == 3
            select object3dItem;

            //外构下的楼层
            List<Object3dItem> floorList = floorResult.ToList<Object3dItem>();
            wq.childs = floorList;

            foreach (Object3dItem floor in floorList)
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
    public static void SetCurrentData<T>(string id, int FloorGroup, string buiderId)
    {

        SceneContext.currentSceneData = FindObjUtilityect3dItemById(id);
        SceneContext.FloorGroup = FloorGroup;
        SceneContext.areaBuiderId = buiderId;
        System.Type type = typeof(T);

        //area
        if (type.Name.Equals(typeof(AreaState).Name))
        {
            currentobject3dList = GetObject3dItemByParent("0");

            //将main_dx set first
            Object3dItem maindx = null;
            for(int i=0;i< currentobject3dList.Count;i++)
            {
                if(currentobject3dList[i].number.Equals(Constant.Main_dxName))
                {
                    maindx = currentobject3dList[i];

                    currentobject3dList.RemoveAt(i);
                    break;
                }
            }

            currentobject3dList.Insert(0, maindx);

        }
        //room 
        else if (type.Name.Equals(typeof(RoomState).Name))
        {
            currentobject3dList = GetRoomObject3dItem(id);
        }
        //floor
        else if (type.Name.Equals(typeof(FloorState).Name))
        {
            currentobject3dList = GetFloorObject3dItem(id);
        }
        //builder
        else if (type.Name.Equals(typeof(BuilderState).Name))
        {
            currentobject3dList = GetBuilderObject3dItem(id, FloorGroup);
        }
        //color
        else if (type.Name.Equals(typeof(ColorAreaState).Name))
        {
            currentobject3dList  = GetColorAreaObject3dItem(id, buiderId, Constant.MapName.ToString());
        }
        //full
        else if(type.Name.Equals(typeof(FullAreaState).Name))
        {
            currentobject3dList = GetColorAreaObject3dItem(id, buiderId, Constant.FullName.ToString());
        }

        //打印输出的场景信息
        foreach (Object3dItem temp in currentobject3dList)
        {
            //Debug.Log(temp.number);
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
              where object3dItem.parentsId.Equals(parentid) && !object3dItem.number.EndsWith(Constant.MapName.ToLower())
               && !object3dItem.number.EndsWith(Constant.FullName.ToLower())
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
              where object3dItem.parentsId.Equals(currentid) && !object3dItem.number.Contains(Constant.JiDian)
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
                where object3dItem.id.Equals(currentid) && !object3dItem.number.Contains(Constant.JiDian)
                select object3dItem;



        if (floorResult.Count() == 1)
        {
            result.Add(floorResult.ToList<Object3dItem>()[0]);
        }

        return result;
    }


    /// <summary>
    /// color  and  full
    /// </summary>
    /// <param name="currentid">当前为-1</param>
    /// <param name="buiderId">建筑id</param>
    /// <returns></returns>
    public static List<Object3dItem> GetColorAreaObject3dItem(string currentid, string buiderId,string identification)
    {

        Object3dItem builderItem = FindObjUtilityect3dItemById(buiderId);
        string firstName = builderItem.number.Split('_')[0];
        IEnumerable<Object3dItem> fullresult =
             from object3dItem in object3dList
             where object3dItem.number.Equals(Constant.SkyboxName) || (object3dItem.number.StartsWith(firstName)  && object3dItem.number.EndsWith(identification))
             select object3dItem;


       // Debug.Log(fullresult.ToList<Object3dItem>().Count);

        return fullresult.ToList<Object3dItem>();
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
              where object3dItem.parentsId.Equals(currentid) && !object3dItem.number.Contains(Constant.JiDian) 
              orderby object3dItem.sortIndex ascending
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
        foreach (Object3dItem floor in floorFiter)
        {
           
            IEnumerable<Object3dItem> roomList =
              from object3dItem in object3dList
              where object3dItem.number.Split('_').Length ==5 && object3dItem.number.StartsWith(floor.number) && !object3dItem.number.Contains(Constant.JiDian)
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


    public static System.Type GetCurrentState(string parentid)
    {
        if(string.IsNullOrEmpty(parentid))
        {

            return typeof(AreaState);
        }

        Type t = SceneData.gameObjectDic[parentid].GetComponent<Object3DElement>().type;
        if(t== Type.Area)
        {
            return typeof(AreaState);
        }
        else if (t == Type.Floor)
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
        Scene3dProxy.GetAll3dObjectData((result) =>
        {
            List<Object3dItem> object3dList = CollectionsConvert.ToObject<List<Object3dItem>>(result);
            SetRoomParent(object3dList);
            SetFloorParent(object3dList);
            SetType(object3dList);

            //  CollectionsConvert.ToJSON(object3dList);
            Dictionary<string, string> saveDic = new Dictionary<string, string>();
            saveDic.Add("result", CollectionsConvert.ToJSON(object3dList));
            Scene3dProxy.PostUpdateScene((postResult) =>
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
            else if(item.number.EndsWith(Constant.DX.ToLower()))
            {
                item.type = Type.Area;
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
        Scene3dProxy.IsExistNewScene((result) =>
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
             where object3dItem.parentsId.Equals(parentid) && object3dItem.number.Contains(Constant.JiDian) && object3dItem.number.EndsWith(endNumber)
             select object3dItem;

            //foreach(Object3dItem temp in result)
            //{
            //    Debug.Log(temp.number);
            //}

            return result.ToList<Object3dItem>();

        }
        return null;
    }


    /// <summary>
    /// 得到建筑的索引组的数量
    /// </summary>
    /// <param name="builderid"></param>
    /// <returns></returns>

    public static int GetBuilderIndexCount(string builderid)
    {
        IEnumerable<Object3dItem> floorResult =
             from object3dItem in object3dList
                 //不包含管网
             where ((object3dItem.parentsId.Equals(builderid) && !object3dItem.number.Contains(Constant.JiDian)))
             select object3dItem;


        return Mathf.CeilToInt(floorResult.Count()/3.0f);
    }


    /// <summary>
    /// 得到number的数据库id
    /// </summary>
    /// <returns></returns>
    public static string GetIdByNumber(string number)
    {
        IEnumerable<Object3dItem> result =
           from object3dItem in object3dList
           where object3dItem.number.Equals(number)
           select object3dItem;

        if(result.Count()==1)
        {
            return result.ToList<Object3dItem>()[0].id;

        }

        return "";
    }

    public static string GetNameByNumber(string number)
    {
        IEnumerable<Object3dItem> result =
           from object3dItem in object3dList
           where object3dItem.number.Equals(number)
           select object3dItem;

        if (result.Count() == 1)
        {
            return result.ToList<Object3dItem>()[0].name;

        }

        return "";
    }


    public static List<Object3dItem> GetDownLoadFinishScene()
    {
        IEnumerable<Object3dItem> result =
         from object3dItem in object3dList
         where object3dItem.isDownFinish == true
         select object3dItem;

        return result.ToList<Object3dItem>();
    }



    public static List<Object3dItem> GetRemoveScene()
    {
        IEnumerable<Object3dItem> result =
         from object3dItem in object3dList
         where (object3dItem.isDownFinish == true && (object3dItem.number.Contains(Constant.MapName) || 
         object3dItem.number.Contains(Constant.FullName) || object3dItem.number.Contains(Constant.JiDian)))
         select object3dItem;

        return result.ToList<Object3dItem>();
    }

}
