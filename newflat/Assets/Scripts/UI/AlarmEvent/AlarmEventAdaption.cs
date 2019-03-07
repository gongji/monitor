using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AlarmEventAdaption : MonoBehaviour,IEventListener {

    public Transform grid;

    Vector2 size =  Vector2.zero;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClick);
       // transform.GetComponentInChildren<Text>().text = "实时报警数据";
    }

    private bool isExpand = false;
    public void ButtonClick()
    {
        float yValue = grid.GetComponent<RectTransform>().sizeDelta.y;
        if (isExpand)
        {
            transform.parent.GetComponent<RectTransform>().DOAnchorPosY(transform.parent.GetComponent<RectTransform>().anchoredPosition.y + yValue, 1.0f);
        }
        else
        {
            transform.parent.GetComponent<RectTransform>().DOAnchorPosY(transform.parent.GetComponent<RectTransform>().anchoredPosition.y - yValue, 1.0f);
        }

        isExpand = !isExpand;
    }
    void Update () {
        size = grid.GetComponent<RectTransform>().sizeDelta;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(grid.GetComponent<RectTransform>().anchoredPosition.x,
            grid.GetComponent<RectTransform>().anchoredPosition.y + size.y-3);

    }

    public bool HandleEvent(string eventName, IDictionary<string, object> dictionary)
    {

        return true;
    }
}
