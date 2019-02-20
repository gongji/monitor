using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestColor : MonoBehaviour {

    // Use this for initialization

    private BaseEquipmentControl bec;
    private Material m;
	void Start () {

        //Tweener tweener = GetComponent<MeshRenderer>().material.DOColor(Color.red, "_EmissionColor", 1.0f);
        //tweener.SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        //bec = GetComponent<BaseEquipmentControl>();
        m = Resources.Load<Material>("Material/color");
      GetComponent<MeshRenderer>().material = m;

    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.A))
        {
            // bec.Alarm();
            // WebSocketService.Instance.SendData("你好");
            GetComponent<MeshRenderer>().material = null;
            Resources.UnloadAsset(m);
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            //bec.CancleAlarm();
           // WebSocketService.Instance.StartSocket();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
           // bec.SelectEquipment();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
           // bec.CancelEquipment();
        }
	}
}
