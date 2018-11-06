using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 查看相机
/// </summary>
public class ViewEquipmentInfo : SingletonCS<ViewEquipmentInfo> {

    private GameObject selectParticleEffection = null;

    private GameObject currentCloneEquipment = null;

    private GameObject currentEquipment = null;

    private GameObject selectArrow = null;

    public void Init(string EquipmentId,System.Action cameraLocateEquipmentCallBack)
    {
        //相机定位到设备相机的位置
        if(cameraLocateEquipmentCallBack != null)
        {
            //if(Camera.main.GetComponent<CameraRotatoAround>()!=null)
            //{
            //    Camera.main.GetComponent<CameraRotatoAround>().SetEnable(false);
            //}
            
            cameraLocateEquipmentCallBack();
        }
        RemoveAllResouce();
        CreateEquipment(EquipmentId);
        
        //显示测点
        ShowTestPoint.Show(currentEquipment.GetComponent<BaseEquipmentControl>().equipmentItem.name,EquipmentId);


    }
    

    private string equipmentId = string.Empty;
    /// <summary>
    ///创建放大的设备
    /// </summary>
    /// <param name="id"></param>
    private void CreateEquipment(string EquipmentId)
    {
        if (EquipmentData.GetEquipmentDic.ContainsKey(EquipmentId))
        {
            currentEquipment = EquipmentData.GetEquipmentDic[EquipmentId];
            currentCloneEquipment = GameObject.Instantiate(currentEquipment);
            Object3dUtility.SetLayerValue(LayerMask.NameToLayer("equipmentZoom"), currentCloneEquipment);
            currentCloneEquipment.transform.localScale = Vector3.one * 0.1f;
            currentCloneEquipment.transform.localPosition = new Vector3(0.26f, -0.04f, 2.5f);
            currentCloneEquipment.transform.eulerAngles = new Vector3(-90.0f, -0.24f, 30.0f);
            currentCloneEquipment.AddComponent<AutoRotation>();
            BaseEquipmentControl bec = currentCloneEquipment.GetComponent<BaseEquipmentControl>();
            if(bec!=null)
            {
                GameObject.Destroy(bec);
            }


            //增加闪烁效果
            AphlaFlashEffection afe = currentEquipment.GetComponent<AphlaFlashEffection>();
            if(afe==null)
            {
                afe = currentEquipment.gameObject.AddComponent<AphlaFlashEffection>();
            }
            
            //增加箭头
            selectArrow =  TransformControlUtility.CreateItem("equipment/select", null);

            selectArrow.transform.position = currentEquipment.transform.position + Vector3.up * currentEquipment.GetComponent<BoxCollider>().bounds.size.y + Vector3.up * 0.1f;
            selectArrow.transform.localScale = Vector3.one * 0.1f;
            this.equipmentId = EquipmentId;
            CreateEffection();


        }

    }

    public string GetCurrentSelectDeive()
    {

        return equipmentId;
    }

    private GameObject selectEffection = null;
    /// <summary>
    /// 创建效果类
    /// </summary>
    private void CreateEffection()
    {
        if (selectEffection == null)
        {

            string url = string.Empty;
#if UNITY_EDITOR
            url = "file://" + Application.dataPath + "/StreamingAssets/R/selecteffect";
#else
                    url = Application.dataPath + "/StreamingAssets/R/selecteffect";
#endif
            ResourceUtility.Instance.GetHttpAssetBundle(url, (bundle) =>
            {
                selectEffection = GameObject.Instantiate(bundle.LoadAsset<GameObject>("selecteffect"));
               
                Object3dUtility.SetLayerValue(LayerMask.NameToLayer("equipmentZoom"), selectEffection);
                selectEffection.name = "equipmentSelect";
                selectEffection.transform.localPosition = new Vector3(0.26f, -0.04f, 1.75f);
                selectEffection.transform.localEulerAngles = new Vector3(10f, 0f, 0f);
                selectEffection.transform.localScale = Vector3.one * 0.04f;
            });
        }
        else
        {
            selectEffection.SetActive(true);
        }
    }

    /// <summary>
    /// 删除资源
    /// </summary>
    /// <param name="isDeleteEffection"></param>
    public void RemoveAllResouce(bool isDeleteEffection = false)
    {
        //取消当前设备的效果
        if (currentEquipment != null)
        {
            AphlaFlashEffection afe = currentEquipment.GetComponent<AphlaFlashEffection>();

            if(afe!=null)
            {
                afe.StopAllTask();
            } 
        }

        //删除克隆的设备

        if(currentCloneEquipment!=null)
        {
            GameObject.Destroy(currentCloneEquipment);
            currentCloneEquipment = null;
        }

        if (selectArrow != null)
        {
            GameObject.Destroy(selectArrow);
            selectArrow = null;

        }

       //粒子效果
        if (selectParticleEffection != null && isDeleteEffection)
        {
            GameObject.Destroy(selectParticleEffection);
            currentEquipment = null;
        }
        equipmentId = string.Empty;

        if(selectEffection!=null)
        {
            selectEffection.SetActive(false);
        }

        ShowTestPoint.DestryGrid();
        


    }
    /// <summary>
    /// 直接显示
    /// </summary>
    public void DInit()
    {

    }
    protected  override void InitFunction()
    {

    }
    protected  override void DestroyFunction()
    {

    }
}
