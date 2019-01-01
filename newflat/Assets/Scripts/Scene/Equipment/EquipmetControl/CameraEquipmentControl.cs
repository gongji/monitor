using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEquipmentControl : BaseEquipmentControl
{

    private void Awake()
    {
        Init("camera");
    }

    private void OnDisable()
    {
        if(equipmentIconObject)
        {
            equipmentIconObject.SetActive(false);
        }
        
    }

    private void OnEnable()
    {
        if(equipmentIconObject)
        {
            equipmentIconObject.SetActive(true);
        }
        
    }
    public override void Alarm()
    {
        base.Alarm();
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
}
