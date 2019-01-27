using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SubsystemMsg {

    private static GameObject systemUI = null;
    private static ILog log = LogManagers.GetLogger("SubsystemMsg");

    private static List<SubSystemItem> dataSource = null;

    private static string _sceneid = "";

    public static void Create(string sceneid)
    {

        if(AppInfo.Platform == BRPlatform.Editor)
        {
            return;
        }
        if(string.IsNullOrEmpty(sceneid) && !string.IsNullOrEmpty(_sceneid))
        {
            sceneid = _sceneid;
        }
        SubSystemProxy.GetSubSystemByScene((result) => {
            if(string.IsNullOrEmpty(result))
            {
                log.Debug("sceneid="+ sceneid  + "is empty");
                return;
            }
            Debug.Log("resultSubSystem=" + result);

            dataSource = Utils.CollectionsConvert.ToObject<List<SubSystemItem>>(result);
            systemUI = TransformControlUtility.CreateItem("UI/UISubSystem/SubjetSystemTree", UIUtility.GetRootCanvas());
            systemUI.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            if (systemUI!=null && systemUI.GetComponent<TreeManager>()!=null)
            {
                systemUI.GetComponent<TreeManager>().Init(dataSource);
            }

            _sceneid = sceneid;
        }, sceneid);
    }

    public static void Delete()
    {
        if(systemUI!=null)
        {
            GameObject.DestroyImmediate(systemUI);
            AllMaterialRestore();
            subSystemItem = null;
            ids.Clear();
        }
        
    }

    private static Material wireframeM;
    private static List<string> ids = new List<string>();
    public static void SetWireframe(string id)
    {
        isSubSystemState = true;
        // Debug.Log("id="+id);
        if (wireframeM == null)
        {
            wireframeM = Resources.Load<Material>("Material/wireframe");
        }
        BaseEquipmentControl[] equipments = GameObject.FindObjectsOfType<BaseEquipmentControl>();
        foreach (BaseEquipmentControl be in equipments)
        {
            be.ShowWireframe(wireframeM);
        }
        subSystemItem = null;

        GetEquipmentById(dataSource, id);
        if (subSystemItem != null)
        {
            ids.Clear();
            GetAllEquipment(subSystemItem);

            foreach (string equipmentid in ids)
            {
                if (EquipmentData.GetAllEquipmentData.ContainsKey(equipmentid))
                {
                    EquipmentData.GetAllEquipmentData[equipmentid].GetComponent<BaseEquipmentControl>().CancelWireframe();
                }

            }
        }

    }

    public static bool isSubSystemState = false;

    public static void AllMaterialRestore()
    {
        BaseEquipmentControl[] equipments = GameObject.FindObjectsOfType<BaseEquipmentControl>();
        foreach (BaseEquipmentControl be in equipments)
        {
            be.CancelWireframe();
        }
        isSubSystemState = false;
    }

    private static SubSystemItem subSystemItem = null;
   /// <summary>
   /// 通过子系统或者模型id
   /// </summary>
   /// <param name="id"></param>
   /// <returns>设备ids</returns>
    private static void GetEquipmentById(List<SubSystemItem> list,string id)
    {
        if(list == null)
        {
            return ;
        }
        for(int i=0;i< list.Count;i++)
        {
            if(list[i].id.Equals(id))
            {
                subSystemItem = list[i];
                return;
            }

            if(list[i].childs!=null && list[i].childs.Count>0)
            {

                foreach(SubSystemItem child in list[i].childs)
                {
                    GetEquipmentById(list[i].childs, id);
                }
            }
        }
    }

    private const string euipmentName = "equipment";
    private static void GetAllEquipment(SubSystemItem subSystemItem)
    {

        if(subSystemItem.type.Equals(euipmentName))
        {
            ids.Add(subSystemItem.id);
        }
       for(int i=0; subSystemItem.childs!=null && subSystemItem.childs.Count>0&&i < subSystemItem.childs.Count;i++)
        {
            SubSystemItem child = subSystemItem.childs[i];
            if (child.type.Equals(euipmentName))
            {
                ids.Add(subSystemItem.childs[i].id);
            }
            if(child.childs!=null && child.childs.Count>0)
            {
                foreach(SubSystemItem _child in child.childs)
                {
                    GetAllEquipment(_child);
                }
            }
        }
        
    }
}
