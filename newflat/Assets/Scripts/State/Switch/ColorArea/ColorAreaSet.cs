using Core.Common.Logging;
using DataModel;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  System.Linq;

public class ColorAreaSet : FullAreaColorSet
{
    private static ILog log = LogManagers.GetLogger("ColorAreaSet");
    private GameObject colorImageUI;
    #region 设置全员场景初始化
    public override void Enter(List<Object3dItem> currentlist, System. Action callBack)
    {
        base.Enter(currentlist, callBack);
        //SaveOrResetFloorPostion(currentlist);
        Object3dItem currentScene = SceneContext.currentSceneData;
        //foreach(Object3dItem item  in currentlist)
        //{
        //    Debug.Log("number="+ item.number);
        //}
       // Debug.Log(currentScene.number);
        SceneContext.currentSceneData = FindMapWqItem();

        // Debug.Log(SceneContext.currentSceneData.number);
        //  CameraInitSet.StartSet(SceneContext.areaBuiderId, null, 0.5f, ()=> {
        SetCamera();
        SetSkyEffection();
        //设置能耗展示
        SetEnergyConsumptionShow();
        CreateNameTip(SceneData.FindObjUtilityect3dItemById(SceneContext.areaBuiderId).name);
         
        colorImageUI = TransformControlUtility.CreateItem("UI/fullAreColor", UIUtility.GetRootCanvas());
        colorImageUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -40.0f);
        colorImageUI.name = "colorImage";

        Object3dItem currentWq = SceneData.FindObjUtilityect3dItemById(SceneContext.areaBuiderId);
        CreateNavigation(currentWq, null, "返回");

