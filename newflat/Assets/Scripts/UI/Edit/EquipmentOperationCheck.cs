using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


/// <summary>
/// 检测鼠标
/// </summary>
public class EquipmentOperationCheck : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {


    private UIElementCommandBar uiElementCommandBar;

    private bool isDown = false;
    void Start()
    {
        uiElementCommandBar = transform.GetComponentInParent<UIElementCommandBar>();
    }
    void Update()
    {

        if (isDown)
        {
            
            HandleMouseDown(transform.name);

        }

    }
    public void OnPointerDown(PointerEventData eventData)

    { 
        Debug.Log("OnPointerDown");

        uiElementCommandBar.HideOther(transform.name);
        HandleMouseDown(transform.name);
        isDown = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        Debug.Log("OnPointerUp");
        HandleMouseUp();
        uiElementCommandBar.ShowAll();
        isDown = false;
    }
   
    private Operation currentOperation;
    private bool isHandlingMouseDrag = false;
    private Vector3 mousePosition;
    private Vector3 offsetClick;
    public GameObject rotationPoint;



    public void HandleMouseDown(string ButtonName)
    {
        EnableCamera(false);
      
        if (isDown)
        {
            HandleMouseDrag();
        }
        else
        {
            if (ButtonName.Equals("moveXZ"))
            {
                // 将3D视图中的上下移动变更为2D视图的前后左右移动
                MoveButtonPress();
                currentOperation = Operation.MoveXZ; //mainCamera.isOrthoGraphic ? Operation.MoveXZ : Operation.MoveY;

            }
            else if (ButtonName.Equals("moveY"))
            {
                // 将3D视图中的上下移动变更为2D视图的上下移动
                MoveButtonPress();
                currentOperation = Operation.MoveY;

            }
            else if (ButtonName.Equals("rotation"))
            {
                //Debug.Log("Rotation");
                //isHandlingMouseDrag = true;
                currentOperation = Operation.RotaionY;
                mousePosition = Input.mousePosition;
            }
        }


    }


    public void EnableCamera(bool isEnable)
    {
        if (Camera.main.GetComponent<CameraObjectController>() != null)
        {
            Camera.main.GetComponent<CameraObjectController>().SetEnable(isEnable);
        }

    }
    private void MoveButtonPress()
    {
        isHandlingMouseDrag = true;
        mousePosition = Input.mousePosition;

        Camera mainCamera = Camera.main;
        Vector3 vPosition = mainCamera.WorldToViewportPoint(UIElementCommandBar.instance.selectingObjectTransform.position);
        offsetClick = new Vector3(Screen.width * vPosition.x, Screen.height * vPosition.y, 0) - mousePosition;

        // Debug.Log(offsetClick);
        // rotateButton.gameObject.SetActive(false);
        
    }

  

    private void HandleMouseDrag()
    {
        if(UIElementCommandBar.instance.selectingObjectTransform == null)
        {

            return;
        }
        switch (currentOperation)
        {
            case Operation.MoveXZ:
                {
                    Camera mainCamera = Camera.main;
                    Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition + offsetClick);
                    float fLerp = (-Vector3.Dot(mouseRay.origin - UIElementCommandBar.instance.selectingObjectTransform.position, transform.up))
                                  / (Vector3.Dot(mouseRay.direction, transform.up));
                    Vector3 vIntersection = mouseRay.origin + fLerp * mouseRay.direction;
                    Vector3 vPosition = new Vector3(vIntersection.x, UIElementCommandBar.instance.selectingObjectTransform.position.y, vIntersection.z);
                    UIElementCommandBar.instance.selectingObjectTransform.position = vPosition;
                }
                break;
            case Operation.MoveY:
                {
                    Camera mainCamera = Camera.main;
                    Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition + offsetClick);
                    RaycastHit hit;
                    if (Physics.Raycast(mouseRay, out hit))//射线发出并碰撞到外壳，那么手臂就应该朝向碰撞点
                    {
                        float fLerp = (-Vector3.Dot(mouseRay.origin - UIElementCommandBar.instance.selectingObjectTransform.position, transform.forward))
                                   / (Vector3.Dot(mouseRay.direction, transform.forward));
                        Vector3 vIntersection = mouseRay.origin + fLerp * mouseRay.direction;
                        Vector3 vPosition = UIElementCommandBar.instance.selectingObjectTransform.position;
                        fLerp = Vector3.Dot(vIntersection - UIElementCommandBar.instance.selectingObjectTransform.position, transform.up);
                        vPosition += fLerp * transform.up;

                        UIElementCommandBar.instance.selectingObjectTransform.position = vPosition;
                    }
                    
                }
                break;
            case Operation.RotaionY:
                {
                    // Camera uiCamera = Camera.main.transform.Find("uiCamera").GetComponent<Camera>();
                    //Vector3 vOrigin = uiCamera.WorldToScreenPointc);
                    Vector3 vOrigin = RectTransformUtility.WorldToScreenPoint(null, rotationPoint.transform.position);


                    float fAngle = Vector3.Angle(Vector3.Normalize(mousePosition - vOrigin), Vector3.Normalize(Input.mousePosition - vOrigin));
                    Vector3 vNormal = Vector3.Cross(mousePosition - vOrigin, Input.mousePosition - vOrigin);
                    fAngle *= vNormal.z >= 0 ? 1 : -1;
                    Quaternion rRotation = Quaternion.AngleAxis(-fAngle, Vector3.up);
                    UIElementCommandBar.instance.selectingObjectTransform.rotation = rRotation * UIElementCommandBar.instance.selectingObjectTransform.rotation;
                    mousePosition = Input.mousePosition;
                }
                break;
        }

    }


    public void HandleMouseUp()
    {
        currentOperation = Operation.None;
        EnableCamera(true);
    }

    private enum Operation
    {
        None,
        MoveXZ,
        MoveY,
        RotaionY,
    }


}
