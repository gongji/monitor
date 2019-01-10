using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmTestPointShow : MonoBehaviour {

    public string equipmentid = string.Empty;

    public void OnMouseDown()
    {
        if(!string.IsNullOrEmpty(equipmentid))
        {
            EquipmentAlarmProxy.GetAlarmPointTestList((result) => {
                List<EquipmentTestPoint> list = Utils.CollectionsConvert.ToObject<List<EquipmentTestPoint>>(result);

                string url = "Grid/TestPointCenter";
                int[] colums = new int[] { 100, 200, 197 };
                string[] titles = new string[] { "测点", "名称", "状态" };

                List<List<string>> resultData = new List<List<string>>();
                foreach(EquipmentTestPoint item in list)
                {
                    List<string> dataItem = new List<string>();
                    dataItem.Add(item.number);
                    dataItem.Add(item.name);
                    dataItem.Add(item.state);
                    resultData.Add(dataItem);
                }
              
                GridMsg.Instance.Show<NormalGrid>(url, 500, 290, "alarm information", "Title/close", colums, titles, resultData);
            }, equipmentid);
        }
        
    }
}
