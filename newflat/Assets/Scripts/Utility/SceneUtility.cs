using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public static class SceneUtility
{
    /// <summary>
    /// 获取场景下某物体
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public static GameObject GetGameObject(string sceneName, string url)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        //if (scene == null) { Debug<Seaweed>.LogError("scene is null"); return null; }
        GameObject[] roots;
        if (scene != null && scene.IsValid())
        {

            roots = scene.GetRootGameObjects();
        }
        else { return null; }

        GameObject objTarget = null;

        string objName = "";
        string objChildName = "";

        if (!url.Contains("/"))
        {
            objName = url;
        }
        else
        {
            string[] urls = url.Split(new char[] { '/' }, 2);
            objName = urls[0];
            objChildName = urls[1];
        }

        foreach (GameObject temp in roots)
        {
            if (temp.name.Equals(objName))
            {
                objTarget = temp;
                break;
            }
        }
        if ((objTarget != null) && (!objChildName.Equals("")))
        {
            //Debug<Seaweed>.Log(objName);
           // Debug<Seaweed>.Log(objChildName);
            objTarget = objTarget.transform.Find(objChildName).gameObject;
        }

        return objTarget;
    }

    /// <summary>
    /// 获取场景下的所有游戏对象
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public static List<GameObject> GetRootGameObjects(string sceneName)
    {
        Scene currentScene = SceneManager.GetSceneByName(sceneName);
        if (currentScene != null && currentScene.IsValid())
        {
            GameObject[] roots = currentScene.GetRootGameObjects();
            return new List<GameObject>(roots);
        }
        else
        {
            return null;
        }

    }


    /// <summary>
    /// 控制场景的显示隐藏
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="isVisible"></param>
    public static void SetRootGameObjects(string sceneName,bool isVisible)
    {
        Scene currentScene = SceneManager.GetSceneByName(sceneName);
        if (currentScene != null && currentScene.IsValid())
        {
            GameObject[] roots = currentScene.GetRootGameObjects();
            foreach(GameObject g in roots)
            {
                g.SetActive(isVisible);
            }
            
        }
       

    }




    /// <summary>
    /// 获取场景根目录下的节点,根据名称完全匹配
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="rootName"></param>
    /// <returns></returns>
    public static GameObject GetGameByRootName(string sceneName,string rootName,bool isLike =false)
    {
        Scene currentScene = SceneManager.GetSceneByName(sceneName);
        if (currentScene != null && currentScene.IsValid())
        {
            GameObject[] roots = currentScene.GetRootGameObjects();
            IEnumerable gameObjectQuery = null;
            if (!isLike)
            {
                gameObjectQuery =
                from g in roots
                where g.name.ToLower().Equals(rootName.ToLower())
                select g;
            }
            else
            {
               gameObjectQuery =
               from g in roots
               where g.name.ToLower().Contains(rootName.ToLower())
               select g;
            }
            


            foreach (GameObject _g in gameObjectQuery)
            {
                return _g;
            }
            return null;
        }
        else
        {
            return null;
        }

    }

    /// <summary>
    /// 获取场景下碰撞器的节点的名字
    /// </summary>
    /// <param name="sceneCode"></param>
    /// <returns></returns>
    public static GameObject GetSceneCollider(string sceneCode)
    {
        GameObject rootGameObjerct = SceneUtility.GetGameByRootName(sceneCode, sceneCode);
        if (rootGameObjerct != null)
        {

            GameObject collider = FindObjUtility.GetTransformChildByName(rootGameObjerct.transform, Constant.ColliderName);
            if (collider != null)
            {
                return collider;
            }
        }
        return null;
    }



    /// <summary>
    /// 获取场景下相机+的节点的名字
    /// </summary>
    /// <param name="sceneCode"></param>
    /// <returns></returns>
    public static GameObject GetSceneCameraObject(string sceneCode,string cameraName = "Camera")
    {
        GameObject rootGameObjerct = SceneUtility.GetGameByRootName(sceneCode, sceneCode);
        if (rootGameObjerct != null)
        {

            GameObject cameraGameObject = FindObjUtility.GetTransformChildByName(rootGameObjerct.transform, cameraName);
            if (cameraGameObject != null)
            {
                return cameraGameObject;
            }
        }
        return null;
    }


    // 获取场景根目录下的节点,根据组件名称
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="rootName"></param>
    /// <returns></returns>
    public static GameObject GetGameByComponent<T>(string sceneName)
    {
        Scene currentScene = SceneManager.GetSceneByName(sceneName);
        if (currentScene != null && currentScene.IsValid())
        {
            GameObject[] roots = currentScene.GetRootGameObjects();

            IEnumerable gameObjectQuery =
            from g in roots
            where g.GetComponent<T>() != null
            select g;


            foreach (GameObject _g in gameObjectQuery)
            {
                return _g;
            }
            return null;
        }
        else
        {
            return null;
        }

    }
}
