using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View2dTextManager : MonoSingleton<View2dTextManager>
{

    // Use this for initialization

    Transform parent = null;
    void Start()
    {
        GetParent();

    }

    private void GetParent()
    {
        if (parent == null)
        {
            parent = UIUtility.GetRootCanvas().Find("2dTips");
        }
       
    }

    private List<GameObject> textList = new List<GameObject>();
    private Dictionary<string, Bounds> dic = new Dictionary<string, Bounds>();
    public void Create2dText(Object3dItem object3dItem, Bounds bounds)
    {
        GetParent();

        GameObject text = GameObject.Instantiate(Resources.Load<GameObject>("Text"));
        ContentSizeFitter csf = text.GetComponent<ContentSizeFitter>();
        if(csf==null)
        {
            csf = text.AddComponent<ContentSizeFitter>();
        }
        csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        text.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        textList.Add(text);
        if(!dic.ContainsKey(object3dItem.id))
        {
            dic.Add(object3dItem.id, bounds);
        }
      
        
        text.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = object3dItem.name;
        text.GetComponentInChildren<TMPro.TextMeshProUGUI>().fontSize = 12;
        text.transform.SetParent(parent);
        text.name = object3dItem.id;
        Mouse2DTips mouse2DTips = text.AddComponent<Mouse2DTips>();
        mouse2DTips.sceneid = object3dItem.id;

    }

    public void Delete3dText()
    {
        foreach (GameObject tips in textList)
        {
            GameObject.Destroy(tips);
        }
        dic.Clear();
        textList.Clear();
    }

    private bool isVisible = true;


    public void ShowHide(bool isVisible)
    {
        foreach (GameObject tips in textList)
        {
            if(isVisible && TipsToggle.isOn)
            {
                tips.SetActive(true);
                if (isVisible && dic.ContainsKey(tips.name))
                {
                    tips.GetComponent<RectTransform>().anchoredPosition = UIUtility.WorldToUI(dic[tips.name].center, Camera.main);
                }
            }
            else
            {
                tips.SetActive(false);
            }
            
        }
    }
}
