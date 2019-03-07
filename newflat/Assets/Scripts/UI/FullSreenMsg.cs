using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullSreenMsg : MonoSingleton<FullSreenMsg> {
    public void InitUI()
    {
        JSCall callJs = GameObject.FindObjectOfType<JSCall>();
        if(callJs!=null)
        {
            string url =  callJs._GetUrl();
            if(!url.Contains("#"))
            {
                EventMgr.Instance.SendEvent(EventName.HIDE_UI,null);
            }
        }
    }
}
