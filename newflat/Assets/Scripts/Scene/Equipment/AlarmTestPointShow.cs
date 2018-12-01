using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmTestPointShow : MonoBehaviour {

    public string equipmentid = string.Empty;

    public string equipmentName = string.Empty;

    public void OnMouseDown()
    {
        if(!string.IsNullOrEmpty(equipmentid))
        {
            EquipmentAlarmProxy.GetAlarmPointTestList((result) => {
                if(string.IsNullOrEmpty(result))
                {

                    Debug.Log(equipmentid  +":alarm testPoint data is null" );
                    return;
                }
                List<TestPointItem> list = Utils.CollectionsConvert.ToObject<List<TestPointItem>>(result);

              //  List<TestPointItem> _dataList = list[0].data;
                string url = "Grid/TestPointCenter";
                int[] colums = new int[] { 100, 250, 247 };
                string[] titles = new string[] { "测点", "名称", "状态" };

                List<List<string>> resultData = new List<List<string>>();
                foreach (TestPointItem item in list)
                {
                    List<string> dataItem = new List<string>();
                    dataItem.Add(item.number);
                    dataItem.Add(item.name);
                    dataItem.Add(item.state);
                    resultData.Add(dataItem);
                }


                GridMsg.Instance.Show<NormalGrid>(url, 600, 290, equipmentName + "报警详细信息", "Title/close", colums, titles, resultData);
            }, equipmentid);
        }
        
    }
}
