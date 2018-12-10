using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class FlyTextMeshModel : MeshTextAbstract
{
    // Use this for initialization
    private GameObject fontModelGo;
    private Material fontMaterial;
    //是否缩放 
    public bool yRotate = true;
    public bool xRotate = true;
    public bool zRotate = true;

    private Vector3 defaultScale = Vector3.zero;
   
    void FixedUpdate()
    {
        if (AppInfo.currentView == ViewType.View3D)
        {
            LookAtCamera();
            SetTextScale();
            
        }
    }

    public override Transform Create(string text, Vector3 postion, Transform parentBox)
    {
        if (fontModel == null)
        {
            fontModel = FlyingText.GetObject(text);
            AddCollider(fontModel);
            fontModel.transform.position = postion;
            fontModel.transform.SetParent(parentBox.parent);
            fontModel.name = text;
           
            AddScripts();
        }
        return fontModel.transform;

    }


    protected override void AddScripts()
    {
        if (fontModel != null)
        {
            
            MouseTips mte = fontModel.gameObject.GetComponent<MouseTips>();
            if (mte == null)
            {
                mte = fontModel.gameObject.AddComponent<MouseTips>();
            }
            mte.id = id;
            mte.scaleEffetion = MinLoacalScale / 2.0f;


        }
    }


    private void DestroyModel()
    {
        GameObject.Destroy(fontModelGo);
        fontMaterial = null;
    }

    
    

    /// <summary>
    /// 获取三个相量的绝对值的最小值
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    private float getMixValue(Vector3 v)
    {
        float minValue = Mathf.Abs(v.x);

        if (Mathf.Abs(v.y) < minValue)
        {
            minValue = Mathf.Abs(v.y);
        }


        if (Mathf.Abs(v.z) < minValue)
        {
            minValue = Mathf.Abs(v.z);
        }
        return minValue;

    }

    private void AddCollider(GameObject go)
    {
        //Debug.LogError(go.name);

        BoxCollider box = fontModel.gameObject.GetComponent<BoxCollider>();
        if (box == null)
        {
            box = fontModel.gameObject.AddComponent<BoxCollider>();
        }

        Bounds bounds = new Bounds();
        bounds.size = Vector3.zero;
        bounds.center = Vector3.zero;
        MeshRenderer[] trans = go.transform.GetComponentsInChildren<MeshRenderer>();
      

        foreach (var item in trans)
        {
            //合并包围盒
            bounds.Encapsulate(item.bounds);
        }

   
        box.size = Vector3.zero;
        box.size = bounds.size;
    }



}
