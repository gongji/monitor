using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeshTextAbstract : MonoBehaviour {

    public  string id { get; set; } 

    
    public abstract Transform Create(string text, Vector3 postion, Transform parentBox);


    protected abstract void AddScripts();

    private MeshRenderer[] meshRenders;

    public Vector3 MinLoacalScale = Vector3.one;

    public Vector3 MaxLocalScale = Vector3.one;

    protected GameObject fontModel = null;

    /// <summary>
    /// 始终面向相机
    /// </summary>
    protected void LookAtCamera()
    {
        if (fontModel != null && fontModel.GetComponentInChildren<MeshRenderer>().enabled)
        {

            // Object3dUtils.LookAtCamera(fontModel.gameObject,false,true,false, Camera.main);
            Object3dUtility.LookAtCamera2(fontModel.gameObject, CameraMsg.GetCurrentCamera());
        }

    }


    protected void SetTextScale()
    {
        if (fontModel != null && fontModel.GetComponentInChildren<MeshRenderer>().enabled)
        {

            SetScale();
        }
    }

    protected void SetScale()
    {
        if(Camera.main!=null)
        {
            float distance = Vector3.Distance(fontModel.transform.position, Camera.main.transform.position);
            distance /= 1000.0f;
            distance = Mathf.Clamp01(distance);
            fontModel.transform.localScale = Vector3.Lerp(MinLoacalScale, MaxLocalScale, distance);
        }
        
    }



}
