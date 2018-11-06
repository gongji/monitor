using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskManager : MonoSingleton<MaskManager> {

    // Use this for initialization

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
