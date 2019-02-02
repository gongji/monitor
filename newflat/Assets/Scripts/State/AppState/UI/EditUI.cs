using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EditUI {

    public static void Create()
    {
        CreateModelListUI();
        CreateEditTools();
        CreateEquipmentMenu();
        GameObject tips = TransformControlUtility.CreateItem("Edit/operateTips", UIUtility.GetRootCanvas());
        tips.name = "tips";
    }

    private static void CreateModelListUI()
    {
        GameObject modelList = TransformControlUtility.CreateItem("Edit/EditUI", UIUtility.GetRootCanvas());
        modelList.name = "EditUI";
        modelList.GetComponent<RectTransform>().anchoredPosition = new Vector2(-210.0f,-135f);
        //ShowModelList sml = modelList.GetComponent<ShowModelList>();
        //if (sml == null)
        //{
        //    sml = modelList.AddComponent<ShowModelList>();
        //}
    }


    private static void CreateEditTools()
    {
        GameObject toolBar = TransformControlUtility.CreateItem("Edit/toolBar", UIUtility.GetRootCanvas());
        toolBar.name = "toolBar";
        toolBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-95f);
    }

    private static void CreateEquipmentMenu()
    {
        GameObject toolBar = TransformControlUtility.CreateItem("Edit/EquipmentMenu", GetUIParent());
        toolBar.name = "EquipmentMenu";
        toolBar.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

   
    private static Transform GetUIParent()
    {
        Transform canvas = UIUtility.GetRootCanvas();
        if(canvas!=null)
        {
             return canvas.Find("Edit");
        }
        return null;
    }
}
