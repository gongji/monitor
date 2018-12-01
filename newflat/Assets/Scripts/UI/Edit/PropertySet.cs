using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropertySet : MonoBehaviour {


    public static PropertySet instance;
    public TMP_InputField equipmentName;
    public TMP_Dropdown parentObject;
    public TMP_InputField x;
    public TMP_InputField y;
    public TMP_InputField z;

    public TMP_InputField rotationX;
    public TMP_InputField rotationY;
    public TMP_InputField rotationZ;

    public TMP_InputField scaleX;

    public TMP_InputField scaleY;

    public TMP_InputField scaleZ;
    void Start () {
        instance = this;
    }

    private DataModel.EquipmentItem equipmentItem;
    public void UpdateData(DataModel.EquipmentItem equipmentItem)
    {
        FormatUtil.FormatEquipmentData(equipmentItem);
        this.equipmentItem = equipmentItem;
        equipmentName.text = equipmentItem.name;
        x.text = equipmentItem.x.ToString();
        y.text = equipmentItem.y.ToString();
        z.text = equipmentItem.z.ToString();

        rotationX.text = equipmentItem.rotationX.ToString();
        rotationY.text = equipmentItem.rotationY.ToString();
        rotationZ.text = equipmentItem.rotationZ.ToString();

        scaleX.text = equipmentItem.scaleX .ToString();

        scaleY.text = equipmentItem.scaleY.ToString();

        scaleZ.text = equipmentItem.scaleZ.ToString();
    }

    public void ChangeName()
    {
        equipmentItem.name = equipmentName.text;
    }

    public void ChangeParent()
    {

    }

    public  void ChangeTransform()
    {
        //Debug.Log("change");
        Transform select =  UIElementCommandBar.instance.selectingObjectTransform;
        if(select!=null)
        {
            select.localPosition = new Vector3(float.Parse(x.text), float.Parse(y.text), float.Parse(z.text));
            select.localEulerAngles = new Vector3(float.Parse(rotationX.text), float.Parse(rotationY.text), float.Parse(rotationZ.text));
            select.localScale = new Vector3(float.Parse(scaleX.text), float.Parse(scaleY.text), float.Parse(scaleZ.text));
        }
        
    }
}
