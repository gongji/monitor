using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            ShowControlTest();
        }

        //if(Input.GetKeyDown(KeyCode.B))
        //{
        //    SetControlPoint.GetSelectData();
        //}
      
    }

    private void ShowControlTest()
    {

        List<EquipmentControlPointItem> ecps = new List<EquipmentControlPointItem>();
       
       EquipmentControlPointItem ecp = new EquipmentControlPointItem();
       ecp.name = "name1";
       ecp.number = "S0-E1-S1";
       ecp.unnumber = "S0-E1-S1-R1";
       ecp.type = 0;
       ecp.unit = "单位1";
       ecps.Add(ecp);


        ecp = new EquipmentControlPointItem();
        ecp.name = "name2";
        ecp.number = "S0-E1-S2";
        ecp.unnumber = "S0-E1-S2-R2";
        ecp.type = 1;
        ecp.unit = "单位2";
        ecp.values = "value1,value2,value3";
        ecp.describes = "值一,值二,值三";

        ecps.Add(ecp);

        ecp = new EquipmentControlPointItem();
        ecp.name = "name3";
        ecp.number = "S0-E1-S3";
        ecp.unnumber = "S0-E1-S2-R3";
        ecp.type = 2;
        ecp.unit = "单位3";
        ecp.StartValue = 10;
        ecp.endValue = 20.4f;

        ecps.Add(ecp);

        SetControlPoint.Instance.SetDataSouce(ecps, "设备1");
    }
}
