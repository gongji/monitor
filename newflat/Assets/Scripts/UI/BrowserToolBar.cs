using DataModel;
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

    private Transform guanxian;

    private Transform fullArea;

    public static BrowserToolBar instance;

    private void Awake()
    {

        instance = this;
    }

    private void Start()
    {
        reset = transform.Find("reset");
        viewSwitch = transform.Find("viewSwitch");
        qiangti = transform.Find("qiangti");
        guanxian = transform.Find("guanxian");
        fullArea = transform.Find("fullArea");

        TransformControlUtility.AddEventToBtn(reset.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { ViewReset();});

        TransformControlUtility.AddEventToBtn(fullArea.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { FullAreaButton(); });

        guanxian.GetComponent<Toggle>().onValueChanged.AddListener((bool value) => OnGuanWangToggleClick(guanxian.GetComponent<Toggle>(), value));

    }

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
            GuanWangMsg.ShowGuanWangShow();
        }
        else
        {
            GuanWangMsg.AllGuanWangHide();
        }
        Debug.Log("toggle change " + (value ? "On" : "Off"));
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

    private bool isFullModeAreaMode = false;
    private void FullAreaButton()
    {

        if(!isFullModeAreaMode)
        {
            fullArea.GetComponentInChildren<Text>().text = "退出全景";
        }
        else
        {
            fullArea.GetComponentInChildren<Text>().text = "全景模式";
        }

        isFullModeAreaMode = !isFullModeAreaMode;
    }

    /// <summary>
    /// 设置工具条的显示隐藏
    /// </summary>
    public void SetToolBarState()
    {
        transform.localScale = Vector3.one;
        reset.gameObject.SetActive(true);
        viewSwitch.gameObject.SetActive(true);
        qiangti.gameObject.SetActive(true);
        guanxian.gameObject.SetActive(true);
        fullArea.gameObject.SetActive(true);

        IState mCurrentState = Main.instance.stateMachineManager.mCurrentState;
        if (mCurrentState is AreaState)
        {
            qiangti.gameObject.SetActive(false);
            guanxian.gameObject.SetActive(false);
        }
        else if(mCurrentState is RoomState)
        {
            fullArea.gameObject.SetActive(false);
            qiangti.gameObject.SetActive(false);
            guanxian.gameObject.SetActive(false);
        }
       
        else if(mCurrentState is BuilderState)
        {
            reset.gameObject.SetActive(false);
            viewSwitch.gameObject.SetActive(false);
            qiangti.gameObject.SetActive(false);
            guanxian.gameObject.SetActive(false);
            fullArea.gameObject.SetActive(false);

        }
        else if(mCurrentState is FloorState)
        {
            fullArea.gameObject.SetActive(false);

        }
        else if(mCurrentState is FullAreaState)
        {
            reset.gameObject.SetActive(false);
            viewSwitch.gameObject.SetActive(false);
            qiangti.gameObject.SetActive(false);
            guanxian.gameObject.SetActive(false);
        }

        
    }

    public void HideShowGuwang(bool isShow)
    {
        guanxian.gameObject.SetActive(isShow);
    }

    public bool GetWangWangToggleState()
    {
        return guanxianSelect3d;
    }
    
}
