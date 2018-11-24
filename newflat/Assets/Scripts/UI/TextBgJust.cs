using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBgJust : MonoBehaviour {

    // Use this for initialization

    private RectTransform recTransform;
    private Text text;
    void Start () {
        recTransform = GetComponent<RectTransform>();
        text = GetComponentInChildren<Text>();

    }
	
	// Update is called once per frame
	void Update () {

        recTransform.sizeDelta = new Vector2(text.preferredWidth+50, recTransform.sizeDelta.y); 
	}
}
