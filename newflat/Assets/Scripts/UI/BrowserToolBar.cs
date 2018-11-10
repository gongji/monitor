using DataModel;
using State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrowserToolBar : MonoBehaviour {

    private Transform reset;

    private Transform viewSwitch;

    private Transform qiangti;

    private Transform guanxian;

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
     
        TransformControlUtility.AddEventToBtn(reset.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { ViewReset();});

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

    public void SetToolBarState()
    {
        transform.localScale = Vector3.one;
        reset.gameObject.SetActive(true);
        viewSwitch.gameObject.SetActive(true);
        qiangti.gameObject.SetActive(true);
        guanxian.gameObject.SetActive(true);

        IState mCurrentState = Main.instance.stateMachineManager.mCurrentState;
        if (mCurrentState is AreaState || mCurrentState is RoomState)
        {
            qiangti.gameObject.SetActive(false);
            guanxian.gameObject.SetActive(false);
        }
       
        else if(mCurrentState is BuilderState)
        {
            reset.gameObject.SetActive(false);
            viewSwitch.gameObject.SetActive(false);
            qiangti.gameObject.SetActive(false);
            guanxian.gameObject.SetActive(false);

        }

        
    }
    
}
