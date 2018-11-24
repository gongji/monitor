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

        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yValue - 100.0f);
        MoveShow();

    }

    private void MoveShow()
    {
        transform.GetComponent<RectTransform>().DOAnchorPosY(yValue, 1.0f).OnComplete(() => {

           
        }); ;
    }
	


    public void SetShowText(string text)
    {
        GetComponentInChildren<Text>().text = text;
    }

    public void DestroryGameObject()
    {
        transform.GetComponent<RectTransform>().DOAnchorPosY(yValue-100.0f, 1.0f).OnComplete(() => {
            GameObject.Destroy(gameObject);
        }); ;

      
    }
}
