using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTime : MonoBehaviour {



    private void Start()
    {
        StartCoroutine(ShowCurrentTime());
    }
    private string user = "，管理员。";
    private int hour = 0;
    private string result = string.Empty;
    private IEnumerator ShowCurrentTime()
    {
        while(true)
        {
            hour = DateTime.Now.Hour;
            if(hour>=8 && hour<12)
            {
                result = "上午好"+ user;
            }
            else if(hour >= 12 && hour < 14)
            {
                result = "中午好"+ user;
            }
            else if (hour >= 14 && hour < 18)
            {
                result = "下午好"+ user;
            }
            else if (hour >= 18 && hour < 23)
            {
                result = "晚上好"+ user;
            }
            else
            {
                result = "夜深人静" + user;
            }
            GetComponent<TextMeshProUGUI>().text = result;
            yield return 0;
        }
    }
}
