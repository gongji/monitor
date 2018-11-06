using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using State;
using System.Collections.Generic;

public class testswitch : MonoBehaviour {

	// Use this for initialization
	public Transform area;
	public Transform buidler;
	public Transform floor;
	public Transform room;
	public Transform equipment;

	void Start () {
		//园区
		area.GetComponent<Button> ().onClick.AddListener (()=>{
			Main.instance.stateMachineManager.SwitchStatus<AreaState>(string.Empty);
           


		});

		//大楼
		buidler.GetComponent<Button> ().onClick.AddListener (()=>{

			Main.instance.stateMachineManager.SwitchStatus<BuilderState>("juliusuo_wq");
		});
		//楼层
		floor.GetComponent<Button> ().onClick.AddListener (()=>{

			Main.instance.stateMachineManager.SwitchStatus<FloorState>("juliusuo_sn_f1");
		});
		//房间
		room.GetComponent<Button> ().onClick.AddListener (()=>{

			Main.instance.stateMachineManager.SwitchStatus<RoomState>("juliusuo_sn_f1_fj1");
		});
		//设备
		equipment.GetComponent<Button> ().onClick.AddListener (()=>{

            //Main.instance.stateMachineManager.SwitchStatus<EquipmentState>("1234");

           // Main.instance.stateMachineManager.ViewEquipment("7a15bab254eb44ac8485febedee34311", "juliusuo_sn_f1_fj1");
        });
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
