using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using DataModel;
using System.Linq;
using State;
using System.Text.RegularExpressions;

public static class SceneParse  {
    /// <summary>
    /// 通过ID查找外构下的相机位置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static GameObject FindWQMoveCamera(string id)
    {
        
        Object3dItem object3dItem = SceneData.FindObjUtilityect3dItemById(id.Trim());
        if (object3dItem != null)
        {
            GameObject rootGameObjerct = SceneUtility.GetGameByRootName(object3dItem.number, object3dItem.number);
            if (rootGameObjerct != null)
            {

                GameObject cameraGameObject = FindObjUtility.GetTransformChildByName(rootGameObjerct.transform, "camera");

                return cameraGameObject;

            }
        }

        return null;
    }

    public static void DoSceneGameObject(string sceneName,string id)
    {
        List<GameObject> gs = SceneUtility.GetRootGameObjects(sceneName);

        GameObject sceneRoot = null;
        //移除隐藏的节点
        foreach (GameObject item in gs)
        {
            if (!item.activeSelf)
            {
                GameObject.Destroy(item);
            }
            if (sceneName.Equals(item.name))
            {
                sceneRoot = item;
                item.AddComponent<TransformObject>();
            }
        }

        HideManYouPersonObject(sceneRoot);
        string endStr = GetEndSpitStr(sceneName);

        Regex flooRegex = new Regex("f\\d");
        Regex fjRegex = new Regex("fj\\d");
        Regex wqRegex = new Regex("wq");

        Object3DElement object3DElement = null;
        //楼层
        if (flooRegex.IsMatch(endStr))
        {
            object3DElement = sceneRoot.AddComponent<Object3DElement>();
            if (sceneRoot.name.ToLower().Contains(Constant.JiDian.ToLower()))
            {
                object3DElement.type = Type.JiDian;
                AddChildBimScritps(sceneRoot.transform);
            }
            else
            {
                object3DElement.type = Type.Floor;
                sceneRoot.AddComponent<FloorSceneAlarm>();
                AddBimScript(sceneRoot.transform);
            }
        }
        //房间
        else if(fjRegex.IsMatch(endStr)  && !sceneName.Contains(Constant.Door))
        {
            object3DElement = sceneRoot.AddComponent<Object3DElement>();
            
            object3DElement.type = Type.Room;
            sceneRoot.AddComponent<RoomSceneAlarm>();
        }
        //建筑外构
        else if(wqRegex.IsMatch(endStr))
        {
            object3DElement = sceneRoot.AddComponent<Object3DElement>();
            object3DElement.type = Type.Builder;
            AddWqAlarmObjectScripts(flooRegex, sceneRoot.transform);
        }
        //门的场景
        else if(fjRegex.IsMatch(endStr) &&  sceneName.Contains(Constant.Door))
        {
            object3DElement = sceneRoot.AddComponent<Object3DElement>();
            object3DElement.type = Type.RoomDoor;
            sceneRoot.AddComponent<DoorSceneData>();
        //地形
        }else if(sceneName.Equals(Constant.Main_dxName.ToLower()))
        {
            object3DElement = sceneRoot.AddComponent<Object3DElement>();
            object3DElement.type = Type.Area;
        }
        if(object3DElement!=null)
        {
            object3DElement.sceneId = id;

        }
        if(object3DElement!=null)
        {
            SceneData.gameObjectDic.Add(object3DElement.sceneId, object3DElement);
        }
        

        foreach (GameObject item in gs)
        {
            if(item.GetComponent<Light>()!=null)
            {
                continue;
            }
            //处理地形的碰撞器
            if (item.name.ToLower().Contains(Constant.ColliderName.ToLower()))
            {
                SetBoxColliderAndSetLayer(item.gameObject);
            }
            else
            {
                Object3dUtility.SetLayerValue(LayerMask.NameToLayer("scene"), item);
            }
            //处理楼层和房间的碰撞器问题
            GameObject colliderGameObject = FindObjUtility.GetTransformChildByName(item.transform, Constant.ColliderName);
            if (colliderGameObject != null)
            {
                SetBoxColliderAndSetLayer(colliderGameObject);
            }
            //处理楼层下的房间
            List<Transform> roomts = FindObjUtility.FindRoom(item.transform);
            
            if(roomts != null && roomts.Count>0)
           
            {
                foreach(Transform roomt in roomts)
                {
                    object3DElement = roomt.gameObject.AddComponent<Object3DElement>();
                    object3DElement.type = Type.Room;
                    string sceneid = SceneData.GetIdByNumber(roomt.transform.name);
                    object3DElement.sceneId = sceneid;

                    GameObject roomcolliderGameObject = FindObjUtility.GetTransformChildByName(roomt.transform, Constant.ColliderName);
                    if (roomcolliderGameObject != null)
                    {
                        SetBoxColliderAndSetLayer(roomcolliderGameObject);
                    }
                    roomt.gameObject.AddComponent<FloorRoomSceneAlarm>();
                }
               

            }

            //处理门禁

            bool isDoor = IsDoor(item.name);

            if(isDoor)
            {
                AddDoorScripts(item.transform);
            }

            SetLight(item.gameObject);
        }
    }

