using FlyingText3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTextMsg : MonoBehaviour {


    public static FlyingTextMsg instance;
    void Start () {
        instance = this;
        LoadFontAsset();
    }

    public void LoadFontAsset()
    {
      
        string url = Application.streamingAssetsPath + "/Text/microsoftyaheigb";
        ResourceUtility.Instance.GetHttpAssetBundle(url, (result) => {
            TextAsset fontAsset = result.LoadAsset("microsoftyaheigb") as TextAsset;

            FontData fd = new FontData();
            fd.fontName = TTFFontInfo.GetFontName(fontAsset.bytes);
            fd.ttfFile = fontAsset;
            if (FlyingText.instance.m_fontData == null)
            {
                FlyingText.instance.m_fontData = new List<FontData>();
            }
            FlyingText.instance.m_fontData.Add(fd);
            GetComponent<FlyingText>().Initialize();

        });
    }
}
