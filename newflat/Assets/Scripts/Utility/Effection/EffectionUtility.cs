using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 效果工具类
/// </summary>
public sealed  class EffectionUtility  {


    #region OutlineEffect
    /// <summary>
    /// 描边效果
    /// </summary>
    /// <param name="selectingObjectTransform"></param>
    public static void PlayOutlineEffect(Transform selectingObjectTransform, Color fromColor,Color toColor)
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
        //flash.play(Color.blue, Color.yellow);

        flash.play(fromColor, toColor);

    }

    /// <summary>
    /// 取消描边的效果
    /// </summary>
    /// <param name="selectingObjectTransform"></param>
    public static void StopOutlineEffect(Transform selectingObjectTransform)
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

    #endregion
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


    #region AphlaFlash
    public static void PlayAphlaFlash(GameObject gameObject)
    {
        AphlaFlashEffection afe = gameObject.GetComponent<AphlaFlashEffection>();
        if(!afe)
        {
            afe = gameObject.AddComponent<AphlaFlashEffection>();
        }
        afe.Flash();


    }

    public static void StopAphlaFlash(GameObject gameObject)
    {
        AphlaFlashEffection afe = gameObject.GetComponent<AphlaFlashEffection>();
        if (afe)
        {
            afe.StopAllTask();
        }
    }


    # endregion


    #region  dotwenAphlaFlash
    public static void PlayDotweenAphlaFlash(GameObject gameObject,Color color,string property)
    {
        DoTweenAphlaFlashEffection afe = gameObject.GetComponent<DoTweenAphlaFlashEffection>();
        if (!afe)
        {
            afe = gameObject.AddComponent<DoTweenAphlaFlashEffection>();
        }
        afe.Flash(color, property);


    }

    public static void StopDotweenAphlaFlash(GameObject gameObject)
    {
        DoTweenAphlaFlashEffection afe = gameObject.GetComponent<DoTweenAphlaFlashEffection>();
        if (afe)
        {
            afe.StopAllTask();
        }
    }


    # endregion
}
