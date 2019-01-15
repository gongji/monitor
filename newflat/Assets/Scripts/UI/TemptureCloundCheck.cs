using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemptureCloundCheck : MonoBehaviour {

	public void CheckChange(){
		 if(GetComponent<Toggle>().isOn)
		 {
			 CloundMsg.ShowClound();
		 }
		 else{

			 CloundMsg.RemoveClound();
		 }
	}

	private void OnDisable()
	{
		GetComponent<Toggle>().isOn = false;
		CloundMsg.RemoveClound();
	}
}
