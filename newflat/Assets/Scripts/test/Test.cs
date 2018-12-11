using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DataModel;
using System.Reflection;
using System.Collections.Generic;

public class Test : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    public Transform t1 = null;
	void Start () {

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

        CreateTreeData();




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
    }

    private void CreateTreeData()
    {
        List<SubSystemItem> ss = new List<SubSystemItem>();

        for (int i = 0;i<5;i++)
        {
            SubSystemItem ssi = new SubSystemItem();
            ssi.name = "button1" + i;
            ssi.id = "id1" + i;
            ssi.type = "0";

            List<SubSystemItem> twoList = new List<SubSystemItem>();
            for (int k= 0;k < 8;k++)
            {
                SubSystemItem ssi2 = new SubSystemItem();
                ssi2.name = "button2" + i;
                ssi2.id = "id" + i;
                ssi2.type = "0";
                twoList.Add(ssi2);

                if(k%2==0)
                {
                    List<SubSystemItem> threeList = new List<SubSystemItem>();

                    for(int j=0;j<3;j++)
                    {
                        SubSystemItem ssi3 = new SubSystemItem();
                        ssi3.name = "button3" + i;
                        ssi3.id = "id" + i;
                        ssi3.type = "0";
                        threeList.Add(ssi3);
                    }
                    ssi2.childs = threeList;

                }


            }
            ssi.childs = twoList;
            ss.Add(ssi);

        }

        GetComponent<TreeManager>().Init(ss);
    }

   


}
