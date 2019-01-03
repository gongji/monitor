using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BomSlider : MonoBehaviour {

    public void Change()
    {
        Debug.Log(GetComponent<Slider>().value);
    }
}
