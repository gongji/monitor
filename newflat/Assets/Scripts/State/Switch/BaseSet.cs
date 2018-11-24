using DataModel;
using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSet
{

    protected List<Object3dItem> currentlist;
    protected void SwitchBG(bool isArea)
    {
            GameObject renderGameObjerct = SceneUtility.GetGameByRootName(Constant.SkyboxName, "main");
            if (renderGameObjerct != null)
            {
                if (isArea)
                {
                    renderGameObjerct.GetComponent<RenderSettingsValue>().SetRenderSettings();
                }
                else
                {
                    renderGameObjerct.GetComponent<RenderSettingsValue>().SwitchNoArea(true);

                }

            }
            else
            {
                RenderSettingsValue.SetNoAreaEffction();
            }
       
    }

    protected void ShowOrHideScene( bool isShow)
    {
        //  Debug.Log("isShow="+ isShow.ToString());

       // Debug.Log("退出场景");
        foreach (Object3dItem item in currentlist)
        {
           // Debug.Log("item.number="+ item.number);
            if(!item.number.EndsWith(Constant.DX))
            {
                GameObject root = SceneUtility.GetGameByRootName(item.number, item.number);
                if (root != null)
                {
                    root.SetActive(isShow);
                }
            }
            else
            {
                SceneUtility.SetRootGameObjects(item.number, isShow);
            }
           
        }
    }

    protected void EnableOrDisableCamera(bool isEnable,bool isEnableBoundsCheck = true)
    {

        return;
        //if(isEnable)
        //{
        //    CameraObjectController acc = Camera.main.gameObject.AddComponent<CameraObjectController>();
        //    acc.SetCamerRotation();
        //}
        //else
        //{
        //    CameraObjectController acc = Camera.main.gameObject.GetComponent <CameraObjectController>();
        //    if(acc!=null)
        //    {
        //        GameObject.Destroy(acc);
        //    }
        //    acc.SetCheckPositionRange(isEnableBoundsCheck);
        //}
    }

    


    public virtual void Enter(List<Object3dItem> currentData, System.Action callBack) {
        this.currentlist = currentData;
        if (AppInfo.Platform == BRPlatform.Browser )
        {
            BrowserToolBar.instance.SetToolBarState();
        }
        ShowOrHideScene(true);
    }
    //带动画的退出
    public virtual void Exit(string nextid, System.Action callBack) {
      //  EnableOrDisableCamera(false);

    }
    //不带动画，直接退出
    public virtual void Exit(string nextid) {
        //EnableOrDisableCamera(false);
        TipsMgr.Instance.DeleteTips();
        ShowOrHideScene(false);
    }



    protected Quaternion cameraRoation = Quaternion.identity;
    protected Vector3 cameraPostion = Vector3.zero;
    /// <summary>
    /// 计算相机的位置
    /// </summary>
   
       

 }
