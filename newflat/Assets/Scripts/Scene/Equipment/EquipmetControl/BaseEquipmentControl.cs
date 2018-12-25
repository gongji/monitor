using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEquipmentControl : MonoBehaviour {

    public EquipmentItem equipmentItem =new EquipmentItem();

    protected Dictionary<MeshRenderer, Material[]> materialDic = null;

    private bool isAlarm = false;

    protected GameObject equipmentIconObject;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="iconName">icon的名字</param>
    protected void Init(string iconName)
    {
        equipmentItem = GetComponent<Object3DElement>().equipmentData;
        equipmentIconObject = TransformControlUtility.CreateItem("IconPrefeb/"+ iconName, UIUtility.GetRootCanvas());
        EquipmentIcon ei = equipmentIconObject.GetComponent<EquipmentIcon>();
        if (!ei)
        {
            ei = equipmentIconObject.AddComponent<EquipmentIcon>();
        }
        ei.equipmentObject = gameObject;
    }
    public virtual void Alarm(){
        if(isAlarm)
        {
            return;
        }
        isAlarm = true;
        UpdateShow();
    }




    public virtual void CancleAlarm() {
        if(!isAlarm)
        {
            return;
        }
        isAlarm = false;
        UpdateShow();
    }

    private bool isSelect = false;
    public virtual void SelectEquipment() {

        if(isSelect)
        {
            return;
        }
        isSelect = true;
        UpdateShow();

        CameraViewData.GetCurrentEquipmentCameraView((result) =>
        {
            Vector3 targetPostion = Vector3.zero;
            Vector3 eulerAngles = Vector3.zero;
            if (result!=null)
            {
                targetPostion = new Vector3(result.x, result.y, result.z);
                eulerAngles = new Vector3(result.rotationX, result.rotationY, result.rotationZ);
            }
            else
            {
                BoxCollider collider = transform.GetComponentInChildren<BoxCollider>();

                Vector3 upPostion = transform.position + transform.up * collider.bounds.size.y * 1.5f;

                // Vector3 targetPostion = upPostion + transform.forward * collider.bounds.size.z * 2.0f;
                targetPostion = upPostion + transform.forward * collider.bounds.size.z * 4.0f;
                Vector3 center = transform.position + transform.up * collider.bounds.size.y * 0.5f;
                Vector3 dir = center - targetPostion;
                Quaternion quaternion = Quaternion.LookRotation(dir, transform.up);
                eulerAngles = quaternion.eulerAngles;
            }

            CameraAnimation.CameraMove(Camera.main, targetPostion, eulerAngles, 1.0f, null);
        }, equipmentItem.id);
      
        //Camera.main.transform.position = targetPostion;
        //Camera.main.transform.localRotation = quaternion;
       // SelectEffection();
       // LocateBack.instance.Show();
    }

   


    private GameObject selectArrow = null;
    private  void CreateSelectEffection()
    {
       
        selectArrow = TransformControlUtility.CreateItem("equipment/select", null);

        selectArrow.name = transform.name;
        selectArrow.transform.position = transform.position + 
            Vector3.up * GetComponentInChildren<BoxCollider>().bounds.size.y + Vector3.up * 0.05f;
        
        selectArrow.transform.SetParent(transform.parent);
        EffectionUtility.PlayDotweenAphlaFlash(gameObject,Color.blue, "_Color");
       
    }


    private GameObject alarmArrow;
    private void CreateAlarmEffection()
    {
        if(GetComponentInChildren<BoxCollider>()!=null)
        {
            alarmArrow = TransformControlUtility.CreateItem("equipment/alarm", null);
            alarmArrow.transform.position = transform.position +
                Vector3.up * GetComponentInChildren<BoxCollider>().bounds.size.y + Vector3.up * 0.05f;
            alarmArrow.transform.SetParent(transform.parent);

            EffectionUtility.PlayDotweenAphlaFlash(gameObject, Color.red, "_Color");
        }
       
    }

    public virtual void CancelEquipment()
    {
        if (!isSelect)
        {
            return;
        }

        isSelect = false;
        UpdateShow();
       
        //LocateBack.instance.Hide();
    }

    private void CancelEffection(GameObject effectionObject)
    {
        if (effectionObject != null)
        {
            GameObject.Destroy(effectionObject);
        }
        if (GetComponentInChildren<BoxCollider>() != null)
        {
            DoTweenAphlaFlashEffection afe = GetComponent<DoTweenAphlaFlashEffection>();
            if (afe != null)
            {
                afe.StopAllTask();
            }
        }
           
    }

 
    public virtual void OnMouseClick() { }

   

    private void UpdateShow()
    {
        CancelEffection(selectArrow);
        CancelEffection(alarmArrow);

        if (isSelect)
        {
            
            CreateSelectEffection();
        }
        else
        {
            if(isAlarm)
            {
                CreateAlarmEffection();
            }
        }
    }

    protected void InitMaterial()
    {
        if(materialDic == null)
        {
            materialDic = new Dictionary<MeshRenderer, Material[]>();
        }
        MeshRenderer[] meshRenders = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer meshRender in meshRenders)
        {
            materialDic.Add(meshRender, meshRender.materials);
        }
    }

    public void RestoreMaterial()
    {
        if (materialDic == null || !isMerialChange)
        {
            //Debug.Log("材质未恢复");
            return;
        }

        foreach (MeshRenderer meshRender in materialDic.Keys)
        {
            meshRender.GetComponent<MeshRenderer>().materials = materialDic[meshRender];
        }
        isMerialChange = false;
    }

    private void OnDisable()
    {
        RestoreMaterial();
    }

    private bool isMerialChange = false;
    public void ChangMaterial(Material wireframe)
    {
        if(materialDic==null)
        {
            return;
        }
        foreach (MeshRenderer meshRender in materialDic.Keys)
        {
            meshRender.GetComponent<MeshRenderer>().sharedMaterial = wireframe;
        }
        isMerialChange = true;
    }
   
}
