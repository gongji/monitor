using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DataModel;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

public class Test : MonoBehaviour {
    [SerializeField]
    public Transform t1 = null;
    void Start() {


        
        // Debug.Log(PlatformMsg.instance.currentPlatform);
        string names = "123";
        string names23 = "123";
        SoundUtilty.PlayServerSound(Application.streamingAssetsPath + "/Sound/alarm.mp3");
        //string[] nameArray = names.Split(',');
        //Debug.Log(nameArray.Length);
        //Debug.Log(nameArray[0]);



        //List<AA> lists = new List<AA>();

        //AA aa = new AA("11", "12");
        //lists.Add(aa);

        //aa = new AA("22", "23");
        //lists.Add(aa);

        //var list = from n in lists
        //select new BB
        //{
        //    BB1 = n.AA1,
        //    BB2 = n.AA2

        //};

        //foreach(var item in list)
        //{
        //    Debug.Log(item.BB1);
        //}


        //List<SceneAlarmItem> sai = new List<SceneAlarmItem>();
        //SceneAlarmItem temp1 = new SceneAlarmItem();
        //temp1.number = "room1";
        //temp1.id = "1";

        //sai.Add(temp1);

        //temp1 = new SceneAlarmItem();
        //temp1.number = "room2";
        //temp1.id = "2";

        //sai.Add(temp1);

        // Debug.Log(  Utils.CollectionsConvert.ToJSON(  BaseProxy.GetPostDataByObject(sai)));


        //string url = Application.streamingAssetsPath + "/R/alarmeffection";
        //Debug.Log(url);
        //ResourceUtility.Instance.GetHttpAssetBundle(url, (result) => {

        //   GameObject g =   (GameObject)GameObject.Instantiate(result.LoadAsset<GameObject>("alarmeffection"));
        //});
        //Debug.Log( Utils.StrUtil.GetNewGuid());
        // }
        // Debug.Log("base start");
        //EnergyConsumptionTestData.GetTestData();
        //Debug.Log(Camera.allCameras);
        //foreach(Camera c in Camera.allCameras)
        //{
        //    Debug.Log(c.name);

        //}


        //float f1 = 0.99999999999f;
        //float f2 = 1.100000000f;
        //Debug.Log(Mathf.Approximately(f1, f2));


        //EquipmentItem item1 = new EquipmentItem();
        //item1.name = "1223";
        //item1.x = 0.99999999999f;
        //item1.y = 7.111f;

        //EquipmentItem item2 = new EquipmentItem();
        //item2.name = "456";
        //item2.x = 1.000000000f;
        //item2.y = 7.112f;

        //System.Type type1 = item1.GetType();
        //FieldInfo[] fieldInfos1 = type1.GetFields();

        //System.Type type2 = item2.GetType();

        //foreach (var f in fieldInfos1)
        //{
        //    //字段名称
        //    string fieldName = f.Name;

        //    FieldInfo fieldInfo2 = type2.GetField(fieldName);
        //    //字段类型
        //    //string fieldType = f.FieldType.ToString() ;

        //    object fieldValue1 = f.GetValue(item1);

        //    object fieldValue2 = fieldInfo2.GetValue(item2);

        //    bool isSame = true;
        //    if (f.FieldType == typeof(System.Single))
        //    {
        //       isSame =  Mathf.Approximately((float)fieldValue1, (float)fieldValue2);
        //        Debug.Log("single:"+fieldName +":"+ isSame.ToString());
        //    }
        //    else
        //    {
        //        isSame = fieldValue1.Equals(fieldValue2);

        //        Debug.Log(fieldName + ":" + isSame.ToString());
        //    }




        //Debug.Log ("fieldName------>" + fieldName);
        //Debug.Log("fieldType------>" + fieldType);
        //Debug.Log("fieldValue------>" + fieldValue);

        //Debug.Log("-------------------------------------------------------------------");

        // }

        // CreateTreeData();

        //SubSystemProxy.GetSubSystemByScene((result) => {
        //    Debug.Log("result=" + result);

        //}, "242");

        //CreateTreeData();


    }




    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    gizmoScript gs = GameObject.FindObjectOfType<gizmoScript>();
        //    if(gs!=null)
        //    {
        //        gs.SetSelectObject(transform);
        //    }
        //}

        //if(Input.GetKeyDown(KeyCode.A))
        //{

        //}
    }


    private void CreateTreeData()
    {
    //    List<SubSystemItem> ss = new List<SubSystemItem>();

    //    for (int i = 0;i<5;i++)
    //    {
    //        SubSystemItem ssi = new SubSystemItem();
    //        ssi.name = "button1" + i;
    //        ssi.id = "id1" + i;
    //        ssi.type = "01";

    //        List<SubSystemItem> twoList = new List<SubSystemItem>();
    //        for (int k= 0;k < 8;k++)
    //        {
    //            SubSystemItem ssi2 = new SubSystemItem();
    //            ssi2.name = "button2" + i;
    //            ssi2.id = "id" + i;
    //            ssi2.type = "model";
    //            twoList.Add(ssi2);

    //            if(k%2==0)
    //            {
    //                List<SubSystemItem> threeList = new List<SubSystemItem>();

    //                for(int j=0;j<3;j++)
    //                {
    //                    SubSystemItem ssi3 = new SubSystemItem();
    //                    ssi3.name = "button3" + i;
    //                    ssi3.id = "id" + i;
    //                    ssi3.type = "equipment";
    //                    threeList.Add(ssi3);
    //                }
    //                ssi2.childs = threeList;

    //            }


    //        }
    //        ssi.childs = twoList;
    //        ss.Add(ssi);

    //    }

    //    GetComponent<TreeManager>().Init(ss);
    }


    private void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("点击ui上");
            return;
        }
        Debug.Log("OnMouseDown");
    }


    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
    }



}

public class AA
{
    public string AA1;

    public string AA2;

    public AA(string AA1,string AA2)
    {
        this.AA1 = AA1;
        this.AA2 = AA2;
    }
}

public class BB
{
    public string BB1;

    public string BB2;

    public BB(string BB1,string BB2)
    {
        this.BB1 = BB1;
        this.BB2 = BB2;
    }

    public BB()
    {

    }
}
