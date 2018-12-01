using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectionResouceLoader : MonoBehaviour {

    public static EffectionResouceLoader instance;
    public GameObject effection;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        string url = Application.streamingAssetsPath + "/R/" + PlatformMsg.instance.currentPlatform.ToString() + "/alarmeffection";

        ResourceUtility.Instance.GetHttpAssetBundle(url, (result) =>
        {
            effection = result.LoadAsset<GameObject>("alarmeffection");
          
            result.Unload(false);

        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
