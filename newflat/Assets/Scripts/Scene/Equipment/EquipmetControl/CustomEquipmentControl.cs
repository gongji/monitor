﻿using Core.Common.Logging;
using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomEquipmentControl : NormalEquipmentControl {

    private ILog log = LogManagers.GetLogger("CustomEquipmentControl");

    private void Awake()
    {
        //Init("door");
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

    public override void ExeAnimation(string name, bool isExe)
    { 
        DoorControl[] doors = GetComponentsInChildren<DoorControl>();
        foreach(DoorControl dc in doors)
        {
            dc.GetType().GetMethod(name).Invoke(dc,null);
        }
    }
}
