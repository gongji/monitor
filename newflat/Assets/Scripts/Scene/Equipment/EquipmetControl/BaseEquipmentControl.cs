using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEquipmentControl : MonoBehaviour {

    public EquipmentItem equipmentItem =new EquipmentItem();

    protected Dictionary<MeshRenderer, Material[]> materialDic = null;

    private bool isAlarm = false;

    protected GameObject equipmentIconObject;

    //测点快捷菜单
    protected GameObject testPointMenu = null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="iconName">icon的名字</param>
    protected void Init(string iconName)
    {
        equipmentItem = GetComponent<Object3DElement>().equipmentData;
        equipmentIconObject = TransformControlUtility.CreateItem("equipment/IconPrefeb/" + iconName, 
            UIUtility.GetRootCanvas().Find("equipmentIcon"));
        EquipmentIcon ei = equipmentIconObject.GetComponent<EquipmentIcon>();
        if (!ei)
        {
            ei = equipmentIconObject.AddComponent<EquipmentIcon>();
        }
        ei.equipmentObject = gameObject;
    }

    public abstract void ExeAnimation(string name,bool isExe);
    private int state = 0;
    public virtual void Alarm(int state=0){

        this.state = state;
        if (isAlarm)
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

        string equipmentId = equipmentItem.id;

        if(string.IsNullOrEmpty(equipmentId))
        {
            equipmentId = GetComponent<Object3DElement>().equipmentData.id;
        }
      //  Debug.Log("equipmentId="+ equipmentId);
        CameraViewData.GetCurrentEquipmentCameraView((result) =>
        {
            Vector3 targetPostion = Vector3.zero;
            Vector3 eulerAngles = Vector3.zero;
            if (result!=null)
            {
                targetPostion = new Vector3(result.x, result.y, result.z);
                eulerAngles = new Vector3(result.rotationX, result.rotationY, result.rotationZ);
                CameraAnimation.CameraMove(Camera.main, targetPostion, eulerAngles, 1.0f, null);
            }
            else
            {
                if(GetComponentInChildren<Camera>()!=null)
                {
                    targetPostion = GetComponentInChildren<Camera>(true).transform.position;
                    eulerAngles = GetComponentInChildren<Camera>(true).transform.eulerAngles;
                    CameraAnimation.CameraMove(Camera.main, targetPostion, eulerAngles, 1.0f, null);
                }
                else
                {
                    SetDefaultWatchPoint();
                }
               
            }

           
        }, equipmentId, (error)=> {

            SetDefaultWatchPoint();
        });
      
        //Camera.main.transform.position = targetPostion;
        //Camera.main.transform.localRotation = quaternion;
       // SelectEffection();
       // LocateBack.instance.Show();
    }

    //设置默认观察点
    private void SetDefaultWatchPoint()
    {
        Vector3 targetPostion = Vector3.zero;
        Vector3 eulerAngles = Vector3.zero;

        BoxCollider collider = transform.GetComponentInChildren<BoxCollider>();

        Vector3 upPostion = transform.position + transform.up * collider.bounds.size.y * 1.5f;

        // Vector3 targetPostion = upPostion + transform.forward * collider.bounds.size.z * 2.0f;
        targetPostion = upPostion + transform.forward * collider.bounds.size.z * 4.0f;
        Vector3 center = transform.position + transform.up * collider.bounds.size.y * 0.5f;
        Vector3 dir = center - targetPostion;
        Quaternion quaternion = Quaternion.LookRotation(dir, transform.up);
        eulerAngles = quaternion.eulerAngles;

        CameraAnimation.CameraMove(Camera.main, targetPostion, eulerAngles, 1.0f, null);
    }

   


    private GameObject selectArrow = null;
    private  void CreateSelectEffection()
    {
       
        selectArrow = TransformControlUtility.CreateItem("equipment/select", null);
        selectArrow.SetActive(false);
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
            AlarmTestPointShow aep = alarmArrow.gameObject.AddComponent<AlarmTestPointShow>();
            aep.equipmentid = equipmentItem.id;
            aep.equipmentName = equipmentItem.name;
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

        if(isAlarm)
        {
            CreateAlarmEffection();
        }
        else
        {
            if(isSelect)
            {
                CreateSelectEffection();
            }
        }

        //if (isSelect)
        //{
            
        //    CreateSelectEffection();
        //}
        //else
        //{
        //    if(isAlarm)
        //    {
        //        CreateAlarmEffection();
        //    }
        //}
    }

    protected void InitMaterial()
    {
        //if(materialDic == null)
        //{
        //    materialDic = new Dictionary<MeshRenderer, Material[]>();
        //}
        //MeshRenderer[] meshRenders = gameObject.GetComponentsInChildren<MeshRenderer>();
        //foreach(MeshRenderer meshRender in meshRenders)
        //{
        //    materialDic.Add(meshRender, meshRender.materials);
        //}
    }

    public void CancelWireframe()
    {
        //if (materialDic == null || !isMerialChange)
        //{
        //    //Debug.Log("材质未恢复");
        //    return;
        //}

        //foreach (MeshRenderer meshRender in materialDic.Keys)
        //{
        //    meshRender.GetComponent<MeshRenderer>().materials = materialDic[meshRender];
        //}
        if(virtualFrameGame!=null)
        {
            GameObject.DestroyImmediate(virtualFrameGame);
        }
        new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>(true)).ForEach((x) => {

            x.enabled = true;
        });

        virtualFrameGame = null;


       // isMerialChange = false;
    }

    private void OnDisable()
    {
        // RestoreMaterial();
        CancelWireframe();
    }

  //  private bool isMe = false;

    private GameObject virtualFrameGame = null;
    public void ShowWireframe(Material wireframe)
    {
        //if(materialDic==null)
        //{
        //    return;
        //}
        //foreach (MeshRenderer meshRender in materialDic.Keys)
        //{
        //    meshRender.GetComponent<MeshRenderer>().sharedMaterial = wireframe;
        //}
        if(virtualFrameGame!=null)
        {
            return;
        }
        virtualFrameGame = VirtualFrameEquipment.Create(transform, wireframe);
        new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>()).ForEach((x) => {

            x.enabled = false;
        });
       // isMerialChange = true;
    }

    public void DestoryTestPointMenu()
    {
        if(testPointMenu!=null)
        {
            GameObject.DestroyImmediate(testPointMenu);
        }
    }
   
}
