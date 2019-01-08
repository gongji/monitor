using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMsg : MonoBehaviour {

    public static PlatformMsg instance;

    public UnityPlatform currentPlatform = UnityPlatform.PC;
    private void Awake()
    {
        instance = this;
#if UNITY_ANDROID
        currentPlatform =  UnityPlatform.ANDROID;
#elif UNITY_STANDALONE_WIN

        currentPlatform = UnityPlatform.PC;

#elif UNITY_WEBGL
         currentPlatform = UnityPlatform.WEB;

#elif UNITY_IOS

        currentPlatform = UnityPlatform.IOS;
#endif
    }


}

public enum UnityPlatform
{
    PC,
    WEB,
    IOS,
    ANDROID

}
