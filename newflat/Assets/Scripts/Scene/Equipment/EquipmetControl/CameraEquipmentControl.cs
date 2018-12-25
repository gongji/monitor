using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEquipmentControl : BaseEquipmentControl
{

    private void Start()
    {
        Init("camera");
    }

    private void OnDisable()
    {
        equipmentIconObject.SetActive(false);
    }

    private void OnEnable()
    {
        equipmentIconObject.SetActive(true);
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
