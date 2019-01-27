using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public  class LouShuiEquipmentControl : ChildEquipmentControl
{

    void Start()
    {
        if(GetComponent<Object3DElement>()!=null)
        {
            equipmentItem = GetComponent<Object3DElement>().equipmentData;
        }
    }
    public override void Alarm(int state=0)
    {
        base.Alarm(state);
    }

    public void LouShuiAlarm(int state, int segments)
    {
        Alarm(state);
        //SetSegmentsAlarm(segments);
    }

    public override void CancleAlarm()
    {
        base.CancleAlarm();
        ResetAll();
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
    /// 漏水绳的数据格式-10,0,35.7&0.4,0,35.7|0.4,0,35.7&8.5,0,35.7&8.500005,0,24.2|8.500005,0,24.2&8.500005,0,10.7
    /// </summary>
    /// <param name="data"></param>
    public void CreateLouShui(string data)
    {
        if(transform.childCount>0)
        {
            return;
        }
        this.data = data;
        if(loushuiLine==null)
        {
            loushuiLine = Resources.Load("equipment/loushui");
        }
       
        material = Resources.Load<Material>("equipment/linerender");

        originalColor = material.color;
        string[] postions = data.Split('|');

        for(int i=0;i< postions.Length;i++)
        {
            GameObject lineRender = (GameObject)GameObject.Instantiate(loushuiLine);
            lineRender.GetComponent<LineRenderer>().startWidth = 0.04f;
            lineRender.GetComponent<LineRenderer>().endWidth = 0.04f;
            string[] segments = postions[i].Split('&');

            List<Vector3> lineRenderpostions = new List<Vector3>();
            foreach(string str in segments)
            {
                string[] _postions = str.Split(',');
             
                Vector3  postion = new Vector3(float.Parse(_postions[0]), float.Parse(_postions[1]), float.Parse(_postions[2]));
                Vector3 worldPostion = transform.parent.TransformPoint(postion);
                worldPostion = worldPostion + Vector3.up * 0.05f;
                lineRenderpostions.Add(worldPostion);
            }
            lineRender.transform.SetParent(transform);
            lineRender.name = (i+1).ToString();
            lineRender.GetComponent<LineRenderer>().positionCount = lineRenderpostions.Count;
            lineRender.GetComponent<LineRenderer>().SetPositions(lineRenderpostions.ToArray());
            Material m = GameObject.Instantiate<Material>(material);
            lineRender.GetComponent<LineRenderer>().material = m;
            lineRender.GetComponent<LineRenderer>().useWorldSpace = true;
        }
    }

    private Tweener tweener = null;
    private int alarmIndex = -1;
   
    private void StopDotween()
    {
        if(tweener!=null)
        {
            tweener.Kill();
            tweener = null;
        }
    }

    private void ResetAll()
    {
        StopDotween();
        foreach (Transform child in transform)
        {
            child.GetComponent<LineRenderer>().material.color = originalColor;

        }
    }
}
