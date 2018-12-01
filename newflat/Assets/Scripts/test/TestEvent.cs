using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.A))
        {
            Dictionary<string, string> sendDic = new Dictionary<string, string>();
            sendDic.Add("eventId", "122");
            sendDic.Add("commitTime", "2018-12-31 20:20:20");

            sendDic.Add("commitUser", "admin");
            sendDic.Add("repairName", "admin");


            string sendData = "S0-C0-E21-A0-EV|25150CE96DAC410FB3AFB1C6594F31|2018-12-31 20:20:20|admin|admin|25";

            //string json = Utils.CollectionsConvert.ToJSON(sendDic);
            WebsocjetService.Instance.SendData(sendData);
        }
    }
}
