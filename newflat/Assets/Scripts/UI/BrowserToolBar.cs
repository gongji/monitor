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

    private Transform guanxian;

    private Transform fullArea;

    private Transform tips;

    private Transform cameraMode;

    private Transform bim;

    public static BrowserToolBar instance;

    private Transform mainCamera;
    private Transform firstFPSController;
    private Camera firstCamera;

    protected Texture2D m_FirstPersonIcon = null;

    private Transform builderSwitch = null;

    private void Awake()
    {

        instance = this;
    }

    List<Transform> buttonList = new List<Transform>();
    private void Start()
    {
        reset = transform.Find("reset");
        buttonList.Add(reset);
        viewSwitch = transform.Find("viewSwitch");
        buttonList.Add(viewSwitch);
        qiangti = transform.Find("qiangti");
        buttonList.Add(qiangti);
        guanxian = transform.Find("guanxian");
        buttonList.Add(guanxian);
        fullArea = transform.Find("fullArea");
        buttonList.Add(fullArea);
        tips = transform.Find("tips");
        buttonList.Add(tips);
        cameraMode = transform.Find("cameraMode");
        buttonList.Add(cameraMode);
        builderSwitch = transform.Find("builderSwitch");
        buttonList.Add(builderSwitch);

        bim = transform.Find("bim");
        buttonList.Add(bim);


        TransformControlUtility.AddEventToBtn(reset.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { ViewReset();});

        TransformControlUtility.AddEventToBtn(fullArea.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { FullAreaButton(); });

        TransformControlUtility.AddEventToBtn(cameraMode.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { CameraModeSwitch(); });

        TransformControlUtility.AddEventToBtn(builderSwitch.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { BuilderSwitch(); });


        guanxian.GetComponent<Toggle>().onValueChanged.AddListener((bool value) => OnGuanWangToggleClick(guanxian.GetComponent<Toggle>(), value));
        mainCamera = Camera.main.transform;
        if(GameObject.Find("FPSController")!=null)
        {
            firstFPSController = GameObject.Find("FPSController").transform;
            firstCamera = firstFPSController.GetComponentInChildren<Camera>(true);
            firstFPSController.gameObject.SetActive(false);
        }
        m_FirstPersonIcon = Resources.Load("UI/frist_target") as Texture2D;

    }

    #region toolBar control
    /// <summary>
    /// 设置工具条的显示隐藏
    /// </summary>
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
            guanxian.gameObject.SetActive(false);
            tips.gameObject.SetActive(false);
            builderSwitch.gameObject.SetActive(false);
            bim.gameObject.SetActive(false);

        }
        else if (mCurrentState is RoomState)
        {
            fullArea.gameObject.SetActive(false);
            qiangti.gameObject.SetActive(false);
            guanxian.gameObject.SetActive(false);
            builderSwitch.gameObject.SetActive(false);
            tips.gameObject.SetActive(false);
            bim.gameObject.SetActive(false);
            cameraMode.gameObject.SetActive(false);
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
            bim.gameObject.SetActive(false);
        }
        else if (mCurrentState is FloorState)
        {
            fullArea.gameObject.SetActive(false);
            builderSwitch.gameObject.SetActive(false);
        }
        else if (mCurrentState is FullAreaState)
        {
            reset.gameObject.SetActive(false);
            viewSwitch.gameObject.SetActive(false);
            qiangti.gameObject.SetActive(false);
            guanxian.gameObject.SetActive(false);
            tips.gameObject.SetActive(false);
            cameraMode.gameObject.SetActive(false);
            builderSwitch.gameObject.SetActive(false);
            bim.gameObject.SetActive(false);
        }

        //temp Disable
        qiangti.gameObject.SetActive(false);
       // viewSwitch.gameObject.SetActive(false);
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
            Main.instance.stateMachineManager.SwitchStatus<FullAreaState>("-1",null,0,"241");
        }
        else
        {
            fullArea.GetComponentInChildren<Text>().text = "全景模式";
            string sceneId = SceneData.GetIdByNumber(Constant.Main_dxName);
            Main.instance.stateMachineManager.SwitchStatus<AreaState>(sceneId);
        }

        isFullModeAreaMode = !isFullModeAreaMode;
    }


    private bool isFlyCameraMode = true;
    public bool IsFlyCameraMode
    {
        get

        {
            return isFlyCameraMode;
        }
    }
    private void CameraModeSwitch()
    {
        if (isFlyCameraMode)
        {
            cameraMode.GetComponentInChildren<Text>().text = "人物模式";
            mainCamera.gameObject.SetActive(false);

            GameObject point = ManyouMsg.GetManYouPoint();
            if(point==null)
            {
                firstFPSController.transform.position = mainCamera.transform.position - Vector3.up * 0.6f;
            }
            else
            {
                firstFPSController.transform.position = point.transform.position;
            }
           
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


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && !isFlyCameraMode)
        {
            CameraModeSwitch();
        }

        //进入人物模式
        
    }

    private  void OnGUI()
    {
        if (!isFlyCameraMode)
        {
            Cursor.visible = false;

#if UNITY_EDITOR
            int nSize = 32;
#else
           int nSize = Screen.width > 1920 ? (Screen.width * 32) / 1920 : 32;
#endif


            DrawCursor(new Vector3(Screen.width >> 1, (Screen.height + nSize) >> 1), m_FirstPersonIcon, nSize);
        }
        else
        {
            Cursor.visible = true;
        }
    }
    private void DrawCursor(Vector3 mousePosition, Texture2D texture, int nIconSize)
    {
        GUI.DrawTexture(new Rect(mousePosition.x - (nIconSize >> 1), Screen.height - mousePosition.y - (nIconSize >> 1), nIconSize, nIconSize), texture);
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