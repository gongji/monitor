using Core.Common.Logging;
using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NormalEquipmentControl :BaseEquipmentControl {

    private ILog log = LogManagers.GetLogger("NormalEquipmentControl");
    private GameObject equipmentTips;

   // public bool isUpdate = true;
 
    void Start () {
        equipmentItem = GetComponent<Object3DElement>().equipmentData;
        Transform t = GameObject.Find("Canvas/equipment").transform;
        equipmentTips = TransformControlUtility.CreateItem("Tips/EquipmentTips", t);
        string name = equipmentItem.name == null ? "默认" : equipmentItem.name;
        equipmentTips.name = name;
        equipmentTips.SetActive(false);
        equipmentTips.GetComponentInChildren<TextMeshProUGUI>().text = name;

    }

    //是否显示标签
   
    private IState istate;
	void Update () {


        return;
        if (!isShowTips)
        {
            equipmentTips.SetActive(false);
            return;
        }
   
        if (IsRoomState())
        {
            equipmentTips.SetActive(true);
        }
            
        if (equipmentTips != null && equipmentTips.activeSelf)
        {
            
            equipmentTips.transform.GetComponent<RectTransform>().anchoredPosition = UIUtility.WorldToUI(GetBoxTopPostion(), Camera.main);
        }
    }

    private bool IsRoomState()
    {
        if (Main.instance != null && Main.instance.stateMachineManager != null)
        {
            istate = Main.instance.stateMachineManager.mCurrentState;
            if (istate is RoomState)
            {
                return true;
            }
        }
        return false;
    }

    
    /// <summary>
    /// 获取box的位置
    /// </summary>
    /// <returns></returns>
    private Vector3 GetBoxTopPostion()
    {
        Vector3 result = Vector3.zero;

        Bounds bounds = transform.GetComponentInChildren<BoxCollider>().bounds;
       result = bounds.center + Vector3.up * bounds.size.y / 2.0f;
      
        return result;
    }

    //private void OnEnable()
    //{
    //    if(equipmentTips!=null && IsRoomState())
    //    {
    //        equipmentTips.SetActive(true);
    //    }
        
    //}

    //private void OnDisable()
    //{
    //    if (equipmentTips != null)
    //    {
    //        equipmentTips.SetActive(false);
    //    }
    //}

   

    public void OnMouseEnter()
    {

        // Debug.Log("OnMouseEnter");
        //string equipmentid = ViewEquipmentInfo.Instance.GetCurrentSelectDeive();
        //if(equipmentid.Equals(equipmentItem.id))
        //{
        //    log.Debug("already selected");
        //    return;
        //}
        //EffectionUtility.playSelectingEffect(transform);
        //if(!IsRoomState())
        //{
        //    equipmentTips.SetActive(true);
        //}
        equipmentTips.SetActive(true);
        equipmentTips.transform.GetComponent<RectTransform>().anchoredPosition = UIUtility.WorldToUI(GetBoxTopPostion(), Camera.main);
    }

    public void OnMouseExit()
    {

        // Debug.Log("OnMouseExit");
        //EffectionUtility.StopFlashingEffect(transform);
        //if (!IsRoomState())
        //{
        //    equipmentTips.SetActive(false);
        //}
        equipmentTips.SetActive(false);
    }

    protected bool isShowTips = true;

    public void SetTipsShow(bool isEnable)
    {
        isShowTips = isEnable;
    }
    //定位设备
    public override void SelectEquipment()
    {
        base.SelectEquipment();

    }

    public override void OnMouseClick() {
        ShowTestPoint.Show(equipmentItem.name, equipmentItem.id);
    }
    

}
