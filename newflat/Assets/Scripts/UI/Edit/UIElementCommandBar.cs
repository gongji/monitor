using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using DataModel;
using TMPro;

/// <summary>
/// 
/// </summary>
public sealed class UIElementCommandBar : MonoBehaviour
{
    public static UIElementCommandBar instance { get; private set; }

    private enum Operation
    {
        None,
        MoveXZ,
        MoveY,
        RotaionY,
    }
    public Button moveXZButton, moveYButton;
    public Button rotateButton;
    
    public Button copy;
    public Button mulkCopy;

    public Button edit;

    public Button locate;

    public Button bind;

    public Button delete;

    private bool isHandlingMouseDrag = false;

    private Operation currentOperation;

    private Vector3 offsetClick;
    private Vector3 mousePosition;

    public Transform selectingObjectTransform;
    private Vector3 prePosition;


    private List<Button> buttons = new List<Button>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Init();
        TransformControlUtility.AddEventToBtn(copy.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { CopyClick(copy.gameObject); });
        TransformControlUtility.AddEventToBtn(mulkCopy.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { MulkCopyClick(mulkCopy.gameObject); });
        TransformControlUtility.AddEventToBtn(edit.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { EditClick(edit.gameObject); });
        TransformControlUtility.AddEventToBtn(locate.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { SaveLocateClick(locate.gameObject); });
        TransformControlUtility.AddEventToBtn(bind.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { BindClick(bind.gameObject); });
        TransformControlUtility.AddEventToBtn(delete.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { DeleteClick(delete.gameObject); });
        buttons.Add(moveXZButton);
        buttons.Add(moveYButton);
        buttons.Add(rotateButton);
        buttons.Add(copy);
        buttons.Add(mulkCopy);
        buttons.Add(edit);
        buttons.Add(locate);
        buttons.Add(bind);
        buttons.Add(delete);


    }

    public void CopyClick(GameObject g)
    {
        Hide();
        
    }
    //批量复制
    public void MulkCopyClick(GameObject g)
    {
        OperateControlManager.Instance.CurrentState = OperateControlManager.EquipmentEditState.BulkCopy;
        OperateControlManager.Instance.BatchCopyEquipment(selectingObjectTransform);
        Hide();
       
       
    }

    private gizmoScript gs = null;
    /// <summary>
    /// 门的话就保存，其他有模型的设备就编辑
    /// </summary>
    /// <param name="g"></param>
    public void EditClick(GameObject g)
    {
        //
        if(selectingObjectTransform.GetComponent<Object3DElement>().type == Type.De_Door)
        {
         
            SaveEquipmentData.SaveDoor(selectingObjectTransform.GetComponent<Object3DElement>().equipmentData);
        }
        else
        {
            Transform selectE = selectingObjectTransform.GetComponent<Object3DElement>().transform;
          
            OperateControlManager.Instance.CurrentState = OperateControlManager.EquipmentEditState.Edit;
            if (gs == null)
            {
                gs = TransformControlUtility.CreateItem("Edit/Gizmo", null).GetComponent<gizmoScript>();
                gs.transform.localScale = Vector3.one *1.0f;
            }
            gs.SetSelectObject(selectE);
        }

        Hide();

    }

    public void DestroyGizmo()
    {
        if(gs!=null)
        {
            GameObject.DestroyImmediate(gs.par);
            GameObject.DestroyImmediate(gs.gameObject);
        }
       
        gs = null;
    }

    //保存定位点
    public void SaveLocateClick(GameObject g)
    {
        EquipmentItem equipmentItem = selectingObjectTransform.GetComponent<Object3DElement>().equipmentData;
        if(equipmentItem!=null)
        {
            CameraViewData.SaveEquipmentCameraView(equipmentItem.id);
        }
      
        Hide();
    }

    public void BindClick(GameObject g)
    {
        Hide();
    }
    //删除
    private void DeleteClick(GameObject g)
    {
        
        Object3DElement equipmentItem = selectingObjectTransform.GetComponent<Object3DElement>();
        if(selectingObjectTransform!=null)
        {
            //不为空的话，保存数据库
            if(!string.IsNullOrEmpty(equipmentItem.equipmentData.id))
            {
                EquipmentData.RemoveDeleteEquipment(equipmentItem.equipmentData.id);

                Object3DElement.AddDeleteItem(equipmentItem.equipmentData.id);
            }
            else
            {
                //移除新建的列表
                Object3DElement.DeleteNewItem(equipmentItem);
            }
            GameObject.Destroy(selectingObjectTransform.gameObject);
        }
        Hide();

    }

    public void Init()
    {
        isHandlingMouseDrag = false;
        currentOperation = Operation.None;
        transform.localScale = Vector3.zero;


    }

    private float showTime = 0.25f;
    public void Show()
    {
        transform.localScale = Vector3.zero;
        EquipmentUIControl.instance.ShowModelList(false);
        transform.DOScale(1.0f, showTime);
       
    }

    public void Hide()
    {
       
        EquipmentUIControl.instance.ShowModelList(true);
        transform.DOScale(0.0f, showTime);
        selectingObjectTransform = null;


    }

    
    void Update()
    {
       
        if (selectingObjectTransform != null)
        {
            RefreshUIPostion();
        }


        if (AppInfo.currentView == ViewType.View3D)
        {
            moveYButton.GetComponent<Button>().enabled = true;
            //  edit.GetComponent<Button>().enabled = true;
            edit.GetComponent<EventTrigger>().enabled = true;
            locate.GetComponent<EventTrigger>().enabled = true;


            moveYButton.GetComponent<Image>().color = Color.white;
            locate.GetComponent<Image>().color = new Color32(0, 152, 255, 255);
            edit.GetComponent<Image>().color = new Color32(0,152,255,255);
        }
        else
        {
            moveYButton.GetComponent<Button>().enabled = false;
            edit.GetComponent<EventTrigger>().enabled = false;
            locate.GetComponent<EventTrigger>().enabled = false;
            moveYButton.GetComponent<Image>().color = Color.gray;
            edit.GetComponent<Image>().color = Color.gray;
            locate.GetComponent<Image>().color = Color.gray;
        }
    }

    public void HideOther(string name)
    {
        foreach(Button button in buttons)
        {

            if (name.Equals(button.name))
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }
        
    }

    public void ShowAll()
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
           
        }
    }

    private void RefreshUIPostion()
    {

        Vector3 prePosition = Object3dUtility.GetMaxBounds(selectingObjectTransform).center;
        Vector3 uiPostion = UIUtility.WorldToUI(prePosition, Camera.main);
        GetComponent<RectTransform>().anchoredPosition = uiPostion;
        
    }

  
    private Vector3 selectPoint = Vector3.zero;

    /// 选中设备
    /// </summary>
    public void SelectEquipment(GameObject currentGameObject)
    {
        if (currentGameObject == null || OperateControlManager.Instance.CurrentState != OperateControlManager.EquipmentEditState.None)
        {
            return;
        }
        Init();
        EffectionUtility.StopOutlineEffect(selectingObjectTransform);
        selectingObjectTransform = currentGameObject.transform;
        EffectionUtility.PlayOutlineEffect(selectingObjectTransform,Color.blue,Color.yellow);
        Show();
        PropertySet.instance.UpdateData(selectingObjectTransform.GetComponent<Object3DElement>().equipmentData);
        if(selectingObjectTransform.GetComponent<Object3DElement>().type  == Type.De_Door)
        {
            SetEditName(true);
        }
        else
        {
            SetEditName(false);
        }
    }

    public void SetEditName(bool isDoor)
    {
        if (isDoor)
        {
            edit.GetComponentInChildren<TextMeshProUGUI>().text = "保存";
        }
        else
        {
            edit.GetComponentInChildren<TextMeshProUGUI>().text = "编辑";
        }
    }

    public void CancelSelect()
    {
        EffectionUtility.StopOutlineEffect(selectingObjectTransform);
        ShowAll();
        Hide();
        selectingObjectTransform = null;
    }



}
