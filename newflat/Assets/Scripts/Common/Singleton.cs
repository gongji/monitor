using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 类单例模板
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton <T> where T : class, new(){

    private static T instance = null;
    private static readonly object synlock = new object();

    public static T Instance {
        get {
            if (instance == null)
            {
                lock (synlock)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }
            return instance;
        }
    }

}
