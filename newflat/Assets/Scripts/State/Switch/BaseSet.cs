using DataModel;
using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSet
{
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

    protected void ShowOrHideScene(List<Object3dItem> items, bool isShow)
    {
        foreach (Object3dItem temp in items)
        {
            GameObject root = SceneUtility.GetGameByRootName(temp.number, temp.number);
            root.SetActive(isShow);
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
        if(AppInfo.Platform == BRPlatform.Browser )
        {
            BrowserToolBar.instance.SetToolBarState();
        }
    }
    //带动画的退出
    public virtual void Exit(string nextid, System.Action callBack) {
      //  EnableOrDisableCamera(false);

    }
    //不带动画，直接退出
    public virtual void Exit(string nextid) {
        //EnableOrDisableCamera(false);
        TipsMgr.Instance.DeleteTips();
    }



    protected Quaternion cameraRoation = Quaternion.identity;
    protected Vector3 cameraPostion = Vector3.zero;
    /// <summary>
    /// 计算相机的位置
    /// </summary>
   
       

 }
