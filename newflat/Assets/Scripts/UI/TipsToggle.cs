using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsToggle : MonoBehaviour {


    public static bool isOn = true;
    public void SelectChange()
    {
        isOn = GetComponent<Toggle>().isOn;
     //   Debug.Log(isOn);

    }
}
