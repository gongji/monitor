using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateControlManager : MonoSingleton<OperateControlManager> {

    private EquipmentEditState currentState = EquipmentEditState.None;

    public EquipmentEditState CurrentState
    {
        get
        {
            return currentState;
        }
        set

        {
            currentState = value;
            ChangeState();
        }
    }

 
    public enum EquipmentEditState
    {
        None,
        BulkCopy,
        Edit
    }

    private OperateTips operateTips;
   
    private void ChangeState()
    {
        if(operateTips)
        {
            operateTips.HideTips();
        }

        if(ebc)
        {
            GameObject.Destroy(ebc);
        }

        if(currentState != EquipmentEditState.None)
        {
            operateTips = OperateTips.instance;
        }
        else
        {
            UIElementCommandBar.instance.DestroyGizmo();
        }

        if(operateTips!=null && currentState == EquipmentEditState.Edit)
        {
            operateTips.Show("快捷键：数字1位置，数字2旋转，数字3缩放。");

        }else if(operateTips != null && currentState == EquipmentEditState.BulkCopy)
        {
            operateTips.Show("shift水平垂直复制，滚轴+左ctrl调整距离,ESC键取消操作。");
        }
    }

    private EquipmentBatchCopy ebc;
    public void BatchCopyEquipment(Transform equipment)
    {
        if(!equipment)
        {

            return;
        }
        if(ebc!=null)
        {
            GameObject.Destroy(ebc);
        }

        ebc = equipment.gameObject.AddComponent<EquipmentBatchCopy>();
        ebc.SetCreateState();
    }
}


