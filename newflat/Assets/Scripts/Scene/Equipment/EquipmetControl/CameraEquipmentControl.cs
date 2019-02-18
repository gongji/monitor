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

    public  override void ExeAnimation(string name, bool isExe)
    {
        
    }

    private void OnEnable()
    {
        if(equipmentIconObject)
        {
            equipmentIconObject.SetActive(true);
        }
        
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
        GameObject.FindObjectOfType<JSCall>()._OpenCamera(equipmentItem.id);
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
