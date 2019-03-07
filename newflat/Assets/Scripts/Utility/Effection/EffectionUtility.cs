using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public sealed  class EffectionUtility  {

    #region OutlineEffect
    
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
        if (hi == null)
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

    #endregion

    #region mulitMaterial

    public static void PlayMulitMaterialEffect(Transform selectingObjectTransform, Color toColor)
    {

        MultiMaterialTweenColor tweenColor = selectingObjectTransform.GetComponent<MultiMaterialTweenColor>();
        if (tweenColor == null)
        {
            tweenColor = selectingObjectTransform.gameObject.AddComponent<MultiMaterialTweenColor>();
        }
        tweenColor.to = toColor;
        tweenColor.style = UITweener.Style.PingPong;
        tweenColor.duration = 0.5f;
        tweenColor.Play(true);
    }
    public static void StopMulitMaterialEffect(Transform selectingObjectTransform)
    {
        MultiMaterialTweenColor tweenColor = selectingObjectTransform.GetComponent<MultiMaterialTweenColor>();
        if (tweenColor != null)
        {
            tweenColor.style = UITweener.Style.Once;
            tweenColor.duration = 0.1f;
            tweenColor.Play(false);
        }
           
    }
    #endregion


    #region singel Color no flash

    public static void PlaySinlgeMaterialEffect(Transform selectingObjectTransform, Color toColor)
    {
        toColor = new Color32(6,148,255,255);
        MouseSinlgeColorEffection mouseSinlgeColorEffection = selectingObjectTransform.GetComponent<MouseSinlgeColorEffection>();
        if (mouseSinlgeColorEffection == null)
        {
            mouseSinlgeColorEffection = selectingObjectTransform.gameObject.AddComponent<MouseSinlgeColorEffection>();
        }
        mouseSinlgeColorEffection.SetEffection(toColor, "_Color");


    }

    public static void StopSinlgeMaterialEffect(Transform selectingObjectTransform)
    {
        MouseSinlgeColorEffection mouseSinlgeColorEffection = selectingObjectTransform.GetComponent<MouseSinlgeColorEffection>();
        if (mouseSinlgeColorEffection != null)
        {
            mouseSinlgeColorEffection.ResetColor();
        }
    }
    #endregion
}
