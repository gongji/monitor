using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;

/// <summary>
/// TextMesh tips
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


    public Vector3 size = Vector3.one;
    /// <summary>
    /// set bg
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
    protected override void AddScripts()
    {
        if (fontModel != null)
        {
            Transform tips = fontModel.transform.Find("tips");
            Mouse3DTips mte = tips.gameObject.GetComponent<Mouse3DTips>();
            if (mte == null)
            {
                mte = tips.gameObject.AddComponent<Mouse3DTips>();
            }
            mte.id = id;
            mte.scaleEffetion = MinLoacalScale / 2.0f;
           

        }
    }


}
