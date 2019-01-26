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

    private Transform fullArea;

    private Transform bim;

     private Transform temptureClound;
    private Transform fpsController;
    private Camera firstCamera;

    protected Texture2D m_FirstPersonIcon = null;

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

        fullArea = transform.Find("fullArea");


        TransformControlUtility.AddEventToBtn(reset.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { ViewReset();});

        
        TransformControlUtility.AddEventToBtn(colorArea.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { ColorAreaButton(); });

        TransformControlUtility.AddEventToBtn(fullArea.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { FullAreaButton(); });

        TransformControlUtility.AddEventToBtn(cameraMode.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { CameraModeSwitch(); });

        TransformControlUtility.AddEventToBtn(builderSwitch.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { BuilderSwitch(); });


        jidian.GetComponent<Toggle>().onValueChanged.AddListener((bool value) => OnGuanWangToggleClick(jidian.GetComponent<Toggle>(), value));
      
        m_FirstPersonIcon = Resources.Load("UI/frist_target") as Texture2D;

        foreach(Transform child in transform)
        {
            buttonList.Add(child);
        }

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
            jidian.gameObject.SetActive(false);
            tips.gameObject.SetActive(false);
            builderSwitch.gameObject.SetActive(false);
            bim.gameObject.SetActive(false);
            temptureClound.gameObject.SetActive(false);
            

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
            fullArea.gameObject.SetActive(false);
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
            fullArea.gameObject.SetActive(false);
        }
        else if (mCurrentState is FloorState)
        {
            colorArea.gameObject.SetActive(false);
            builderSwitch.gameObject.SetActive(false);
            fullArea.gameObject.SetActive(false);
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
            fullArea.gameObject.SetActive(false);
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
            JiDaianMsg.ShowJiDian();
           
        }
        else
        {
            JiDaianMsg.AllJiDianHide();
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

        if(!isColorModeAreaMode)
        {
            colorArea.GetComponentInChildren<Text>().text = "返回园区";
            Main.instance.stateMachineManager.SwitchStatus<ColorAreaState>("-1",null,0,"94");
        }
        else
        {
            colorArea.GetComponentInChildren<Text>().text = "查看能耗";
            string sceneId = SceneData.GetIdByNumber(Constant.Main_dxName);
            Main.instance.stateMachineManager.SwitchStatus<AreaState>(sceneId);
        }

        isColorModeAreaMode = !isColorModeAreaMode;
    }


    private bool isFullModeAreaMod = false;
    private void FullAreaButton()
    {
        if (!isFullModeAreaMod)
        {
            fullArea.GetComponentInChildren<Text>().text = "返回园区";
            Main.instance.stateMachineManager.SwitchStatus<FullAreaState>("-1", null, 0, "94");
        }
        else
        {
            colorArea.GetComponentInChildren<Text>().text = "查看全景";
            string sceneId = SceneData.GetIdByNumber(Constant.Main_dxName);
            Main.instance.stateMachineManager.SwitchStatus<AreaState>(sceneId);
        }

        isFullModeAreaMod = !isFullModeAreaMod;
    }

    public void FullAndColorAreaButtonReset()
    {
        colorArea.GetComponentInChildren<Text>().text = "查看能耗";

        colorArea.GetComponentInChildren<Text>().text = "查看全景";

        isColorModeAreaMode = false;
        isFullModeAreaMod = false;
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
            ThirdSwitchMsg.instacne.SwitchThirdPerson();
            UIUtility.ShowTips("您已进入人物模式，按Q键切换键飞行模式。");
        }
        else
        {
            cameraMode.GetComponentInChildren<Text>().text = "飞行模式";
            ThirdSwitchMsg.instacne.SwitchNormalCamera();
            
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
        return;
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


    #region jidian
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