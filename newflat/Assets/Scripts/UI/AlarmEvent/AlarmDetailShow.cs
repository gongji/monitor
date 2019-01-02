using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using animation;

public  class AlarmDetailShow:MonoBehaviour  {

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
    private UICenterScaleBig uiCenterScaleBig = null;
    public void Show(AlarmEventItem aei)
    { 
        uiCenterScaleBig = new UICenterScaleBig(gameObject,0.5f);
        uiCenterScaleBig.EnterAnimation(()=> {
            System.Type type = aei.GetType();
            FieldInfo[] fieldInfos = type.GetFields();
            foreach (var f in fieldInfos)
            {
                string fieldName = f.Name;
                //Debug.Log("fieldName="+ fieldName);
                object fieldValue = f.GetValue(aei);
                //  Debug.Log("fieldValue="+ fieldValue);
                SetValue(fieldName, fieldValue.ToString());

            }
        });
       

    }
    private void SetValue(string fieldName,string fieldValue)
    {

        foreach(Text text in GetComponentsInChildren<Text>())
        {
            if(text.name.Trim().ToLower().Equals(fieldName.ToLower().Trim()))
            {
                text.text = fieldValue;
                break;
            }
        }
    }

    public void Hide()
    {
        if(uiCenterScaleBig!=null)
        {
            uiCenterScaleBig.ExitAnimation(() => {
                GameObject.DestroyImmediate(gameObject);
            });
        }
       
    }
}