        if (callBack != null)
        {
            callBack.Invoke();
        }
        ExternalSceneSwitch.Instance.SaveSwitchData("4", SceneContext.currentSceneData.id);
        // });
    }


    private GameObject uiTempObject;
    /// <summary>
    /// 创建每一楼层的能源数据
    /// </summary>
    /// <param name="floorList"></param>
    private void CreateFloorTips(List<Transform> floorList)
    {
        if(uiTempObject==null)
        {
            uiTempObject = new GameObject();
        }
        uiTempObject.name = "ColorAreaNavigationUI";
        ColorAreaNavigationUI fnu = uiTempObject.AddComponent<ColorAreaNavigationUI>();
        fnu.CreateNavigateUI(floorList);
    }
    /// <summary>
    /// 设置能耗展示
    /// </summary>
    private  void SetEnergyConsumptionShow()
    {
        EnergyConsumptionProxy.GetEnergyConsumptionData((result) => {
           // List<EnergyConsumptionItem> energyData = Utils.CollectionsConvert.ToObject<List<EnergyConsumptionItem>>(result);

        },SceneContext.areaBuiderId
        );

        List<EnergyConsumptionItem> energyData = EnergyConsumptionTestData.GetTestData();
        Dictionary<string, EnergyConsumptionItem> dic = new Dictionary<string, EnergyConsumptionItem>();
        foreach(EnergyConsumptionItem item in energyData)
        {
            if(!dic.ContainsKey(item.id))
            {
                dic.Add(item.id, item);
            }
           
        }

        string wqNumber = FindMapWqItem().number;
        //查找外构

        List<Transform> floorList = new List<Transform>();
        if(!string.IsNullOrEmpty(wqNumber))
        {
           GameObject root =  SceneUtility.GetGameByRootName(wqNumber, wqNumber);

            if(root!=null)
            {
                foreach (Transform chilid in root.transform)
                {
                    floorList.Add(chilid);
                    string floorid = SceneData.GetIdByNumber(chilid.name.ToLower().Trim());

                    EnergyConsumptionItem item = null;
                    dic.TryGetValue(floorid, out item);
                    if(item!=null)
                    {
                        CreateEnergyConsumptionMaterial(item, chilid);
                    }
                }
            }
        }

        SwitchCamera(floorList);
        CreateFloorTips(floorList);
    }

    protected override void CreateNavigation(Object3dItem currentData, string frontname, string backName)
    {
        base.CreateNavigation(currentData, frontname, backName);
       // string parentid = currentData.parentsId;
        List<Object3dItem> wqList = SceneData.GetAllWq();
        //Object3dItem tempWqItem = null;
        //for (int i=0;i< wqList.Count;i++)
        //{
        //   if(wqList[i].id.Equals(SceneContext.areaBuiderId))
        //    {
        //        tempWqItem = wqList[i];
        //        wqList.RemoveAt(i);
        //        break;
        //    }
        //}
        //if(tempWqItem!=null)
        //{
        //    wqList.Insert(0, tempWqItem);
        //}
       

       // Object3dItem object3dItem = SceneData.FindObjUtilityect3dItemById(parentid);
        if (fnu!=null)
        {
            fnu.CreateFloorRoomNavagitionList(wqList, navigationUI.transform, currentData, frontname, backName,true);
        }
        
       
    }

    /// <summary>
    /// 创建能耗材质
    /// </summary>
    /// <param name="item"></param>
    /// <param name="t"></param>
    private void CreateEnergyConsumptionMaterial(EnergyConsumptionItem item,Transform t)
    {
        if(t.GetComponent<MeshRenderer>().material.name.Equals(t.name))
        {
            return;
        }
        Shader shader1 = Shader.Find("Custom/Multi-Gradient");

        Material m = new Material(shader1);
        m.name = t.name;

        t.GetComponent<MeshRenderer>().material = m;

        Color startc = new Color32(item.startR, item.startG,item.startB, item.startA);
        Color endc = new Color32(item.endR, item.endG, item.endB, item.endA);
        m.SetColor("_Color1", startc);
        m.SetColor("_Color2", endc);
    }

    /// <summary>
    /// 切换为旋转相机
    /// </summary>
    /// <param name="list"></param>
    private void SwitchCamera(List<Transform> list)
    {
        if(list==null || list.Count == 0)
        {
            return;
        }

        IEnumerable<Transform> result =
            from t in list
            orderby t.name.ToLower().Contains(Constant.ColliderName.ToLower())
            select t;

        Transform resultTransform = result.ToList<Transform>()[0];
        //Debug.Log(resultTransform);
        CameraInitSet.SetRotationCamera(resultTransform,true);
    }

    /// <summary>
    /// 查找wq的场景
    /// </summary>
    /// <returns></returns>
    private Object3dItem FindMapWqItem()
    {
        foreach (Object3dItem item in currentlist)
        {
            if (item.number.Contains(Constant.WQName))
            {
                return item;
            }
        }

        return null;
    }

    private Transform FindMapWqTransform()
    {
        Object3dItem mapWqItem = FindMapWqItem();
        GameObject g =   SceneUtility.GetGameByRootName(mapWqItem.number, mapWqItem.number);
        if(g!=null)
        {
            return g.transform;
        }

        return null;
    }

    private FlyTextMeshModel tmm = null;
    private void CreateNameTip(string name)
    {

        Transform t = FindMapWqTransform();

        if(t==null || tmm!=null)
        {
            return;
        }
 
        GameObject collider = FindObjUtility.GetTransformChildByName(t, Constant.ColliderName);
        tmm = collider.GetComponent<FlyTextMeshModel>();
        if (tmm == null)
        {

            tmm = collider.AddComponent<FlyTextMeshModel>();
        }
       
        BoxCollider bc = collider.GetComponent<BoxCollider>();
        if (bc == null)
        {
            bc = collider.gameObject.AddComponent<BoxCollider>();
        }
        bool isActive = bc.enabled;
        bc.enabled = true;
        Bounds boudns = bc.GetComponent<BoxCollider>().bounds;
        tmm.MinLoacalScale = Vector3.one * 6 ;
        tmm.MaxLocalScale = Vector3.one * 10;
        tmm.isAddScript = false;

        tmm.Create(name, boudns.center + Vector3.up * boudns.size.y, collider.transform);
    }
    #endregion

    #region 退出场景的逻辑处理
    public override void Exit(string nextid, System.Action callBack)
    {

        base.Exit(nextid, callBack);
        
        Exit(nextid);
        if (callBack != null)
        {
            callBack.Invoke();
        }
    }
   

   public override void Exit(string nextid)
    {
        base.Exit(nextid);

        if(uiTempObject)
        {
            uiTempObject.GetComponent<ColorAreaNavigationUI>().DeleteAllUI();
            GameObject.DestroyImmediate(uiTempObject);
        }
        
        RenderSettingsValue.SetNoAreaEffction();
        CameraInitSet.SetObjectCamera();

        if(colorImageUI!=null)
        {
            GameObject.DestroyImmediate(colorImageUI);
        }
        if (navigationUI != null)
        {
            GameObject.Destroy(navigationUI);
        }

    }
    #endregion
}
