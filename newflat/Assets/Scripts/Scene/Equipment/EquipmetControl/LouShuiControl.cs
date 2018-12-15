using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public  class LouShuiControl : BaseEquipmentControl {


    public override void Alarm()
    {
        base.Alarm();
    }

    public void Alarm(int segments)
    {
        Alarm();
    }

    public override void CancleAlarm()
    {
        base.CancleAlarm();
    }


    public override void OnMouseClick()
    {
       // base.OnMouseClick();
    }

    public override void SelectEquipment()
    {
        base.SelectEquipment();
    }

    public override void CancelEquipment()
    {
        base.CancelEquipment();
    }


    private Object loushuiLine = null;

    private Material material;

    private string data;

    private Color color;
    /// <summary>
    /// 漏水绳的数据格式25.5，,5,5,7.8|14.1.36.9.58.7
    /// </summary>
    /// <param name="data"></param>
    public void CreateLouShui(string data)
    {
        this.data = data;
        if(loushuiLine==null)
        {
            loushuiLine = Resources.Load("equipment/loushui");
        }

        material = Resources.Load<Material>("equipment/linerender");

        string[] postions = data.Split('|');

        for(int i=1;i< postions.Length;i++)
        {
            GameObject lineRender = (GameObject)GameObject.Instantiate(loushuiLine);
            lineRender.GetComponent<LineRenderer>().startWidth = 0.1f;
            lineRender.GetComponent<LineRenderer>().endWidth = 0.1f;
            string[] _postions = postions[i - 1].Split(',');
            lineRender.transform.SetParent(transform);
            lineRender.name = i.ToString();
            Vector3 preVerctor3 = new Vector3(float.Parse(_postions[0]), float.Parse(_postions[1]), float.Parse(_postions[2]));

            _postions = postions[i ].Split(',');

            Vector3 currentVerctor3 = new Vector3(float.Parse(_postions[0]), float.Parse(_postions[1]), float.Parse(_postions[2]));
            Vector3[] vs = new Vector3[2] { preVerctor3 , currentVerctor3 };
            lineRender.GetComponent<LineRenderer>().SetPositions(vs);
            Material m = GameObject.Instantiate<Material>(material);
            lineRender.GetComponent<LineRenderer>().material = m;
        }
    }

    private void SetSegmentsAlarm(int i)
    {

        Tweener tweener = transform.Find(i.ToString()).GetComponent<Material>().DOColor(Color.red, 1.0f);
        tweener.SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }





}
