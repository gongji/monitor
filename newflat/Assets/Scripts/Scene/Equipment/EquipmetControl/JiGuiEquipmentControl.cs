using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiGuiEquipmentControl : NormalEquipmentControl {

    private bool isLoad = false;

    public bool IsLoad
    {
        set
        {
            isLoad = value;
        }
    }

    private float distance = 3.0f;
    private void Update()
    {
        if(Vector3.Distance(transform.position,Camera.main.transform.position)> distance && !isLoad)
        {
            isLoad = true;

            LoadChildEquipment loadChildEquipment = gameObject.AddComponent<LoadChildEquipment>();
            loadChildEquipment.StartLoad(equipmentItem.id,()=> {

            });
        }
    }
}
