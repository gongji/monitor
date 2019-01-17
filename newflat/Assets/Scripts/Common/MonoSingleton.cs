using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// MonoBegaviour 单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {

    private static T _instance;
    private static object _lock = new object();
    private static bool applicationIsQuitting = false;
    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
            
                // Debug.Log("[Singleton] Instance " + typeof(T) +" already destroyed on application quit." +"Won't create again - returning null.");
                return null;
            }
            lock (_lock)
            {
                if (_instance == null)
                {
                    
                    _instance = (T)FindObjectOfType(typeof(T));
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).ToString();
                        singleton.hideFlags = HideFlags.HideInHierarchy;
                        DontDestroyOnLoad(singleton);
                    }
                }
                return _instance;
            }
        }
    }

    /// <summary>
    /// When unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.    
    /// If any script calls Instance after it have been destroyed,     
    ///   it will create a buggy ghost object that will stay on the Editor scene    
    ///   even after stopping playing the Application. Really bad!    
    /// So, this was made to be sure we're not creating that buggy ghost object.    
    /// </summary>
    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
