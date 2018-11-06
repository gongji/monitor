using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Object3DElement : MonoBehaviour {

    public string id;
    private void Awake()
    {
       // id = transform.name;
    }
    public DataModel.Type type;


    public EquipmentItem equipmentData = new EquipmentItem();

    private void Update()
    {
        if(type == Type.Equipment)
        {
            equipmentData.Update(transform.localPosition, transform.localScale, transform.localEulerAngles);
        }
        
    }

    public void SelectHigh(bool isSelected)
    {
        if (isSelected)
        {
            EffectionUtility.playSelectingEffect(transform);
        }
        else
        {
            EffectionUtility.StopFlashingEffect(transform);
        }
    }




}
