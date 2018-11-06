using UnityEngine;

/// <summary>
/// Generic CSharp Singleton.
/// </summary>
public abstract class SingletonCS<T> where T : SingletonCS<T>, new() {

    private static T instance;
    private static readonly object syncObject = new object();

    protected SingletonCS() {
        if (instance != null) {
            Debug.LogError(string.Format("{0}的单例已存在，不能重复创建该对象。", typeof(T).Name));
        }
    }

    public static T Instance {
        get {
            if (instance == null) {
                lock (syncObject) {
                    if (instance == null) {
                        instance = new T();
                        instance.InitFunction();
                    }
                }
            }
            return instance;
        }
    }

    protected abstract void InitFunction();
    protected abstract void DestroyFunction();

    ~SingletonCS() {
        DestroyFunction();
    }

}