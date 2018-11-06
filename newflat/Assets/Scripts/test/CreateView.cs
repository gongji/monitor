using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataModel;
using System.Linq;
using System;

public class CreateView : MonoBehaviour {

	void Start () {
		List<CameraView>  cameraviewList = new List<CameraView> ();


        CameraView area = new CameraView();
        area.id = "root";
        area.postion = new Vector3(12.7664f, 8.354657f, -6.289821f);
        area.angel = new Vector3(25.3f, -46.142f,0);
        cameraviewList.Add(area);

        CameraView room = new CameraView();
        room.id = "juliusuo_sn_f1_fj1";
        room.postion = new Vector3(0.9552342f, 101.0509f, -0.001728173f);
        room.angel = new Vector3(42.52f, -89.896f, 0);
        cameraviewList.Add(room);


        room = new CameraView();
        room.id = "juliusuo_sn_f1_fj2";
        room.postion = new Vector3(0.5140118f, 201.0653f, -0.005112677f);
        room.angel = new Vector3(60, -90.57001f, 0);
        cameraviewList.Add(room);

        FileUtils.WriteContent(Application.streamingAssetsPath + "/cameraview.bat", FileUtils.WriteType.Write,Utils.CollectionsConvert.ToJSON(cameraviewList));

	}
	
	// Update is called once per frame
	void Update () {
	    

        
	}
}
