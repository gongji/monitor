using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BimDataShow : MonoBehaviour {

    public Transform bimTitlePrefeb;
    public Transform bimContentPrefeb;

    public Transform PropertyContent;
    public Transform TypeContent;

    public void  Show(List<BimItem> propertyList, List<BimItem> typeList)
    {
        //property
        foreach(BimItem bimItem in propertyList)
        {
            GameObject titleGameObject = GameObject.Instantiate<GameObject>(bimTitlePrefeb.gameObject);
            titleGameObject.SetActive(true);
            titleGameObject.transform.SetParent(PropertyContent);
            titleGameObject.transform.localScale = Vector3.one;
            titleGameObject.GetComponentInChildren<Text>().text = "  "+bimItem.title;

            string names = bimItem.name;
            string values = bimItem.value;

            if(!string.IsNullOrEmpty(names))
            {
                GameObject content = GameObject.Instantiate<GameObject>(bimContentPrefeb.gameObject);
               
                content.SetActive(true);
                content.transform.SetParent(PropertyContent);
                content.transform.localScale = Vector3.one;

                string[] nameArray = names.Split(',');
                string[] valueArray = values.Split(',');
                content.GetComponentInChildren<Text>() .text = "";
                content.GetComponentInChildren<Text>().lineSpacing = 1.2f;
                for (int i=0;i< nameArray.Length;i++)
                {
                    content.GetComponentInChildren<Text>().text += "\u3000" + nameArray[i] + "<color=#ffffff>" + GetSpaceLength(nameArray[i]) + "</color>" + valueArray[i] + "\n";
                }
                
            }
        }

        //type
        foreach (BimItem bimItem in typeList)
        {
            GameObject titleGameObject = GameObject.Instantiate<GameObject>(bimTitlePrefeb.gameObject);
            titleGameObject.SetActive(true);
            titleGameObject.transform.SetParent(TypeContent);
            titleGameObject.GetComponentInChildren<Text>().text = "  " + bimItem.title;

            string names = bimItem.name;
            string values = bimItem.value;

            if (!string.IsNullOrEmpty(names))
            {
                GameObject content = GameObject.Instantiate<GameObject>(bimContentPrefeb.gameObject);
                content.SetActive(true);
                content.transform.SetParent(TypeContent);

                string[] nameArray = names.Split(',');
                string[] valueArray = values.Split(',');
                content.GetComponentInChildren<Text>().text = "";
                content.GetComponentInChildren<Text>().lineSpacing = 1.2f;

                for (int i = 0; i < nameArray.Length; i++)
                {
                    content.GetComponentInChildren<Text>().text += "\u3000" + nameArray[i] + "<color=#ffffff>" + GetSpaceLength(nameArray[i]) + "</color>" +valueArray[i] + "\n";
                }


            }
        }
    }


    private string GetSpaceLength(string content)
    {
        string result = "";

        int currrentLength = content.Length ;

        int remain = 15 - currrentLength;

        for(int i = 0;i<remain;i++)
        {
            result += "你";
        }

        return result;

    }


}
