﻿using UnityEngine;
using System.Collections;
using SystemCore.Task;

public class TestDown : MonoBehaviour {

	// Use this for initialization
	private ITaskQueue taskQueue;
	void Start () {

       // MouseType mouseType =  MouseType.Instance;
		// taskQueue = new TaskQueue (this);
		//string path1 = "http://127.0.0.1:8080/test/Cube.unity3d";


  //      ABModelDownloadTask abModelDownloadTask = new ABModelDownloadTask(path1,path1);
		//taskQueue.Add (abModelDownloadTask);

		//string path3 ="http://127.0.0.1:8080/test/abm_wq.unity3d";

		//SceneDownloadTask sceneDownloadTask3 =new SceneDownloadTask(path3,path3);

	 //  taskQueue.Add (sceneDownloadTask3);

		//taskQueue.StartTask ();
		//taskQueue.OnFinish = () => {
		//	Debug.Log ("OnFinish");
  //          AssetBundle ab = abModelDownloadTask.Data as AssetBundle;
  //          GameObject.Instantiate( ab.mainAsset);

  //      };

	}
	
	// Update is called once per frame
	void Update () {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            Debug.Log(hit.transform);
            //clickHitTransform = hit.transform;
            //clickHitPoint = hit.point;
        }
    }
}