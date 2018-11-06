using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EditUI {

    public static void Create()
    {
        CreateModelListUI();
        CreateEditTools();
        CreateEquipmentMenu();
    }



    /// <summary>
    /// 创建的模型列表的ui
    /// </summary>
    private static void CreateModelListUI()
    {
        GameObject modelList = TransformControlUtility.CreateItem("Edit/EdtiUI", UIUtility.GetRootCanvas());
        modelList.name = "EdtiUI";
        modelList.GetComponent<RectTransform>().anchoredPosition = new Vector2(-210.0f,-135f);
        //ShowModelList sml = modelList.GetComponent<ShowModelList>();
        //if (sml == null)
        //{
        //    sml = modelList.AddComponent<ShowModelList>();
        //}
    }

    /// <summary>
    /// 创建编辑器的工具栏
    /// </summary>
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



    /// <summary>
    /// 得到ui的父对象
    /// </summary>
    /// <returns></returns>
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
