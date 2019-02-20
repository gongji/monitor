using UnityEngine;
using System.Collections;

public class AutoClear : MonoSingleton<AutoClear>
{
    public float timeStep = 5;

    private float curTime = 0;


    private float gbValue = 1024 * 1024;

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= timeStep)
        {
            Clear();
            System.GC.Collect();
            curTime = 0;
        }
        
    }

    public void Clear()
    {
       // Debug.Log("aaaaaaaaa");
       // Debug.Log("GetTotalUnusedReservedMemory=" + Profiler.GetTotalUnusedReservedMemory() / gbValue);
        //预订
        //Debug.Log("GetTotalReservedMemory=" + Profiler.GetTotalReservedMemory() / gbValue);
        //分配
      //  Debug.Log("GetTotalAllocatedMemory=" + Profiler.GetTotalAllocatedMemory() / gbValue);
        Resources.UnloadUnusedAssets();
       
    }
}
