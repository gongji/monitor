using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WenShiduDataUpdate : MonoBehaviour {

    private GameObject ui;
	void Start () {

        Transform parent = UIUtility.GetRootCanvas().Find("wenshidu");
        if(parent!=null)
        {
            ui = TransformControlUtility.CreateItem("UI/Wenshidu/Temperature", parent);
        }
        ui.transform.localScale = Vector3.one * 0.25f;
        ui.GetComponent<UITemperature>().Show(gameObject, transform.name);
    }

    public void UpdateaData(float temperatureValue, float humidity)
    {
        ui.GetComponent<UITemperature>().SetData(temperatureValue, humidity);
    }

    private void OnEnable()
    {
        if(ui!=null)
        {
            ui.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (ui != null)
        {
            ui.SetActive(false);
        }
    }
}
