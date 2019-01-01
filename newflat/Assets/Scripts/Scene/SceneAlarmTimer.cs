﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAlarmTimer :MonoSingleton<SceneAlarmTimer>,ITimer {

    private int time = 5;
    public int Time
    {
        get
        {
            return time;
        }

        set
        {
            time = value;
        }

       
    }
    public void StartTimer()
    {
        StartCoroutine(Start());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            UpdateData();
            yield return new WaitForSeconds(time);
        }
    }

    private void UpdateData()
    {
       
    }
}