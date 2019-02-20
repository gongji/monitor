using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataModel;

public class TipsMgr : MonoSingleton<TipsMgr>,IEventListener {
    
 
    public void InitManager()
    {
       
    }

    private List<Transform> tipsList = new List<Transform>();
    public void CreateTips(GameObject collider, Object3dItem object3dItem,Transform parent,Vector3 minScale,Vector3 maxScale)
    {
        EventMgr.Instance.RemoveListener(this, EventName.DeleteObject);
        EventMgr.Instance.AddListener(this, EventName.DeleteObject);
        if (collider != null)
        {
            FlyTextMeshModel tmm = collider.GetComponent<FlyTextMeshModel>();
            if (tmm == null)
            {

                tmm = collider.AddComponent<FlyTextMeshModel>();
            }
            tmm.id = object3dItem.id;
            BoxCollider bc = collider.GetComponent<BoxCollider>();
            if (bc == null)
            {
                bc = collider.gameObject.AddComponent<BoxCollider>();
            }
            bool isActive = bc.enabled;
            bc.enabled = true;
            Bounds boudns = bc.GetComponent<BoxCollider>().bounds;
            tmm.MinLoacalScale = minScale;
            tmm.MaxLocalScale = maxScale;
            //Debug.Log(boudns.center + Vector3.up * boudns.size.y);
           // Debug.Log(parent.name);
           Transform tips =   tmm.Create(object3dItem.name, boudns.center + Vector3.up * boudns.size.y/2.0f, parent);
            tipsList.Add(tips);
            bc.enabled = isActive;
            
            View2dTextManager.Instance.Create2dText(object3dItem, boudns);
        }
    }

    private void DeleteTips()
    {
        foreach(Transform tips in tipsList)
        {
            if(tips!=null)
            {
                GameObject.Destroy(tips.gameObject);
            }
            
        }
        tipsList.Clear();
        View2dTextManager.Instance.Delete3dText();

        EventMgr.Instance.RemoveListener(this, EventName.DeleteObject);
    }

    private bool isVisible = true;
    void Update()
    {
        if (AppInfo.currentView == ViewType.View3D)
        {
           
            isVisible = true;
            View2dTextManager.Instance.ShowHide(false);
        }
        else
        {
            isVisible = false;
            View2dTextManager.Instance.ShowHide(true);
        }
        foreach (Transform tips in tipsList)
        {
            if(!tips)
            {
                continue;
            }
            if(isVisible && TipsToggle.isOn)
            {
                tips.gameObject.SetActive(true);
            }
            else
            {
                tips.gameObject.SetActive(false);
            }
            
        }
    }

    public bool HandleEvent(string eventName, IDictionary<string, object> dictionary)
    {
        //throw new System.NotImplementedException();

        if (eventName.Equals(EventName.DeleteObject))
        {
            DeleteTips();
        }

        return true;
    }



    public void Create3dText(List<Object3dItem> data)
    {
        if (data != null && data.Count > 0)
        {
            foreach (Object3dItem object3dItem in data)
            {
                GameObject rootGameObjerct = SceneUtility.GetGameByRootName(object3dItem.number, object3dItem.number);
                if (rootGameObjerct != null)
                {

                    GameObject collider = FindObjUtility.GetTransformChildByName(rootGameObjerct.transform, Constant.ColliderName);
                    CreateTips(collider, object3dItem, collider.transform, Vector3.one * 5, Vector3.one * 10);
                }
            }
        }
    }
}
