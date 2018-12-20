/***********************************
    **  日期:
    **  姓名:jss
    **  审阅:jss
    **  功能:
    **  备注:
************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITemperature : MonoBehaviour
{
    public struct Items
    {
        public Text[] tex;
        public Text num;
    }

    public Items LeftItem;
    public Items RightItem;
    public GameObject LeftGo;
    public GameObject RightGo;
    public GameObject target;
    private Canvas canvas;
   
    // Use this for initialization
    void Awake()
    {
        Init();
        canvas = UIUtility.GetRootCanvas().GetComponent<Canvas>();
    }

    private void Init()
    {
        LeftItem.tex = LeftGo.transform.Find("tex").GetComponentsInChildren<Text>();
        LeftItem.num = LeftGo.transform.Find("num").GetComponent<Text>();

        RightItem.tex = RightGo.transform.Find("tex").GetComponentsInChildren<Text>();
        RightItem.num = RightGo.transform.Find("num").GetComponent<Text>();

        RightGo.SetActive(false);
        LeftGo.SetActive(false);
    }

    /// <summary>
    /// 设置简拼和设备对象
    /// </summary>
    /// <param name="target"></param>
    /// <param name="SimpleName"></param>
    public void Show(GameObject target, string SimpleName)
    {
        this.target = target;
        Camera cam = Camera.main;
        Vector3 v = cam.WorldToViewportPoint(target.transform.position);
        bool isLeft = v.x > 0.5f ? false : true;
        if (isLeft)
        {
            RightGo.SetActive(false);
            LeftGo.SetActive(true);
        }
        else
        {
            RightGo.SetActive(true);
            LeftGo.SetActive(false);
        }
        LeftItem.num.text = SimpleName;
        RightItem.num.text = SimpleName;
    }
    // Update is called once per frame
    void Update()
    {
        if (this.target != null)
        {
            Camera cam = Camera.main;
            Vector2 screenPoint = cam.WorldToScreenPoint(target.transform.position);
            Vector2 localPoint;
            Vector3 v = cam.WorldToViewportPoint(target.transform.position);

            bool isLeft = v.x > 0.5f ? false : true;
            if (isLeft)
            {
                RightGo.SetActive(false);
                LeftGo.SetActive(true);
            }
            else
            {
                RightGo.SetActive(true);
                LeftGo.SetActive(false);
            }
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(),
                screenPoint, canvas.worldCamera, out localPoint);
            this.transform.localPosition = localPoint;
        }
    }

    public void SetData(float temperatureValue,float humidity)
    {
        LeftItem.tex[1].text = temperatureValue.ToString();
        RightItem.tex[1].text = temperatureValue.ToString();
        LeftItem.tex[4].text = humidity.ToString();
        RightItem.tex[4].text = humidity.ToString();
    }
}


