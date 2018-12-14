using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class LouShui : BaseEquipmentControl {


    public override void Alarm()
    {
        base.Alarm();
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


    private GameObject loushuiLine = null;

    private string data;
    /// <summary>
    /// 漏水绳的数据格式25.5，,5,5,7.8|14.1.36.9.58.7
    /// </summary>
    /// <param name="data"></param>
    public void CreateLouShui(string data)
    {
        this.data = data;
        if(loushuiLine==null)
        {
            loushuiLine = GameObject.Instantiate(Resources.Load<GameObject>("loushui"));
        }

        string[] postions = data.Split('|');

        List<Vector3> ps = new List<Vector3>();
        foreach(string temp in postions)
        {
            string[] _postions = temp.Split('_');
            Vector3 p = new Vector3(float.Parse(_postions[0]), float.Parse(_postions[1]), float.Parse(_postions[2]));
            ps.Add(p);
        }
        loushuiLine.GetComponent<LineRenderer>().startWidth = 0.1f;
        loushuiLine.GetComponent<LineRenderer>().endWidth = 0.1f;
        loushuiLine.GetComponent<LineRenderer>().SetPositions(ps.ToArray());
    }





}
