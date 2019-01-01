using Core.Common.Logging;
using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorEquipmentControl : NormalEquipmentControl {

    private ILog log = LogManagers.GetLogger("DoorEquipmentControl");

    private void Awake()
    {
        Init("door");
    }

    private void OnDisable()
    {
        if(equipmentIconObject!=null)
        {
            equipmentIconObject.SetActive(false);
        }
        
    }

    private void OnEnable()
    {
        if(equipmentIconObject!=null)
        {
            equipmentIconObject.SetActive(true);
        }
       
    }
}
