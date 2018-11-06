﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataModel;
/// <summary>
///// 三维字模型管理，使用前需要初始化
/// </summary>
public class TipsMgr : MonoSingleton<TipsMgr> {
    
    public void Start()
    {

       
    }

    /// <summary>
    /// 初始化管理类
    /// </summary>
    public void InitManager()
    {
       
    }

    private List<Transform> tipsList = new List<Transform>();
    public void CreateTips(GameObject collider, Object3dItem object3dItem,Transform parent,Vector3 minScale,Vector3 maxScale)
    {
        if (collider != null)
        {
            TextMeshModel tmm = collider.GetComponent<TextMeshModel>();
            if (tmm == null)
            {

                tmm = collider.AddComponent<TextMeshModel>();
            }
            tmm.id = object3dItem.id;
            BoxCollider bc = collider.GetComponent<BoxCollider>();
            if (bc == null)
            {
                bc = collider.gameObject.AddComponent<BoxCollider>();
            }
            bool isActive = bc.enabled;
            bc.enabled = true;
            Bounds boudns = bc.GetComponent<BoxCollider>().bounds;
            tmm.MinLoacalScale = minScale;
            tmm.MaxLocalScale = maxScale;
            //Debug.Log(boudns.center + Vector3.up * boudns.size.y);
           // Debug.Log(parent.name);
           Transform tips =   tmm.Create(object3dItem.name, boudns.center + Vector3.up * boudns.size.y, parent);
            tipsList.Add(tips);
            bc.enabled = isActive;
            //处理2d吧标签
            View2dTextManager.Instance.Create2dText(object3dItem, boudns);


        }
    }

    public void DeleteTips()
    {
        foreach(Transform tips in tipsList)
        {
            if(tips!=null)
            {
                GameObject.Destroy(tips.gameObject);
            }
            
        }
        tipsList.Clear();
        View2dTextManager.Instance.Delete3dText();
    }

    private bool isVisible = true;
    void Update()
    {
        if (AppInfo.currentView == ViewType.View3D)
        {
           
            isVisible = true;
            View2dTextManager.Instance.ShowHide(false);
        }
        else
        {
            isVisible = false;
            View2dTextManager.Instance.ShowHide(true);
        }
        foreach (Transform tips in tipsList)
        {
            if(isVisible && TipsToggle.isOn)
            {
                tips.gameObject.SetActive(true);
            }
            else
            {
                tips.gameObject.SetActive(false);
            }
            
        }
    }
    public void Show()
    {

    }

    public void Hide()
    {

    }
    
}