using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 效果工具类
/// </summary>
public sealed  class EffectionUtility  {
    

    /// <summary>
    /// 选中设备
    /// </summary>
    /// <param name="selectingObjectTransform"></param>
    public static  void playSelectingEffect(Transform selectingObjectTransform)
    {
        //this.selectingObjectTransform = selectingObjectTransform;
        HighlightingSystem.HighlightingRenderer hr = Camera.main.GetComponent<HighlightingSystem.HighlightingRenderer>();
        if (hr)
        {
            hr.enabled = true;
        }
        HighlightingSystem.HighlightingBlitter hb = Camera.main.GetComponent<HighlightingSystem.HighlightingBlitter>();
        if (hb)
        {
            hb.enabled = true;

        }

        HighlightingSystem.Highlighter hi = selectingObjectTransform.GetComponent<HighlightingSystem.Highlighter>();
        if (hi = null)
        {
            hi = selectingObjectTransform.gameObject.AddComponent<HighlightingSystem.Highlighter>();
        }
        FlashingController flash = selectingObjectTransform.GetComponent<FlashingController>();
        if (flash == null)
        {
            flash = selectingObjectTransform.gameObject.AddComponent<FlashingController>();
        }
        flash.play(Color.blue, Color.yellow);

    }

    /// <summary>
    /// 取消设备的效果
    /// </summary>
    /// <param name="selectingObjectTransform"></param>
    public static void StopFlashingEffect(Transform selectingObjectTransform)
    {
        if (selectingObjectTransform != null)
        {
            FlashingController flash = selectingObjectTransform.GetComponent<FlashingController>();
            if (flash != null)
            {

                flash.stop();
            }
        }



        HighlightingSystem.HighlightingRenderer hr = Camera.main.GetComponent<HighlightingSystem.HighlightingRenderer>();
        if (hr)
        {
            hr.enabled = false;
        }

       

    }
}
