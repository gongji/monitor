using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using animation;

public  class AlarmEventDetailShow: AlarmEventWindowBase
{

    private void Start()
    {
        
        foreach(Button buttton in GetComponentsInChildren<Button>())
        {
            buttton.onClick.AddListener(() =>
            {
                Hide();
            });
        }
    }

}
