using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestDownLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StartCoroutine(Excute());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public  IEnumerator Excute()
    {
        


        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle("http://127.0.0.1:8080/data/equipment/jigui"))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                
            }
            else
            {

                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
               
                 GameObject.Instantiate( bundle.LoadAsset<GameObject>("jigui"));
                



            }
        }

    }
}
