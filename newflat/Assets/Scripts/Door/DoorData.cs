using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataModel;
using Utils;

public class DoorData : MonoBehaviour {

	void Start () {
        SearchDoorData();
        
    }


    private void SearchDoorData()
    {
        string sql = "sceneId = " + GetComponent<Object3DElement>().sceneId;
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("result", sql);
        Equipment3dProxy.SearchEquipmentData((result) =>
        {
            if (!string.IsNullOrEmpty(result))
            {
                List<EquipmentItem> list = CollectionsConvert.ToObject<List<EquipmentItem>>(result);
                SetDoorData(list);
            }
            

        }, dic);
    }

    private void SetDoorData(List<EquipmentItem> list)
    {
        foreach(EquipmentItem item in list)
        {
            Transform doorTransfrom = transform.Find(item.number);
            if(doorTransfrom!=null)
            {
                doorTransfrom.GetComponent<Object3DElement>().equipmentData = item;
                SetBrowserScript(doorTransfrom, item.id);
            }
        }
    }

    private void SetBrowserScript(Transform child,string equipmentid)
    {
        if (AppInfo.Platform == BRPlatform.Browser)
        {
            child.gameObject.AddComponent<DoorEquipmentControl>();
            Dictionary<string, GameObject> equipmentDic = EquipmentData.GetAllEquipmentData;
            equipmentDic.Add(equipmentid, child.gameObject);
        }
    }
}
