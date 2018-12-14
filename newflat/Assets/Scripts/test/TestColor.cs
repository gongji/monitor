using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestColor : MonoBehaviour {

    // Use this for initialization

    private BaseEquipmentControl bec;
	void Start () {

        //Tweener tweener = GetComponent<MeshRenderer>().material.DOColor(Color.red, "_EmissionColor", 1.0f);
        //tweener.SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        bec = GetComponent<BaseEquipmentControl>();

    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.A))
        {
            bec.Alarm();
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            bec.CancleAlarm();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            bec.SelectEquipment();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            bec.CancelEquipment();
        }
	}
}
