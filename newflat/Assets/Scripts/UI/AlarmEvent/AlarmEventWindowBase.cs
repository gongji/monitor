using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using animation;

public  class AlarmEventWindowBase : MonoBehaviour  {

   
    protected UICenterScaleBig uiCenterScaleBig = null;

    protected AlarmEventItem aei;
    public void Show(AlarmEventItem aei)
    {
        this.aei = aei;
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
               // SetValue(fieldName, fieldValue.ToString());

            }
        });
       

    }
    protected void SetValue(string fieldName,string fieldValue)
    {
        Debug.Log("1");
        Text[] texts = GetComponentsInChildren<Text>();
        Debug.Log("2");
        //foreach (Text text in GetComponentsInChildren<Text>())
        //{
        //    Debug.Log("3");
        //    if (text.name.Trim().ToLower().Equals(fieldName.ToLower().Trim()))
        //    {
        //        text.text = fieldValue;
        //        break;
        //    }
        //}
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
