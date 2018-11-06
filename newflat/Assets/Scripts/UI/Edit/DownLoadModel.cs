using System.Collections;
using System.Collections.Generic;
using SystemCore.Task;
using UnityEngine;

public class DownLoadModel : MonoBehaviour {

	
    public static void Start(string modelid)
    {
        string downequipmentPath = Config.parse("downPath") + "model/";
      
        string path = downequipmentPath + modelid;

        ABModelDownloadTask abDownloadTask = new ABModelDownloadTask(path, path, modelid);
        
      
    }
}
