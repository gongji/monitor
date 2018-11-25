using DataModel;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;

public class TestParent : MonoBehaviour {

    // Use this for initialization

    List<Object3dItem> list = new List<Object3dItem>();
	void Start () {

        //Object3dItem dx = new Object3dItem();
        //dx.id = "1";
        //dx.code = "main_dx";
        //list.Add(dx);


        //Object3dItem bangonglou = new Object3dItem();
        //bangonglou.id = "2";
        //bangonglou.code = "bangonglou_wq";
        //list.Add(bangonglou);


        //Object3dItem juliusuo = new Object3dItem();
        //juliusuo.id = "3";
        //juliusuo.code = "juliusuo_wq";
        //list.Add(juliusuo);


        //Object3dItem juliusuof1 = new Object3dItem();
        //juliusuof1.id = "4";
        //juliusuof1.code = "juliusuo_sn_f1";
        //list.Add(juliusuof1);

        //Object3dItem juliusuofguan = new Object3dItem();
        //juliusuofguan.id = "7";
        //juliusuofguan.code = "juliusuo_sn_guan_f1";
        //list.Add(juliusuofguan);

        //Object3dItem juliusuofj1 = new Object3dItem();
        //juliusuofj1.id = "5";
        //juliusuofj1.code = "juliusuo_sn_f1_fj1";
        //list.Add(juliusuofj1);


        //Object3dItem juliusuofj2 = new Object3dItem();
        //juliusuofj2.id = "6";
        //juliusuofj2.code = "juliusuo_sn_f1_fj2";
        //list.Add(juliusuofj2);


        //Object3dItem juliusuofj2door = new Object3dItem();
        //juliusuofj2door.id = "9";
        //juliusuofj2door.code = "juliusuo_sn_f1_door_fj2";
        //list.Add(juliusuofj2door);


        //Object3dItem juliusuofj2guandao = new Object3dItem();
        //juliusuofj2guandao.id = "10";
        //juliusuofj2guandao.code = "juliusuo_sn_f1_guanndao_fj2";
        //list.Add(juliusuofj2guandao);

        //SetRoomParent(list);

        //SetFloorParent(list);

        //foreach (Object3dItem temp in list)
        //{
        //    Debug.Log(temp.code + ":" + temp.parentid);
        //}

    }
	

    private  void   SetRoomParent(List<Object3dItem> object3dList)
    {
       
      
        Regex fjRegex = new Regex("fj\\d");
        IEnumerable<Object3dItem> roomList =
            from object3dItem in object3dList
            where fjRegex.IsMatch(object3dItem.number)
            select object3dItem;

        foreach (Object3dItem item in roomList)
        {
            string[] str = item.number.Split('_');
            string floorPrefix = str[0] + "_" + str[1] + "_" + str[2];

            IEnumerable<Object3dItem> floorList =
            from object3dItem in object3dList
            where object3dItem.number.ToString().Equals(floorPrefix)
            select object3dItem;

            if (floorList.Count() == 1)
            {
                item.parentsId = floorList.ToArray<Object3dItem>()[0].id;
            }
        }
    }

    private void SetFloorParent(List<Object3dItem> object3dList)
    {
        Regex  flooRegex = new Regex("f\\d");
        IEnumerable<Object3dItem> floorList =
            from object3dItem in object3dList
            where flooRegex.IsMatch(object3dItem.number) && !object3dItem.number.Contains(Constant.FJ.ToLower())
            select object3dItem;

        //Debug.Log(floorList.Count());
        foreach (Object3dItem item in floorList)
        {
            string[] str = item.number.Split('_');
            string floorPrefix = str[0] + "_wq";

            IEnumerable<Object3dItem> wqList =
            from object3dItem in object3dList
            where object3dItem.number.ToString().Equals(floorPrefix)
            select object3dItem;

            if (wqList.Count() == 1)
            {
                item.parentsId = wqList.ToArray<Object3dItem>()[0].id;
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
