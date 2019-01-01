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

        if(Input.GetKeyDown(KeyCode.B))
        {
             
            AlarmEventItem aei = new AlarmEventItem();
            aei.id = "9";
            aei.eventId = "344";
            aei.sceneId = "231";
            aei.content="发生了报警";
            aei.dateTime = "2019-01-01 07:11:11";
            aei.level = "0";
            aei.eventId = "123456789";
            aei.pointName = "A1";
            aei.sceneName = "sceneName";
            aei.channelName = "通道名称";
            aei.type  = "类型";
            aei.eventSource = "eventSource";
            aei.key = "key";
            aei.name = "设备的名字";
            aei.stationName = "二楼检测室";

            ShowAlarmEvent.instance.Show(aei);

        }

        if (Input.GetKeyDown(KeyCode.C))
        {

            AlarmEventItem aei = new AlarmEventItem();
            aei.id = "18";
            aei.sceneId = "230";
            aei.eventId = "344_1";
            aei.content = "发生了报警_1";
            aei.dateTime = "2019-01-01 08:11:11";
            aei.level = "10";
            aei.eventId = "123456789_1";
            aei.pointName = "A2";
            aei.sceneName = "sceneName_1";
            aei.channelName = "通道名称_1";
            aei.type = "类型";
            aei.eventSource = "eventSource_1";
            aei.key = "key_1";
            aei.name = "设备的名字_1";
            aei.stationName = "二楼检测室_1";

            ShowAlarmEvent.instance.Show(aei);

        }
    }
}
