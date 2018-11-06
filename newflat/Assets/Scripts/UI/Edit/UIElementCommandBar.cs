using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

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
        TransformControlUtility.AddEventToBtn(locate.gameObject, UnityEngine.EventSystems.EventTriggerType.PointerClick, (da) => { LocateClick(locate.gameObject); });
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
    public void MulkCopyClick(GameObject g)
    {
        Hide();
    }




    public void EditClick(GameObject g)
    {
        Hide();
    }


    public void LocateClick(GameObject g)
    {
        Hide();
    }

    public void BindClick(GameObject g)
    {
        Hide();
    }
    private void DeleteClick(GameObject g)
    {
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
        // Debug.Log("hide");
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
            moveYButton.GetComponent<Image>().color = Color.white;
            edit.GetComponent<Image>().color = new Color32(0,152,255,255);
        }
        else
        {
            moveYButton.GetComponent<Button>().enabled = false;
            edit.GetComponent<EventTrigger>().enabled = false;

            moveYButton.GetComponent<Image>().color = Color.gray;
            edit.GetComponent<Image>().color = new Color32(0, 152, 255, 130);
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
        if (currentGameObject == null)
        {
            return;
        }
        Init();
        EffectionUtility.StopFlashingEffect(selectingObjectTransform);
        selectingObjectTransform = currentGameObject.transform;
        EffectionUtility.playSelectingEffect(selectingObjectTransform);
        Show();

        
    }

    public void CancelSelect()
    {
        EffectionUtility.StopFlashingEffect(selectingObjectTransform);
        ShowAll();
        Hide();
        selectingObjectTransform = null;
    }



}
