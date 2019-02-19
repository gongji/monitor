using DataModel;
using DG.Tweening;
using State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BrowserToolBar : MonoBehaviour {

    private Transform reset;

    private Transform viewSwitch;

    private Transform qiangti;

    private Transform jidian;

    private Transform colorArea;

    private Transform tips;

    private Transform cameraMode;

    private Transform back;

    private Transform bim;

    public Transform manyou;

    public Transform viewGroup;

    private Transform temptureClound;
    private Transform fpsController;
    private Camera firstCamera;
    private Transform builderSwitch = null;

    public static BrowserToolBar instance;

    private void Awake()
    {
        instance = this;
    }

    List<Transform> buttonList = new List<Transform>();
    private void Start()
    {
        reset = transform.Find("reset");
       
        viewSwitch = transform.Find("viewSwitch");
       
        qiangti = transform.Find("qiangti");
      
        jidian = transform.Find("jidian");

        colorArea = transform.Find("colorArea");
      
        tips = transform.Find("tips");
        
        cameraMode = transform.Find("cameraMode");
        
        builderSwitch = transform.Find("builderSwitch");
       
        bim = transform.Find("bim");

        temptureClound = transform.Find("TemptureClound");

        back = transform.Find("back");

        manyou = transform.Find("manyou");

        viewGroup = transform.Find("viewGroup");

        TransformControlUtility.AddEventToBtn(reset.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { ViewReset();});
        TransformControlUtility.AddEventToBtn(colorArea.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { ColorAreaButton(); });
        TransformControlUtility.AddEventToBtn(back.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { BackAreaButton(); });
        TransformControlUtility.AddEventToBtn(cameraMode.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { CameraModeSwitch(); });
        TransformControlUtility.AddEventToBtn(builderSwitch.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { BuilderSwitch(); });
        TransformControlUtility.AddEventToBtn(viewGroup.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => {
            List<Object3dItem> wqList = SceneData.GetAllWq();
            if(wqList.Count >0)
            {
                Main.instance.stateMachineManager.SwitchStatus<BuilderState>(wqList[0].id, null, 0);
            }
            

        });
        jidian.GetComponent<Toggle>().onValueChanged.AddListener((bool value) => OnGuanWangToggleClick(jidian.GetComponent<Toggle>(), value));
      
        foreach(Transform child in transform)
        {
            buttonList.Add(child);
        }
    }

    #region toolBar control
 
    public void SetToolBarState()
    {
        transform.localScale = Vector3.one;

        IState mCurrentState = Main.instance.stateMachineManager.mCurrentState;


        if (AppInfo.currentView == ViewType.View2D && !(mCurrentState is BuilderState))
        {
            Switch2DButtonControl();
            return;
        }
        foreach (Transform  item in buttonList)
        {
            item.gameObject.SetActive(true);
        }

        if (mCurrentState is AreaState)
        {
            qiangti.gameObject.SetActive(false);
            jidian.gameObject.SetActive(false);
            tips.gameObject.SetActive(false);
            builderSwitch.gameObject.SetActive(false);
            bim.gameObject.SetActive(false);
            temptureClound.gameObject.SetActive(false);
            back.gameObject.SetActive(false);
        }
        else if (mCurrentState is RoomState)
        {
            colorArea.gameObject.SetActive(false);
            qiangti.gameObject.SetActive(false);
            jidian.gameObject.SetActive(false);
            builderSwitch.gameObject.SetActive(false);
            tips.gameObject.SetActive(false);
            bim.gameObject.SetActive(false);
            cameraMode.gameObject.SetActive(false);
            temptureClound.gameObject.SetActive(false);
            // fullArea.gameObject.SetActive(false);
            back.gameObject.SetActive(false);
            manyou.gameObject.SetActive(false);
            colorArea.gameObject.SetActive(false);
            viewGroup.gameObject.SetActive(false);
        }

        else if (mCurrentState is BuilderState)
        {
            reset.gameObject.SetActive(false);
            viewSwitch.gameObject.SetActive(false);
            qiangti.gameObject.SetActive(false);
            jidian.gameObject.SetActive(false);
            colorArea.gameObject.SetActive(false);
            tips.gameObject.SetActive(false);
            cameraMode.gameObject.SetActive(false);
            bim.gameObject.SetActive(false);
            temptureClound.gameObject.SetActive(false);
            manyou.gameObject.SetActive(false);
            back.gameObject.SetActive(false);
            colorArea.gameObject.SetActive(false);
            viewGroup.gameObject.SetActive(false);
        }
        else if (mCurrentState is FloorState)
        {
            colorArea.gameObject.SetActive(false);
            builderSwitch.gameObject.SetActive(false);
            manyou.gameObject.SetActive(false);
            back.gameObject.SetActive(false);
            colorArea.gameObject.SetActive(false);
            viewGroup.gameObject.SetActive(false);
        }
        else if (mCurrentState is ColorAreaState)
        {
            reset.gameObject.SetActive(false);
            viewSwitch.gameObject.SetActive(false);
            qiangti.gameObject.SetActive(false);
            jidian.gameObject.SetActive(false);
            tips.gameObject.SetActive(false);
            cameraMode.gameObject.SetActive(false);
            builderSwitch.gameObject.SetActive(false);
            bim.gameObject.SetActive(false);
            temptureClound.gameObject.SetActive(false);
            manyou.gameObject.SetActive(false);
            colorArea.gameObject.SetActive(false);
            viewGroup.gameObject.SetActive(false);

        }
        else if(mCurrentState is FullAreaState)
        {
            reset.gameObject.SetActive(false);
            viewSwitch.gameObject.SetActive(false);
            qiangti.gameObject.SetActive(false);
            jidian.gameObject.SetActive(false);
            tips.gameObject.SetActive(false);
            cameraMode.gameObject.SetActive(false);
            builderSwitch.gameObject.SetActive(false);
            bim.gameObject.SetActive(false);
            temptureClound.gameObject.SetActive(false);
            colorArea.gameObject.SetActive(false);
            manyou.gameObject.SetActive(false);
            viewGroup.gameObject.SetActive(false);

        }

        //temp Disable
        qiangti.gameObject.SetActive(false);
        if(AppInfo.Platform == BRPlatform.Browser)
        {
            viewSwitch.gameObject.SetActive(false);
        }
       
        viewGroup.gameObject.SetActive(false);
    }

    public void Switch2DButtonControl()
    {
        foreach (Transform item in buttonList)
        {
            item.gameObject.SetActive(false);
        }

        reset.gameObject.SetActive(true);
        viewSwitch.gameObject.SetActive(true);
    }

    #endregion 

    private bool guanxianSelect3d = false;

    /// <summary>
    /// 管网
    /// </summary>
    /// <param name="toggle"></param>
    /// <param name="value"></param>
    public void OnGuanWangToggleClick(Toggle toggle, bool value)
    {
        guanxianSelect3d = value;
        if (value)
        {
            MTMsg.ShowJiDian();
           
        }
        else
        {
            MTMsg.AllJiDianHide();
        }
       // Debug.Log("toggle change " + (value ? "On" : "Off"));
    }


    //视角复位
    public void ViewReset()
    {

        BaseState bs = AppInfo.GetCurrentState as BaseState;

        if(bs!=null && bs.baseEquipmentControl != null)
        {
            bs.baseEquipmentControl.CancelEquipment();
        }
        CameraInitSet.ResetCameraPostion();
    }

    private bool isColorModeAreaMode = false;
    private void ColorAreaButton()
    {
         Main.instance.stateMachineManager.SwitchStatus<ColorAreaState>("-1",null,0,"94");
        
    }

    private void BackAreaButton()
    {
        string sceneId = SceneData.GetIdByNumber(Constant.Main_dxName);
        Main.instance.stateMachineManager.SwitchStatus<AreaState>(sceneId);
    }


    private bool isFullModeAreaMod = false;
    //private void FullAreaButton()
    //{
    //    if (!isFullModeAreaMod)
    //    {
    //       // fullArea.GetComponentInChildren<Text>().text = "返回园区";
    //        Main.instance.stateMachineManager.SwitchStatus<FullAreaState>("-1", null, 0, "94");
    //    }
    //    else
    //    {
    //       // colorArea.GetComponentInChildren<Text>().text = "查看全景";
    //        string sceneId = SceneData.GetIdByNumber(Constant.Main_dxName);
    //        Main.instance.stateMachineManager.SwitchStatus<AreaState>(sceneId);
    //    }

    //    isFullModeAreaMod = !isFullModeAreaMod;
    //}

    public void FullAndColorAreaButtonReset()
    {
        colorArea.GetComponentInChildren<Text>().text = "查看能耗";

       // fullArea.GetComponentInChildren<Text>().text = "查看全景";

        isColorModeAreaMode = false;
        isFullModeAreaMod = false;
    }
    


   
    private void CameraModeSwitch()
    {
        ThirdSwitchMsg.instacne.SwitchMode(cameraMode);
    }


    private bool isExpandbuilder = true;
    private bool isEnaleClick = true;
    private Dictionary<string, object> dic = null;
    private void BuilderSwitch()
    {
        
        if(!isEnaleClick)
        {
            return;
        }
        if(dic==null)
        {
           dic = new Dictionary<string, object>();
        }

        dic.Clear();

        if (!isExpandbuilder)
        {
            builderSwitch.GetComponentInChildren<Text>().text = "楼层复位";
            dic.Add("value", "0");
        }
        else
        {
            builderSwitch.GetComponentInChildren<Text>().text = "楼层展开";
            dic.Add("value", "1");
        }
        EventMgr.Instance.SendEvent(EventName.FloorExpand, dic);
        isExpandbuilder = !isExpandbuilder;

        isEnaleClick = false;
        DOVirtual.DelayedCall(0.8f, () =>
        {
            isEnaleClick = true;
        });

    }

    #region MT
    /// <param name="isShow"></param>
    public void HideShowJiDian(bool isShow)
    {
        jidian.gameObject.SetActive(isShow);
    }

    public bool GetJiDianToggleState()
    {
        return guanxianSelect3d;
    }
#endregion

}