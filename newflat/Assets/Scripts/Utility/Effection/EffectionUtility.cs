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
    /// <summary>
    /// 模糊效果
    /// </summary>
    /// <param name="duringTime"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static IEnumerator BlurEffection(float duringTime,int from,int to)
    {
        float passedTime = 0;
        UnityStandardAssets.ImageEffects.Blur blur = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Blur>();
        if(blur==null)
        {
            yield break;
        }
        blur.iterations = from;
        blur.enabled = true;
        while ((passedTime += Time.deltaTime) < duringTime)
        {

            float result = Mathf.Lerp(from*1.0f, to * 1.0f, passedTime / duringTime);

            blur.iterations = (int)result;
            yield return 0;
        }
        blur.enabled = false;
    }
}
