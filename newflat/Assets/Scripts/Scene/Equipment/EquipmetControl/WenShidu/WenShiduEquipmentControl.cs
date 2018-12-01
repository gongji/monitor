using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WenShiduEquipmentControl : BaseEquipmentControl
{
    private void Start()
    {
        gameObject.AddComponent<WenShiduDataUpdate>();
        equipmentItem = GetComponent<Object3DElement>().equipmentData;

    }
    public override void Alarm(int state)
    {
        base.Alarm(state);
    }

    public override void CancleAlarm()
    {
        base.CancleAlarm();
    }


    public override void OnMouseClick()
    {
       
    }

    public override void SelectEquipment()
    {
        base.SelectEquipment();
    }

    public override void CancelEquipment()
    {
        base.CancelEquipment();
    }

    public override void ExeAnimation(string name, bool isExe)
    {
        Debug.Log(name);
        // throw new System.NotImplementedException();
    }
}
