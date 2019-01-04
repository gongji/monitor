using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 interface ITimer  {

     void StartTimer();

     void StopTimer();

    void DoTimer();

    int Time
    {
        get;
        set;
    }


}
