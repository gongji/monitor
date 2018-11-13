using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlehub.UIControls;
using UnityEngine.EventSystems;

public class EditStatus : AppBaseState
{

    public EditStatus()
    {

    }

    public override void Init()
    {
        EditUI.Create();
        TreeViewControl.Instance.SetEditData();
    }
    
    public override void Update()
    {
        if ((EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()))
        {
            
            return;
        }
    
        base.Update();
        hitTransform = null;
        checkSelectEquipment = CheckClickEquipment(ref hitTransform);
        if (checkSelectEquipment!=null && hitTransform.Equals(MouseCheck.clickHitTransform))
        {
            Debug.Log("编辑态单击设备");
            UIElementCommandBar.instance.SelectEquipment(checkSelectEquipment.gameObject);
            return;
        }
       
        //双击隐藏菜单,操作模式复位
        if(MouseCheck.DOUBLE_CLICK)
        {
            UIElementCommandBar.instance.Hide();
            OperateControlManager.Instance.CurrentState = OperateControlManager.EquipmentEditState.None;
            return;
        }

        Vector3 hitPoint = Vector3.zero;
        //单击场景，生成设备
        if (CheckClickScene(ref hitTransform, ref hitPoint) && modelPrefeb!=null)
        {
            if(hitTransform.Equals(MouseCheck.clickHitTransform))
            {
                CreateEquipment.Create(modelPrefeb, hitPoint, hitTransform);
            }
           
        }

        RefreshPosition();
    }
    private GameObject modelPrefeb = null;
    protected void RefreshPosition()
    {
        modelPrefeb = ShowModelList.instance.prefebGameObject;
        if (modelPrefeb == null)
            return;
        Vector3 point = Vector3.zero;
        if (GetCollisionObject(ref point))
        {
            modelPrefeb.transform.position = point;
        }
    }

    /// <summary>
    /// 获取碰撞点
    /// <param>在创建物体时使用</param>
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    private bool GetCollisionObject(ref Vector3 point)
    {
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask rayMask = 1 << Constant.SceneLayer;
      
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayMask))
        {
            point = hit.point;
            return true;
        }

        return false;
    }

    public override void OnGUI()
    {

    }

    public override void UpdateUI()
    {
    }

    
}


