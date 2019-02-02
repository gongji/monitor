using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 设备操作提示
/// </summary>
public class OperateTips : MonoBehaviour {

    public static OperateTips instance;
    private float yValue = 15.0f;
    void Start () {
        instance = this;
        gameObject.AddComponent<TextBgJust>();
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yValue - 100.0f);
       // MoveShow();

    }

    public void Show(string text)
    {
        GetComponentInChildren<Text>().text = text;
        transform.GetComponent<RectTransform>().DOAnchorPosY(yValue, 1.0f).OnComplete(() => {

           
        }); ;
    }
	

    public void HideTips()
    {
        transform.GetComponent<RectTransform>().DOAnchorPosY(yValue-100.0f, 1.0f).OnComplete(() => {
           
        }); ;

      
    }
}
