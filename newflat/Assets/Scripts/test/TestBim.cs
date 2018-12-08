using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBim : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CreateData();

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public static string CreateData()
    {
        List<BimItem> property = new List<BimItem>();
        BimItem bimItem = new BimItem();
        bimItem.title = "构造";
        bimItem.name = "结构";
        bimItem.value = "钢筋水泥";
        property.Add(bimItem);


        bimItem = new BimItem();
        bimItem.title = "图形";
        bimItem.name = "粗略比例,粗略比例填充颜色";
        bimItem.value = "122,0";
        property.Add(bimItem);


        bimItem = new BimItem();
        bimItem.title = "构造";
        bimItem.name = "默认的厚度";
        bimItem.value = "20";

        property.Add(bimItem);


        bimItem = new BimItem();
        bimItem.title = "标识数据";
        bimItem.name = "类型图像未知";
        bimItem.value = "";

        property.Add(bimItem);



        List<BimItem> type = new List<BimItem>();
        BimItem bimItem1 = new BimItem();
        bimItem1.title = "构造1";
        bimItem1.name = "结构1";
        bimItem1.value = "钢筋水泥1";
        type.Add(bimItem1);


        bimItem1 = new BimItem();
        bimItem1.title = "图形1";
        bimItem1.name = "粗略比例,粗略比例填充颜色1";
        bimItem1.value = "100,01";
        type.Add(bimItem1);


        bimItem1 = new BimItem();
        bimItem1.title = "构造1";
        bimItem1.name = "默认的厚度1";
        bimItem1.value = "201";

        type.Add(bimItem1);

        bimItem1 = new BimItem();
        bimItem1.title = "标识数据1";
        bimItem1.name = "类型图像未知1";
        bimItem1.value = "";

        type.Add(bimItem1);
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("property", property);
        dic.Add("type", type);

        string json = Utils.CollectionsConvert.ToJSON(dic);
        Debug.Log(json);
        return json;


    }
}
