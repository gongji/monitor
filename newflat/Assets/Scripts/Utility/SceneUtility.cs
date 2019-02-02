using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public static class SceneUtility
{
    
    public static GameObject GetGameObjectByPath(string sceneName, string url)
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
                if(_g.GetComponent<Light>()!=null)
                {
                    continue;
                }
            

                return _g;
            }
            return null;
        }
        else
        {
            return null;
        }

    }

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
                if(_g.GetComponent<Light>()!=null)
                {
                    continue;
                }
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
