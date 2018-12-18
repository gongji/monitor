using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSelfAdaption : MonoBehaviour {

    // Use this for initialization


    private TMPro.TextMeshProUGUI text;
    void Start () {
        text = GetComponent<TMPro.TextMeshProUGUI>();

    }
	
	// Update is called once per frame
	void Update () {
        //GetComponent<RectTransform>().sizeDelta = new Vector2(text.);
	}
}
