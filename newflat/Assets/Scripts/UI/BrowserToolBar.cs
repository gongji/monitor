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

    private Transform tips;

    private Transform cameraMode;

    public static BrowserToolBar instance;

    private Transform mainCamera;
    private Transform firstFPSController;
    private Camera firstCamera;

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
        tips = transform.Find("tips");
        cameraMode = transform.Find("cameraMode");

        TransformControlUtility.AddEventToBtn(reset.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { ViewReset();});

        TransformControlUtility.AddEventToBtn(fullArea.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { FullAreaButton(); });

        TransformControlUtility.AddEventToBtn(cameraMode.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { CameraModeSwitch(); });


        guanxian.GetComponent<Toggle>().onValueChanged.AddListener((bool value) => OnGuanWangToggleClick(guanxian.GetComponent<Toggle>(), value));
        mainCamera = Camera.main.transform;
        firstFPSController = GameObject.Find("FPSController").transform;
        firstCamera = firstFPSController.GetComponentInChildren<Camera>(true);
        firstFPSController.gameObject.SetActive(false);


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
        tips.gameObject.SetActive(true);
        cameraMode.gameObject.SetActive(true);


        IState mCurrentState = Main.instance.stateMachineManager.mCurrentState;
        if (mCurrentState is AreaState)
        {
            qiangti.gameObject.SetActive(false);
            guanxian.gameObject.SetActive(false);
            tips.gameObject.SetActive(false);
        }
        else if (mCurrentState is RoomState)
        {
            fullArea.gameObject.SetActive(false);
            qiangti.gameObject.SetActive(false);
            guanxian.gameObject.SetActive(false);
        }

        else if (mCurrentState is BuilderState)
        {
            reset.gameObject.SetActive(false);
            viewSwitch.gameObject.SetActive(false);
            qiangti.gameObject.SetActive(false);
            guanxian.gameObject.SetActive(false);
            fullArea.gameObject.SetActive(false);
            tips.gameObject.SetActive(false);
            cameraMode.gameObject.SetActive(false);
        }


        else if (mCurrentState is FloorState)
        {
            fullArea.gameObject.SetActive(false);

        }
        else if (mCurrentState is FullAreaState)
        {
            reset.gameObject.SetActive(false);
            viewSwitch.gameObject.SetActive(false);
            qiangti.gameObject.SetActive(false);
            guanxian.gameObject.SetActive(false);
            tips.gameObject.SetActive(false);
            cameraMode.gameObject.SetActive(false);
        }
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

    private bool isFullModeAreaMode = false;
    private void FullAreaButton()
    {

        if(!isFullModeAreaMode)
        {
            fullArea.GetComponentInChildren<Text>().text = "退出全景";
            //外构的id暂时写死
            Main.instance.stateMachineManager.SwitchStatus<FullAreaState>("-1",null,0,"147");
        }
        else
        {
            fullArea.GetComponentInChildren<Text>().text = "全景模式";
            Main.instance.stateMachineManager.SwitchStatus<AreaState>(string.Empty);
        }

        isFullModeAreaMode = !isFullModeAreaMode;
    }


    private bool isFlyCameraMode = false;
    private void CameraModeSwitch()
    {
        if (!isFlyCameraMode)
        {
            cameraMode.GetComponentInChildren<Text>().text = "人物模式";
            mainCamera.gameObject.SetActive(false);

            firstFPSController.transform.position = mainCamera.transform.position - Vector3.up * 0.6f;
            firstFPSController.transform.rotation = mainCamera.transform.rotation;
            firstFPSController.gameObject.SetActive(true);
            firstCamera.enabled = true;
            UIUtility.ShowTips("当前进入人物模式，按Q键切换键飞行模式。");
        }
        else
        {
            cameraMode.GetComponentInChildren<Text>().text = "飞行模式";
            mainCamera.transform.position = firstCamera.transform.position;
            mainCamera.transform.rotation = firstCamera.transform.rotation;
            mainCamera.gameObject.SetActive(true);
            firstFPSController.gameObject.SetActive(false);
        }

        isFlyCameraMode = !isFlyCameraMode;
    }


    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q) && isFlyCameraMode)
        {
            CameraModeSwitch();
        }
    }


    #region wangwang
    /// <param name="isShow"></param>
    public void HideShowGuanwang(bool isShow)
    {
        guanxian.gameObject.SetActive(isShow);
    }

    public bool GetGuanWangToggleState()
    {
        return guanxianSelect3d;
    }
    #endregion

}