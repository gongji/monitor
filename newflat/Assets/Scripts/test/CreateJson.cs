using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataModel;

public class CreateJson : MonoBehaviour {

	void Start () {
		List<Object3dItem> assetsList = new List<Object3dItem> ();

        //地形
        Object3dItem aassets = new Object3dItem();
       
        aassets.name = "地形";
        aassets.code = "main_dx";
        aassets.type = Type.Area;
        aassets.id = aassets.code;
       assetsList.Add(aassets);

        aassets = new Object3dItem();
        aassets.name = "效果";
        aassets.code = "skybox";
        aassets.id = aassets.code;
        aassets.type = Type.Area;
        assetsList.Add(aassets);

        Object3dItem buildeobject3dItem1 = new Object3dItem ();
		buildeobject3dItem1.name = "拘留所";
		buildeobject3dItem1.code = "juliusuo_wq";

        buildeobject3dItem1.id = buildeobject3dItem1.code;
        buildeobject3dItem1.type = Type.Builder;
        assetsList.Add (buildeobject3dItem1);

		if(buildeobject3dItem1.childs ==null)
		{
			buildeobject3dItem1.childs = new List<Object3dItem> ();
		}
			
		Object3dItem floor1 = new Object3dItem ();
		floor1.name = "拘留所一层";
		floor1.code = "juliusuo_sn_f1";
		floor1.type = Type.Floor;
        floor1.parentid = "juliusuo_wq";


        floor1.id = floor1.code;

        buildeobject3dItem1.childs.Add (floor1);


		if(floor1.childs ==null)
		{
			floor1.childs = new List<Object3dItem> ();
		}

		Object3dItem f1oo1room1= new Object3dItem ();
		f1oo1room1.name = "一层房间1";
		f1oo1room1.code = "juliusuo_sn_f1_fj1";
		f1oo1room1.type = Type.Room;
        f1oo1room1.id = floor1.code;
        f1oo1room1.parentid = "juliusuo_sn_f1";
        floor1.childs .Add (f1oo1room1);

		Object3dItem f1oo1room2= new Object3dItem ();
		f1oo1room2.name = "一层房间2";
		f1oo1room2.code = "juliusuo_sn_f1_fj2";
        f1oo1room2.parentid = "juliusuo_sn_f1";
        f1oo1room2.id = f1oo1room2.code;
        f1oo1room2.type = Type.Room;
        floor1.id = floor1.code;





        floor1.childs .Add (f1oo1room2);

	
		Object3dItem floor2 = new Object3dItem ();
		floor2.name = "拘留所二层";
		floor2.code = "c";
        floor2.id = floor2.code;
        floor2.type = Type.Floor;
        floor2.parentid = "juliusuo_wq";
        buildeobject3dItem1.childs.Add (floor2);


		if(floor2.childs ==null)
		{
			floor2.childs = new List<Object3dItem> ();
		}

		Object3dItem f1oo2room1= new Object3dItem ();
		f1oo2room1.name = "二层房间1";
		f1oo2room1.code = "juliusuo_sn_f2_fj1";
        f1oo2room1.id = f1oo2room1.code;
        f1oo2room1.parentid = "juliusuo_sn_f2";
        f1oo2room1.type = Type.Room;
		floor2.childs .Add (f1oo2room1);


		Object3dItem f1oo2room2= new Object3dItem ();
		f1oo2room2.name = "二层房间2";
		f1oo2room2.code = "juliusuo_sn_f2_fj2";
        f1oo2room2.id = f1oo2room2.code;
        f1oo2room2.parentid = "juliusuo_sn_f2";
        f1oo2room2.type = Type.Room;
		floor2.childs .Add (f1oo2room2);



        Object3dItem floor3 = new Object3dItem();
        floor3.name = "拘留所三层";
        floor3.code = "juliusuo_sn_f3";
        floor3.parentid = "juliusuo_wq";
        floor3.type = Type.Floor;
        floor3.id = floor3.code;

        buildeobject3dItem1.childs.Add(floor3);


        //===========================================================
        Object3dItem buildeobject3dItem2 = new Object3dItem ();
        buildeobject3dItem2.name = "办公楼";
		buildeobject3dItem2.code = "bangonglou_wq";
        buildeobject3dItem2.id = buildeobject3dItem2.code;
        buildeobject3dItem2.type = Type.Builder;
        assetsList.Add (buildeobject3dItem2);

        buildeobject3dItem2 = new Object3dItem();
        buildeobject3dItem2.name = "禁闭室";
        buildeobject3dItem2.code = "jds_wq";
        buildeobject3dItem2.id = buildeobject3dItem2.code;
        buildeobject3dItem2.type = Type.Builder;
        assetsList.Add(buildeobject3dItem2);




        //if(buildeobject3dItem2.childs ==null)
        //{
        //	buildeobject3dItem2.childs = new List<Object3dItem> ();
        //}

        //Object3dItem floor11 = new Object3dItem ();
        //floor11.name = "测试一层";
        //floor11.code = "test_sn_f1";
        //floor11.type = Object3dItem.Type.Floor;
        //buildeobject3dItem2.childs.Add (floor11);


        //if(floor11.childs ==null)
        //{
        //	floor11.childs = new List<Object3dItem> ();
        //}

        //Object3dItem f1oo1room11= new Object3dItem ();
        //f1oo1room11.name = "一层房间1";
        //f1oo1room11.code = "test_sn_f1_fj1";
        //f1oo1room11.type = Object3dItem.Type.Room;
        //floor11.childs .Add (f1oo1room11);


        //Object3dItem f1oo1room12= new Object3dItem ();
        //f1oo1room12.name = "一层房间2";
        //f1oo1room12.code = "test_sn_f1_fj2";
        //f1oo1room12.type = Object3dItem.Type.Room;
        //floor11.childs .Add (f1oo1room12);


        //Object3dItem floor22 = new Object3dItem ();
        //floor22.name = "测试二层";
        //floor22.code = "test_sn_f2";
        //floor22.type = Object3dItem.Type.Floor;
        //buildeobject3dItem2.childs.Add (floor22);


        //if(floor22.childs ==null)
        //{
        //	floor22.childs = new List<Object3dItem> ();
        //}

        //Object3dItem f1oo2room12= new Object3dItem ();
        //f1oo2room12.name = "二层房间1";
        //f1oo2room12.code = "test_sn_f2_fj1";
        //f1oo2room12.type = Object3dItem.Type.Room;
        //floor22.childs .Add (f1oo2room12);


        //Object3dItem f1oo2room22= new Object3dItem ();
        //f1oo2room22.name = "二层房间2";
        //f1oo2room22.code = "test_sn_f2_fj2";
        //f1oo2room22.type = Object3dItem.Type.Room;
        //floor22.childs .Add (f1oo2room22);
        FileUtils.WriteContent(Application.streamingAssetsPath + "/data.bat",FileUtils.WriteType.Write,Utils.CollectionsConvert.ToJSON(assetsList));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
