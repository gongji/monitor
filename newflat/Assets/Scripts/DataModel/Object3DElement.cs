using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Object3DElement : MonoBehaviour {

    public string id;
    private void Awake()
    {
    }
    public DataModel.Type type;

 
    
    public EquipmentItem equipmentData = new EquipmentItem();

    public void SetEquipmentData(EquipmentItem _equipmentData)
    {
        this.equipmentData = _equipmentData;
    }

    private void Update()
    {
        if(type == Type.Equipment)
        {
            //equipmentData.Update(transform.localPosition, transform.localScale, transform.localEulerAngles);
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


    #region delete 
    private static List<string> delete3dObjects = new List<string>();

    public static void AddDeleteItem(string deleteItemId)
    {
        delete3dObjects.Add(deleteItemId);
    }
    public static List<string> GetDeleteList()
    {
        return delete3dObjects;
    }

    #endregion




}
