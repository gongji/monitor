using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TestEquipment : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //string sql = "parentsId in(107,106) or parentsId is null";
        //Dictionary<string, string> dic = new Dictionary<string, string>();
        //dic.Add("result", sql);

        //Equipment3dProxy.SearchEquipmentData((result) => {


        //    Debug.Log(result);
        //}, dic);
        CompareObject();


    }

    // Update is called once per frame

    private Dictionary<string, object> result = new Dictionary<string, object>();
	void Update () {

      // Equipment3dProxy.GetEquipment3dObjectDataByParentId

    }

    private void Save()
    {
        //List<EquipmentItem> addList = new List<EquipmentItem>();


        //for (int i = 0; i < 1; i++)
        //{

        //    EquipmentItem item = new EquipmentItem();

        //    item.name = "addnumber" + i;
        //    item.modelId = "23";
        //    item.sceneId = "";
        //    item.number = "number123";
        //    item.type = "door";
        //    addList.Add(item);
        //}




        //result.Add("add", addList);

        //List<EquipmentItem> updateList = new List<EquipmentItem>();



        //for (int i = 0; i < 5; i++)
        //{
        //    EquipmentItem item = new EquipmentItem();
        //    item.id = (12 + i).ToString();
        //    item.name = "modify" + i;
        //    item.modelId = "23";
        //    item.sceneId = "144";
        //    updateList.Add(item);


        //}


        //result.Add("update", updateList);
        //result.Add("delete", "");

        //Dictionary<string, string> postData = new Dictionary<string, string>();
        //postData.Add("result", CollectionsConvert.ToJSON(result));

        //Debug.Log(CollectionsConvert.ToJSON(postData));

        //Equipment3dProxy.PostEquipmentSaveData((a) => { }, postData);
    }

    private void CompareObject()
    {
        //EquipmentItem ei = new EquipmentItem();
        //ei.name = "54333";

        //ei.id = "123456";

        //EquipmentItem  aa = ei.Clone() as EquipmentItem;
        //Debug.Log(aa.name);
        //Debug.Log(aa.id);

        //if(aa.Equals(ei))
        //{
        //    Debug.Log("相等");
        //}
        //else
        //{
        //    Debug.Log("不相等");
        //}

    }
}
