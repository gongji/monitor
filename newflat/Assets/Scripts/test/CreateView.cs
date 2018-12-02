using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataModel;
using System.Linq;
using System;
using Utils;

public class CreateView : MonoBehaviour {

	void Start () {


        CameraViewItem cameraView = new CameraViewItem();
        cameraView.sceneId = "-1";
        cameraView.x = 512.0f;

        Dictionary<string, string> dic = new Dictionary<string, string>();

        dic.Add("result", CollectionsConvert.ToJSON(cameraView));
        Debug.Log(CollectionsConvert.ToJSON(dic));
        CameraViewProxy.SaveCameraview((rersult) =>
        {
           Debug.Log(rersult);
        }, dic);

        //string sql = "equipId = 123";

        //Dictionary<string, string> dic = new Dictionary<string, string>();
        //dic.Add("result", sql); ;

        //CameraViewProxy.GetCameraView(dic, (result) =>
        //{
        //    Debug.Log(result);
        //}, (error) => { });

        //CameraView cameraView = Utils.CollectionsConvert.ToObject<CameraView>("");
        //Debug.Log(cameraView);




    }

    // Update is called once per frame
    void Update () {
	    

        
	}
}
