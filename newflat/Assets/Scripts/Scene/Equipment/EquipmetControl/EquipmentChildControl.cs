using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentChildControl : BaseEquipmentControl
{
    protected Color originalColor = Color.white;


    public override void ExeAnimation(string name, bool isExe)
    {
        SetChildAlarm(name, isExe);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">index</param>
    /// <param name="IsExe">run</param>
    private void SetChildAlarm(string name, bool IsExe = false)
    {
        //StopDotween();
        //ResetAll();
        //tweener = transform.Find(i.ToString()).GetComponent<LineRenderer>().material.DOColor(Color.red, 1.0f);
        //tweener.SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        Transform target = transform.Find(name);
        if (target != null)
        {
            TweenColor tweenColor = UITweener.Begin<TweenColor>(target.gameObject, 0.5f);

            if (IsExe)
            {
                tweenColor.style = UITweener.Style.PingPong;
                tweenColor.from = originalColor;
                tweenColor.to = Color.red;
                tweenColor.Play(true);
            }
            else
            {
                tweenColor.style = UITweener.Style.Once;
                tweenColor.from = Color.red;
                tweenColor.to = originalColor;
                tweenColor.Play(true);
            }

        }


    }
}
