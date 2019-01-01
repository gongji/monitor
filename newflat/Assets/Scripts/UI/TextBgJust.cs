using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 背景随着文本大小适配
/// </summary>
public class TextBgJust : MonoBehaviour {
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
