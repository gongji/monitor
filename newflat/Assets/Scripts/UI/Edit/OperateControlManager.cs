using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 设备操控的状态
/// </summary>
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
    /// <summary>
    /// 改变状态处理标签和
    /// </summary>
    private void ChangeState()
    {
        if(operateTips)
        {
            operateTips.DestroryGameObject();
        }

        if(ebc)
        {
            GameObject.Destroy(ebc);
        }

        if(currentState != EquipmentEditState.None)
        {
            GameObject tips = TransformControlUtility.CreateItem("Edit/operateTips", UIUtility.GetRootCanvas());
            tips.name = "tips";
            operateTips = tips.GetComponent<OperateTips>();
        }

        if(operateTips!=null && currentState == EquipmentEditState.Edit)
        {
            operateTips.SetShowText("处于编辑器态");
        }else if(operateTips != null && currentState == EquipmentEditState.BulkCopy)
        {
            operateTips.SetShowText("shift水平垂直复制，滚轴+左ctrl调整距离");
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
        ebc.isCreate = true;
    }
}


