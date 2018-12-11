using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DataModel;
using System.Reflection;

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

   


}
