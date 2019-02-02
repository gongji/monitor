﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScripts : MonoBehaviour {

    public void AddScirpts()
    {
        gameObject.AddComponent<DownLoader>();
        gameObject.AddComponent<MouseCheck>();
        gameObject.AddComponent<CameraViewChangeManager>();
        gameObject.AddComponent<PlatformMsg>();
        gameObject.AddComponent<EffectionResouceLoader>();
        gameObject.AddComponent<Fps>();
    }

    public void InitDataAndLogin()
    {
        CameraInitSet.SystemInitCamera();
        Battlehub.UIControls.TreeViewControl.Instance.Init();
        ModelData.InitModelData();

        //FlyingTextMsg.instance.LoadFontAsset();
        FontResouceMsg.Instance.Init();
        string isLogin = Config.parse("isLogin");
        if (isLogin.Equals("0"))
        {
            string url = Application.streamingAssetsPath + "/UI/" + PlatformMsg.instance.currentPlatform.ToString() + "/login";
            ResourceUtility.Instance.GetHttpAssetBundle(url, (result) => {

                GameObject login = GameObject.Instantiate(result);
                login.AddComponent<UserLogin>();



            }, "login");
        }
        else
        {
            SceneJump.JumpFirstPage();
        }
    }



}