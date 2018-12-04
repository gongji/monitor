using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CreateSubsystem  {

    public static void Create(string sceneid)
    {
        SubSystemProxy.GetSubSystemByScene((result) => {
            Debug.Log("result=" + result);

        }, sceneid);
    }

    public static void Delete()
    {

    }
}
