using FlyingText3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTextMsg : MonoBehaviour
{ 
    public static FlyingTextMsg instance;
    void Awake()
    {
        instance = this;

        // LoadFontAsset();
       
    }
    private void Start()
    {
        LoadPrefeb();
    }

    public void LoadFontAsset()
    {
        string url = Application.streamingAssetsPath +  "/Text/"  + PlatformMsg.instance.currentPlatform.ToString() + "/microsoftyaheigb";
        ResourceUtility.Instance.GetHttpAssetBundle(url, (result) => {
           
            TextAsset fontAsset = result.LoadAsset("microsoftyaheigb") as TextAsset;

            FontData fd = new FontData();
            fd.fontName = TTFFontInfo.GetFontName(fontAsset.bytes);

            fd.ttfFile = fontAsset;
            if (FlyingText.instance.m_fontData == null)
            {

                FlyingText.instance.m_fontData = new List<FontData>();
            }

            FlyingText.instance.m_fontData[0] = fd;


            GetComponent<FlyingText>().Initialize();
            LoadMaterial();


        });
    }

    public void LoadMaterial()
    {
        string url = Application.streamingAssetsPath +   "/Text/" + PlatformMsg.instance.currentPlatform.ToString()+"/Material /text";
        ResourceUtility.Instance.GetHttpAssetBundle(url, (result) => {
            Material m = result.LoadAsset("text") as Material;
            GetComponent<FlyingText>().m_defaultMaterial = m;
            GetComponent<FlyingText>().Initialize();

        });
    }

    private void LoadPrefeb()
    {
       
        string url = Application.streamingAssetsPath +   "/Text/"  + PlatformMsg.instance.currentPlatform.ToString() + "/FlyingText.unity3d";
       // Debug.Log(url);
        ResourceUtility.Instance.GetHttpAssetBundle(url, (result) => {

            GameObject g =  (GameObject)GameObject.Instantiate(result.mainAsset);
            result.Unload(false);


        });
    }
}
