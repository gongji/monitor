using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;

/// <summary>
/// TextMesh文字
/// </summary>
public class TextMeshModel : MeshTextAbstract
{
    void FixedUpdate()
    {
        if(AppInfo.currentView == ViewType.View3D)
        {
            LookAtCamera();
            SetTextScale();
            SetTextSelfAdaption();
        }
        
    }

    
    public Vector3 MinLoacalScale = Vector3.one;

    public Vector3 MaxLocalScale = Vector3.one;


    private GameObject fontModel = null;

    /// <summary>
    /// 生成的模型
    /// </summary>
    /// <param name="text"></param>
    /// <param name="postion"></param>
    /// <returns></returns>
    public override Transform  Create(string text,  Vector3 postion,Transform parentBox)
    {
        if(fontModel == null)
        {
            fontModel = GameObject.Instantiate(Resources.Load<GameObject>("Tips/tips"));
            fontModel.GetComponentInChildren<MeshRenderer>().enabled = false;
            // fontModel.transform.localScale = fontModel.transform.localScale / 6.0f;
            fontModel.transform.position = postion;
            fontModel.transform.SetParent(parentBox.parent);
          
            fontModel.name = text;
           // fontModel.transform.localScale = new Vector3(fontModel.transform.localScale.x *-1, fontModel.transform.localScale.y, fontModel.transform.localScale.z);
            fontModel.GetComponentInChildren<TMPro.TextMeshPro>().text = text;
           
            //MinLoacalScale = fontModel.transform.localScale;
          //  MaxLocalScale = MinLoacalScale * 5;

            AddScripts();
        }
        return fontModel.transform;




    }

  
    private void DestroyModel()
    {
        GameObject.Destroy(fontModel);
        
    }

    private MeshRenderer[] meshRenders;
    /// <summary>
    /// 始终面向相机
    /// </summary>
    private void LookAtCamera()
    {
        if (fontModel != null && fontModel.GetComponentInChildren<MeshRenderer>().enabled)
        {

            // Object3dUtils.LookAtCamera(fontModel.gameObject,false,true,false, Camera.main);
            Object3dUtility.LookAtCamera2(fontModel.gameObject, Camera.main);
        }
         
    }

  
    private void SetTextScale()
    {
        if (fontModel != null && fontModel.GetComponentInChildren<MeshRenderer>().enabled)
        {

            SetScale();
        }
    }

    private void  SetScale()
    {
        float distance = Vector3.Distance(fontModel.transform.position, Camera.main.transform.position);
        distance /= 1000.0f;
        distance = Mathf.Clamp01(distance);
        fontModel.transform.localScale =  Vector3.Lerp(MinLoacalScale, MaxLocalScale, distance);
    }

    public Vector3 size = Vector3.one;
    /// <summary>
    /// 设置文本的背景大小
    /// </summary>
    private void SetTextSelfAdaption()
    {
        
        if(mteBox==null && fontModel!=null && fontModel.activeSelf)
        {
            fontModel.transform.Find("tips").GetComponent<MeshRenderer>().enabled = true;
            mteBox = fontModel.transform.Find("tips").gameObject. AddComponent<BoxCollider>();
        }
        if(mteBox != null && fontModel!=null)
        {
            Vector3 size = mteBox.size;
            fontModel.transform.Find("tips/bg").localScale = new Vector3(size.x * 0.1f * 1.5f, size.y * 0.1f *1.5f, 0.6f);

        }       

    }


    private BoxCollider mteBox = null;
    private Transform bg;
    /// <summary>
    /// 挂载脚本，设置文本的背景大小
    /// </summary>
    private void AddScripts()
    {
        if (fontModel != null)
        {
            Transform tips = fontModel.transform.Find("tips");
            MouseTips mte = tips.gameObject.GetComponent<MouseTips>();
            if (mte == null)
            {
                mte = tips.gameObject.AddComponent<MouseTips>();
            }
            mte.id = id;
            mte.scaleEffetion = MinLoacalScale / 2.0f;
            //mteBox = tips.gameObject.GetComponent<BoxCollider>();
            //if (mteBox == null)
            //{
            //    mteBox = tips.gameObject.AddComponent<BoxCollider>();
            //}


            //
            //SetScale();
            // Object3dUtility.LookAtCamera2(fontModel.gameObject, Camera.main);
          //  fontModel.GetComponentInChildren<MeshRenderer>().enabled = true;
           
            //DOVirtual.DelayedCall(1.0f, () =>
            //{
            //    mteBox = tips.gameObject.GetComponent<BoxCollider>();
            //    if (mteBox == null)
            //    {
            //        mteBox = tips.gameObject.AddComponent<BoxCollider>();
            //    }
            //});

        }
    }


}
