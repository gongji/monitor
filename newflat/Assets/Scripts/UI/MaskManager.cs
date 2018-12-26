using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///用来遮罩的画布
/// </summary>
public class MaskManager : MonoSingleton<MaskManager> {

  
    private GameObject mask;

    public void Show()
    {

        mask = TransformControlUtility.CreateItem("Mask", UIUtility.GetRootCanvas());
    }

    public void Hide()
    {
        if(mask!=null)
        {
            GameObject.Destroy(mask);
        }
    }
}
