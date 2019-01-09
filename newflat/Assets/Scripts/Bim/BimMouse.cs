using animation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BimMouse : MonoBehaviour {

    public Vector3 defaultPostion = Vector3.zero;
    private void Awake()
    {
        defaultPostion = transform.position;
    }

    private void OnDisable()
    {
        Reset();
    }

    public void Reset()
    {
        transform.position = defaultPostion;
    }
    public void OnMouseDown()
    {
        if(ui!=null || !BimMsg.instacne.isSelected)
        {
            return;
        }
        BimProxy.GetBimData((result) => {
            DoData(result);
        }, transform.name, (error) => {

            DoData(TestBim.CreateData());

        });
    }

    private UICenterScaleBig uiCenterScaleBig = null;
    private GameObject ui = null;
    private void DoData(string json)
    {
       // Debug.Log("json="+ json);
        Dictionary<string, object> dic = Utils.CollectionsConvert.ToObject<Dictionary<string, object>>(json);

        List<BimItem> propertyList = Utils.CollectionsConvert.ToObject<List<BimItem>>(dic["property"].ToString());
      //  Debug.Log(propertyList.Count);

        List<BimItem> typeList = Utils.CollectionsConvert.ToObject<List<BimItem>>(dic["type"].ToString());

        //  Debug.Log(typeList.Count);

        ui = TransformControlUtility.CreateItem("UI/tab/TabControl", UIUtility.GetRootCanvas());
        ui.transform.Find("Close").GetComponent<Button>().onClick.AddListener(CloseWindow);
        uiCenterScaleBig = new UICenterScaleBig(ui, 0.5f);
        uiCenterScaleBig.EnterAnimation(() => {
            ui.GetComponent<BimDataShow>().Show(propertyList, typeList);

        });

    }

    private void CloseWindow()
    {
        if (uiCenterScaleBig != null)
        {
            uiCenterScaleBig.ExitAnimation(() => {
                if(ui!=null)
                {
                    GameObject.DestroyImmediate(ui);
                    ui = null;
                }
                
            });
        }
    }


}
