using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataModel;
using System.Linq;
using System;

public class CreateEquipmentJson : MonoBehaviour {

	void Start () {
		List<EquipmentItem>  equipmentList = new List<EquipmentItem> ();

        EquipmentItem item = new EquipmentItem();
        item.id = Guid.NewGuid().ToString("N");
        item.name = "机柜-1";
        item.parentid = "juliusuo_sn_f1_fj1";
        item.modelid = "jigui";
        item.postion = new Vector3(1.31f, -1.749996f, 0);
        item.eulerAngles = new Vector3(-90,0,90);
        equipmentList.Add(item);


        item = new EquipmentItem();
        item.id = Guid.NewGuid().ToString("N");
        item.name = "机柜-2";
        item.parentid = "juliusuo_sn_f1_fj1";
        item.modelid = "jigui";
        item.postion = new Vector3(-1.15f, -1.749996f, 0.0f);
        item.eulerAngles = new Vector3(-90, 0, 270);
        equipmentList.Add(item);

        item = new EquipmentItem();
        item.id = Guid.NewGuid().ToString("N");
        item.name = "机柜-3";
        item.parentid = "juliusuo_sn_f1_fj1";
        item.modelid = "jigui";
        item.postion = new Vector3(-1.15f, -1.75f, -0.63f);
        item.eulerAngles = new Vector3(-90, 0, 270);
        equipmentList.Add(item);


        item = new EquipmentItem();
        item.id = Guid.NewGuid().ToString("N");
        item.name = "机柜-3";
        item.parentid = "juliusuo_sn_f1_fj2";
        item.modelid = "jigui";
        item.postion = new Vector3(1.31f, -1.749996f, 0);
        item.eulerAngles = new Vector3(-90, 0, 90);
        equipmentList.Add(item);


        item = new EquipmentItem();
        item.id = Guid.NewGuid().ToString("N");
        item.name = "机柜-4";
        item.parentid = "juliusuo_sn_f1_fj2";
        item.modelid = "jigui";
        item.postion = new Vector3(-1.15f, -1.749996f, 0.0f);
        item.eulerAngles = new Vector3(-90, 0, 270);
        equipmentList.Add(item);

        item = new EquipmentItem();
        item.id = Guid.NewGuid().ToString("N");
        item.name = "机柜-5";
        item.parentid = "juliusuo_sn_f1_fj2";
        item.modelid = "jigui";
        item.postion = new Vector3(-1.15f, -1.75f, -0.63f);
        item.eulerAngles = new Vector3(-90, 0, 270);
        equipmentList.Add(item);




        //item = new EquipmentItem();
        //item.id = Guid.NewGuid().ToString("N");
        //item.name = "机柜-4";
        //item.parentid = "juliusuo_sn_f1_fj1";
        //item.modelid = "jigui";
        //item.postion = new Vector3(0.3196545f, -0.5109478f, 0.01509366f);
        //item.eulerAngles = new Vector3(-90, 0, 90);
        //equipmentList.Add(item);



        //item = new EquipmentItem();
        //item.id = Guid.NewGuid().ToString("N");
        //item.name = "机柜-5";
        //item.parentid = "juliusuo_sn_f1_fj1";
        //item.modelid = "jigui";
        //item.postion = new Vector3(-0.2781858f, -0.5109478f, 0.18201195f);
        //item.eulerAngles = new Vector3(-90, -90, 90);
        //equipmentList.Add(item);



        //item = new EquipmentItem();
        //item.id = Guid.NewGuid().ToString("N");
        //item.name = "机柜-6";
        //item.parentid = "juliusuo_sn_f1_fj1";
        //item.modelid = "jigui";
        //item.postion = new Vector3(-0.2781858f, -0.5109478f, 0.1278522f);
        //item.eulerAngles = new Vector3(-90, -90, 90);

        //equipmentList.Add(item);


        //item = new EquipmentItem();
        //item.id = Guid.NewGuid().ToString("N");
        //item.name = "机柜-7";
        //item.parentid = "juliusuo_sn_f1_fj1";
        //item.modelid = "jigui";
        //item.postion = new Vector3(-0.2781858f, -0.5109478f, 0.07014117f);
        //item.eulerAngles = new Vector3(-90, -90, 90);
        //equipmentList.Add(item);



        //item = new EquipmentItem();
        //item.id = Guid.NewGuid().ToString("N");
        //item.name = "机柜-8";
        //item.parentid = "juliusuo_sn_f1_fj1";
        //item.modelid = "jigui";
        //item.postion = new Vector3(-0.2781858f, -0.5109478f, 0.01509366f);
        //item.eulerAngles = new Vector3(-90, -90, 90);
        //equipmentList.Add(item);



        //item = new EquipmentItem();
        //item.id = Guid.NewGuid().ToString("N");
        //item.name = "机柜-9";
        //item.parentid = "juliusuo_sn_f1_fj2";
        //item.modelid = "jigui";
        //item.postion = new Vector3(-0.2781858f, -0.5109478f, 0.07014117f);
        //item.eulerAngles = new Vector3(-90, -90, 90);
        //equipmentList.Add(item);



        //item = new EquipmentItem();
        //item.id = Guid.NewGuid().ToString("N");
        //item.name = "机柜-10";
        //item.parentid = "juliusuo_sn_f1_fj2";
        //item.modelid = "jigui";
        //item.postion = new Vector3(-0.2781858f, -0.5109478f, 0.01509366f);
        //item.eulerAngles = new Vector3(-90, -90, 90);
        //equipmentList.Add(item);




        item = new EquipmentItem();
        item.id = Guid.NewGuid().ToString("N");

        item.name = "园区";
        item.parentid = "";
        item.modelid = "jigui";
        item.postion = new Vector3(-1.27f, 0.473f, 0.275f);
        item.eulerAngles = new Vector3(-90f, -180, 0f);
        equipmentList.Add(item);


       

        item = new EquipmentItem();
        item.id = Guid.NewGuid().ToString("N");
        item.name = "f1楼层机柜";
        item.parentid = "juliusuo_sn_f1";
        item.modelid = "jigui";
        item.postion = Vector3.one;
        item.eulerAngles = new Vector3(-90, -90, 0);
        equipmentList.Add(item);


        item = new EquipmentItem();
        item.id = Guid.NewGuid().ToString("N");
        item.name = "f2层机柜";
        item.parentid = "juliusuo_sn_f2";
        item.modelid = "jigui";
        item.postion = Vector3.one;
        item.eulerAngles = new Vector3(-90, -90, 0);
        equipmentList.Add(item);

        var list = equipmentList.GroupBy(x => new { x.modelid }).Select(group => new
        {
            group.Key.modelid
        }).ToList();
        
        foreach (var a in list)
        {
            Debug.Log(a.modelid);
        }


        FileUtils.WriteContent(Application.streamingAssetsPath + "/equipment.bat",FileUtils.WriteType.Write,Utils.CollectionsConvert.ToJSON(equipmentList));

	}
	
	// Update is called once per frame
	void Update () {
	    

        
	}
}
