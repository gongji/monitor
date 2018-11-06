﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 铺满全屏
/// </summary>
public class ImageBgFull : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int width = Screen.width;
        int height = Screen.height;
        int designWidth = 1920;//开发时分辨率宽
        int designHeight = 1080;//开发时分辨率高
        float s1 = (float)designWidth / (float)designHeight;
        float s2 = (float)width / (float)height;
        if (s1 < s2)
        {
            designWidth = (int)Mathf.FloorToInt(designHeight * s2);
        }
        else if (s1 > s2)
        {
            designHeight = (int)Mathf.FloorToInt(designWidth / s2);
        }
       // float contentScale = (float)designWidth / (float)width;
        RectTransform rectTransform = this.transform as RectTransform;
        if (rectTransform != null)
        {
            rectTransform.sizeDelta = new Vector2(designWidth, designHeight);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
