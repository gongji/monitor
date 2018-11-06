using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUIControl : MonoBehaviour {

  
    public Transform modelList;

    public Transform PropertySet;

    public static EquipmentUIControl instance;

    private void Awake()
    {
        instance = this;
    }
    void Start () {
        ShowModelList(true);
    }

    public void ShowModelList(bool isShow)
    {
        if(isShow)
        {
            modelList.localScale = Vector3.one;
            PropertySet.localScale = Vector3.zero;
        }
        else
        {
            modelList.localScale = Vector3.zero;
            PropertySet.localScale = Vector3.one;
        }
       
    }
}
