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

        //移除隐藏的节点
        foreach (GameObject item in gs)
        {
            if (!item.activeSelf)
            {
                GameObject.Destroy(item);
            }
            if (sceneName.Equals(item.name))
            {
                item.AddComponent<TransformObject>();
            }
        }

       
        string[] names = sceneName.ToLower().Split('_');
        string endStr = names[names.Length - 1].ToLower().Trim();

        Regex flooRegex = new Regex("f\\d");
        Regex fjRegex = new Regex("fj\\d");
        Regex wqRegex = new Regex("wq");

        Object3DElement object3DElement = null;
        //楼层
        if (flooRegex.IsMatch(endStr) && gs.Count == 1)
        {
            object3DElement = gs[0].AddComponent<Object3DElement>();
            object3DElement.type = Type.Floor;

        }
        //房间
        else if(fjRegex.IsMatch(endStr)  && gs.Count == 1)
        {
            object3DElement = gs[0].AddComponent<Object3DElement>();
            object3DElement.type = Type.Room;
        }
        //建筑外构
        else if(wqRegex.IsMatch(endStr) && gs.Count == 1)
        {
            object3DElement = gs[0].AddComponent<Object3DElement>();
            object3DElement.type = Type.Builder;
        }
        if(object3DElement!=null)
        {
            object3DElement.sceneId = id;

        }

        foreach (GameObject item in gs)
        {
            
            //处理地形的碰撞器
            if (item.name.ToLower().Contains(Constant.ColliderName.ToLower()))
            {
                SetBoxDisable(item.gameObject);
            }
            else
            {
                Object3dUtility.SetLayerValue(LayerMask.NameToLayer("scene"), item);
            }
            //处理楼层和房间的碰撞器问题
            GameObject colliderGameObject = FindObjUtility.GetTransformChildByName(item.transform, Constant.ColliderName);
            if (colliderGameObject != null)
            {
                SetBoxDisable(colliderGameObject);
            }
    
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
                        SetBoxDisable(roomcolliderGameObject);
                    }
                }
               

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

    private static void SetBoxDisable(GameObject box)
    {
        Object3dUtility.SetLayerValue(LayerMask.NameToLayer("box"), box);

        //BoxCollider bc = box.GetComponent<BoxCollider>();
        //if (bc != null)
        //{
        //    bc.enabled = false;
        //}

    }



}