    private static void SetLight(GameObject item)
    {
        //设置光线
        Light[] lights = item.GetComponentsInChildren<Light>(true);
        foreach (Light light in lights)
        {
            light.cullingMask = 1 << 9;

        }
    }

    private static void SetBoxColliderAndSetLayer(GameObject box)
    {
        box.layer = LayerMask.NameToLayer("box");
      //  Object3dUtility.SetLayerValue(LayerMask.NameToLayer("box"), box);

        AddBoxCollider(box);
       

    }

    /// <summary>
    /// 判断是否为门的场景
    /// </summary>
    /// <param name="ItemName"></param>
    private static bool IsDoor(string itemName)
    {
        string endStr = GetEndSpitStr(itemName);
        Regex fjRegex = new Regex("fj\\d");
        if (fjRegex.IsMatch(endStr)  && itemName.Contains(Constant.Door))
        {
           return true;
        }
        return false;
    }

    /// <summary>
    /// 添加门禁脚本
    /// </summary>
    /// <param name="doorRoot"></param>
    private static void AddDoorScripts(Transform  doorRoot)
    {
        foreach (Transform child in doorRoot)
        {
            if(child.name.Contains(Constant.Door))
            {
                Object3dUtility.SetLayerValue(LayerMask.NameToLayer("equipment"), child.gameObject);
                Object3DElement object3DElement = child.gameObject.AddComponent<Object3DElement>();
                object3DElement.type = Type.De_Door;
                object3DElement.equipmentData.number = object3DElement.transform.name;
                object3DElement.equipmentData.sceneId = doorRoot.GetComponent<Object3DElement>().sceneId;
                object3DElement.equipmentData.type = Type.De_Door.ToString();
            }
        }
    }

    private static void AddWqAlarmObjectScripts(Regex flooRegex, Transform parent)
    {
        parent.gameObject.AddComponent<WqSceneAlarm>();
      
        //addd box scripts
        foreach(Transform child in parent)
        {
            string endStr = GetEndSpitStr(child.name);
            if(string.IsNullOrEmpty(endStr))
            {
                continue;
            }
            if(flooRegex.IsMatch(endStr))
            {
                GameObject box =  FindObjUtility.GetTransformChildByName(child, Constant.ColliderName);
                
                if(box!=null)
                {
                    AddBoxCollider(box);
                    box.AddComponent<FloorBoxCollider>();
                }
          
            }
        }
    }

    private static void AddBoxCollider(GameObject box)
    {
        if (box.GetComponent<MeshCollider>() != null)
        {
            GameObject.DestroyImmediate (box.GetComponent<MeshCollider>());
        }

        BoxCollider bc = box.GetComponent<BoxCollider>();
        if (bc == null)
        {
            bc = box.gameObject.AddComponent<BoxCollider>();
        }
       
        bc.isTrigger = true;
    }

    private  static string GetEndSpitStr(string str)
    {
        string[] names = str.ToLower().Split('_');
        if(names.Length == 0 )
        {
            return "";
        }
        string endStr = names[names.Length - 1].ToLower().Trim();
        return endStr;
    }

    private static void AddBimScript(Transform t)
    {
   
        foreach(Transform tempT in t)
        {
            //door
            if(tempT.name.ToLower().Equals(Constant.Door))
            {
                AddChildBimScritps(tempT);
            }
            else if(tempT.name.ToLower().Equals(Constant.Qiang.ToLower()))
            {
                foreach(Transform  qiangItem in tempT)
                {
                    AddChildBimScritps(qiangItem);
                }
            }
        }
        
    }

    private static  void AddChildBimScritps(Transform root)
    {
        if(AppInfo.Platform == BRPlatform.Editor)
        {
            return;
        }
        foreach (Transform child in root)
        {
            if(child.GetComponent<MeshRenderer>()!=null)
            {
                child.gameObject.AddComponent<BimMouse>();
            }
        }
    }

    private static void HideManYouPersonObject(GameObject t)
    {
        if(t == null)
        {
            return;
        }
        GameObject item = FindObjUtility.GetTransformChildByName(t.transform, Constant.Person_Point);
        if(item!=null)
        {
            foreach (Transform child in item.transform)
            {
                GameObject.DestroyImmediate(child.gameObject);
            }
        }
    }

}
