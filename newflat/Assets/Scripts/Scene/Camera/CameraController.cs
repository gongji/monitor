﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
using System.Threading;
public class CameraController : MonoBehaviour
{
    [Serializable]
    public class ControllerRanger
    {
        public float m_Distance = 100.0f;
        public float m_Multiples = 1.0f;
    }
    

    public float MinLocateDis;
    //移动标示
    protected const int MOVEMENT = (1 << 0);

    //旋转标示
    protected const int ROTATION = (1 << 1);

    //鼠标左键按下标示
    protected const int MOUSE_LEFT_DOWN = (1 << 2);

    //鼠标右键按下标示
    protected const int MOUSE_RIGHT_DOWN = (1 << 3);

    //向前标示
    protected const int MOVEMENT_FORWARD = (1 << 4);

    //向后标示
    protected const int MOVEMENT_BACKWARD = (1 << 5);

    //向左标示
    protected const int MOVEMENT_LEFT = (1 << 6);

    //向右标示
    protected const int MOVEMENT_RIGHT = (1 << 7);

    //向上标示
    protected const int MOVEMENT_UP = (1 << 8);

    //向下标示
    protected const int MOVEMENT_DOWN = (1 << 9);

    //方向键“前”标示
    protected const int INPUT_KEY_UP_ARROW = (1 << 10);

    //方向键“后”标示
    protected const int INPUT_KEY_DOWN_ARROW = (1 << 11);

    //方向键“左”标示
    protected const int INPUT_KEY_LEFT_ARROW = (1 << 12);

    //方向键“右”标示
    protected const int INPUT_KEY_RIGHT_ARROW = (1 << 13);

    protected const int DISENABLE_STATE = (1 << 14);

    public static CameraController instance { get; private set; }

    public static Camera currentCamera { get; private set; }


    public GameObject DocuMark;
    public int mask;

    /// <summary>飞行相机的标识</summary>
    public Transform m_Target;

    public float m_MinDistance = 5;
    public float m_MaxDistance = 600;
    public float m_CurrentDistance = 100;

    /// <summary>X方向的灵敏度</summary>
    public float sensitivityX = 1f;

    /// <summary>Y方向的灵敏度</summary>
    public float sensitivityY = 1f;

    /// <summary>平移的灵敏度</summary>
    public float translationSensitivity = 0.3f;

    /// <summary>向前的灵敏度</summary>
    public float forwardSensitivity = 0.1f;

    /// <summary>Y旋转的最小值</summary>
    public float minYDeg = -80;

    /// <summary>Y旋转的最大值</summary>
    public float maxYDeg = 80;

    /// <summary>平滑移动的时间</summary>
    public float smoothTime = 0.3f;

    /// <summary>最小能到达的高度</summary>
    public float camMinHeight = 1.0f;

    /// <summary>最大能到达的高度</summary>
    public float camMaxHeight = 600.0f;

    /// <summary>双击居中后最大的距离</summary>
    public float centerMaxDis = 300;

    /// <summary>有效区域</summary>
    public Rect canFlyRect = new Rect(-1200, -1200, 2400, 2400);

    public Transform positionReset;

    public GameObject enterArea;

    //移动速度
    [SerializeField]
    protected float m_MoveSpeed = 5.0f;

    //左键拖拽速度
    [SerializeField]
    protected float m_MouseDragSpeed = 5.0f;

    //旋转速度
    [SerializeField]
    protected float m_RotationSpeed = 3.0f;

    //
    [SerializeField]
    protected float m_MouseTolerance = 10.0f;

    private float m_ScaleSpeed = 5.0f;

    [SerializeField]
    private float m_speedScaleSlope = 0.06f;

    //是否检测窗口边缘
    [SerializeField]
    protected bool m_TestWindowsEdge = true;

    [SerializeField]
    protected Vector3 m_MinRange = new Vector3(float.MinValue, float.MinValue, float.MinValue);

    [SerializeField]
    protected Vector3 m_MaxRange = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

    // [SerializeField]
    //protected List<ControllerRanger> m_ControllerRanger = new List<ControllerRanger>();

    [SerializeField]
    protected float m_DefaultMultiple = 0.1f;

    /// <summary>x角度</summary>
    private float xDeg = 0.0f;

    /// <summary>y角度</summary>
    private float yDeg = 0.0f;

    /// <summary>动作时目标距离</summary>
    private float desDistance;

    /// <summary>姿态</summary>
    private Quaternion currentRotation;

    /// <summary>动作时目标姿态</summary>
    private Quaternion desiredRotation;

    private float forwardVelocity = 0;

    /// <summary>上一帧的姿态</summary>
    private Quaternion lastRotation;

    /// <summary>上一帧的距离</summary>
    private float lastDistance;

    /// <summary>上一帧的tag位置</summary>
    private Vector3 lastTagPos;

    /// <summary>上一帧x角度</summary>
    private float xlastDeg = 0.0f;

    /// <summary>上一帧y角度</summary>
    //private float ylastDeg = 0.0f;

    /// <summary>保存target的原始位置，用于复位</summary>
    private Vector3 oldtargetPos;

    private int m_nFlags;

    protected Vector4 m_vMouseOperation = Vector4.zero;

    protected Vector3 m_vSourcePosition = Vector3.zero;
    protected Quaternion m_vSourceRotation = Quaternion.identity;

    protected Vector3 m_vCameraPosition;
    protected Quaternion m_rCameraRotation;

    protected Matrix4x4 m_ProjectionTransform;

    protected System.Action m_HandleMouse;
    protected System.Action m_HandleKeyboard;

    public bool isMoved { get; private set; }

   

    protected bool m_IsLocating = false;
    protected int m_nEnterRoomCount = 0;

    void Awake()
    {
        instance = this;
        currentCamera = GetComponent<Camera>();
        mask = currentCamera.cullingMask;
    
  
        m_nFlags = 0;

       
    }


  
    // Use this for initialization
    void Start()
    {
        // 如果没有指定目标的话， 新建一个目标
        if (m_Target == null)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (this.transform.forward * m_CurrentDistance);
            m_Target = go.transform;
        }

        transform.LookAt(m_Target);
        Vector3 position = m_Target.position - (currentRotation * Vector3.forward * m_CurrentDistance);
        this.transform.position = position;
        desDistance = m_CurrentDistance;
        currentRotation = transform.rotation;
        desiredRotation = currentRotation;

        // 初始化角度
        xDeg = Vector3.Angle(Vector3.right, transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);

        oldtargetPos = m_Target.position;

        m_vCameraPosition = this.transform.position;
        m_rCameraRotation = this.transform.rotation;
   

        m_HandleMouse = this.HandleNormalMouse;
        m_HandleKeyboard = this.HandleNormalKeyboard;
        SetCharaterScale();
    }

    private void OnDestroy()
    {
        if(m_Target!=null)
        {
            GameObject.DestroyImmediate(m_Target.gameObject);
        }
    }

    /// <summary>
    /// 设定控制器的缩放
    /// </summary>
    private void SetCharaterScale()
    {
        CharacterController cc = GetComponent<CharacterController>();

        if (cc != null)
        {
            cc.stepOffset = 0;
            cc.radius = cc.radius * 0.1f;
            cc.height = cc.height * 0.1f;
            cc.center = cc.center * 0.1f;
            //台阶高度
            cc.stepOffset = cc.height / 2.5f;
            //皮肤厚度，一般为cc.radius的十分之一
           // 
            float ccRadius = float.Parse(cc.radius.ToString());
            if (ccRadius>0.1f)
            {
                cc.skinWidth = ccRadius * 0.1f;
            }
            else
            {
                cc.skinWidth = ccRadius;
                
            }
            if (cc.skinWidth < 0.06f)
            {
                cc.skinWidth = 0.06f;
            }
           
        }
    }

  

 

    public void SetCamerRotation()
    {
        m_rCameraRotation = this.transform.rotation;
    }

    

    void OnEnable()
    {
        this.ResetData();
    }

    void Update()
    {
        //Debug.Log("1:" + m_nFlags + ".2:" + DISENABLE_STATE + "3:" + (m_nFlags & DISENABLE_STATE));
    
        if ((m_nFlags & DISENABLE_STATE) != 0)
            return;
        //假如点击了主菜单那就不应该再响应其他的了
        //if (roundMainMenuCtrl.game_state > 0)
        //    return;

        this.HandleInput();
        //// 视角
        //if (input.GetViewButton())
        //    RotationView(input.GetViewDelta());

        //Move();
    }

    void OnTriggerEnter(Collider collider)
    {
    }

    void OnTriggerExit(Collider collider)
    {
        //SceneController.onCameraTriggerExit(collider);
    }

    public void Enable()
    {
        m_nFlags = 0;
    }

    public void Disable()
    {
        m_nFlags = 0;
        m_nFlags |= DISENABLE_STATE;
    }

    protected void HandleInput()
    {
       
            if (Input.GetKeyDown(KeyCode.W) )
            {
                m_nFlags |= MOVEMENT_FORWARD;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
               
                m_nFlags &= ~MOVEMENT_FORWARD;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                m_nFlags |= MOVEMENT_BACKWARD;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                
                m_nFlags &= ~MOVEMENT_BACKWARD;
            }
            if (Input.GetKeyDown(KeyCode.A) )
            {
                m_nFlags |= MOVEMENT_LEFT;
            }
            if (Input.GetKeyUp(KeyCode.A) )
            {
               
                m_nFlags &= ~MOVEMENT_LEFT;
            }

            if (Input.GetKeyDown(KeyCode.D) )
            {
                m_nFlags |= MOVEMENT_RIGHT;
            }
            if (Input.GetKeyUp(KeyCode.D) )
            {
               
                m_nFlags &= ~MOVEMENT_RIGHT;
            }

            if (Input.GetKeyDown(KeyCode.PageUp))
            {
                m_nFlags |= MOVEMENT_UP;
            }
            if (Input.GetKeyUp(KeyCode.PageUp))
            {
                m_nFlags &= ~MOVEMENT_UP;
            }

            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                m_nFlags |= MOVEMENT_DOWN;
            }
            if (Input.GetKeyUp(KeyCode.PageDown))
            {
                m_nFlags &= ~MOVEMENT_DOWN;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_nFlags |= INPUT_KEY_UP_ARROW;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) )
            {
               
                m_nFlags &= ~INPUT_KEY_UP_ARROW;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                m_nFlags |= INPUT_KEY_DOWN_ARROW;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
               
                m_nFlags &= ~INPUT_KEY_DOWN_ARROW;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) )
            {
                m_nFlags |= INPUT_KEY_LEFT_ARROW;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) )
            {
                
                m_nFlags &= ~INPUT_KEY_LEFT_ARROW;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) )
            {
                m_nFlags |= INPUT_KEY_RIGHT_ARROW;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow) )
            {
              
                m_nFlags &= ~INPUT_KEY_RIGHT_ARROW;
            }
        

        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            m_vMouseOperation = Vector4.zero;
            m_nFlags |= MOUSE_LEFT_DOWN;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_nFlags &= ~MOUSE_LEFT_DOWN;
        }

        if (Input.GetMouseButtonDown(1))
        {
            m_nFlags |= MOUSE_RIGHT_DOWN;
        }
        if (Input.GetMouseButtonUp(1))
        {
            m_nFlags &= ~MOUSE_RIGHT_DOWN;
        }

        if ( m_HandleMouse != null)
            m_HandleMouse();

        //if (!UICamera.selectedObject)
        if (m_HandleKeyboard != null)
        {
            m_HandleKeyboard();
        }
            

        if ((m_nFlags & MOVEMENT) != 0)
        {
            this.isMoved = true;
            // Vector3 temp=new Vector3(currentCamera.transform.position.x,currentCamera.transform.position.y,currentCamera.transform.position.z);

            move(m_vCameraPosition.x, m_vCameraPosition.y, m_vCameraPosition.z);
        }

        if ((m_nFlags & ROTATION) != 0)
        {
            //this.transform.rotation = m_rCameraRotation;

            rotate(m_rCameraRotation.x, m_rCameraRotation.y, m_rCameraRotation.z, m_rCameraRotation.w);
        }

        m_nFlags &= ~(MOVEMENT | ROTATION);
    }

    /// <summary>
    /// 拖动鼠标
    /// </summary>

    protected void HandleNormalMouse()
    {
        if ((m_nFlags & MOUSE_RIGHT_DOWN) != 0)
        {
            float dx = Input.GetAxis("Mouse X"), dy = Input.GetAxis("Mouse Y");
            if (dx == 0.0f && dy == 0.0f)
            {
                dx = m_vMouseOperation.z * 5;
                dy = m_vMouseOperation.w * 5;
            }
            else
            {
                m_vMouseOperation.z = 0.0f;//dx;
                m_vMouseOperation.w = 0.0f;//dy;
            }

            Vector3 vEulerAngles = m_rCameraRotation.eulerAngles;
            //防止万向锁
            if (vEulerAngles.x > 180.0f)
                vEulerAngles.x -= 360.0f;
            vEulerAngles.x -= m_RotationSpeed * dy;
            if (vEulerAngles.x > 60.0f)
                vEulerAngles.x = 60.0f;
            else if (vEulerAngles.x < -60.0f)
                vEulerAngles.x = -60.0f;
            vEulerAngles.y += m_RotationSpeed * dx;

            m_nFlags |= ROTATION;
            m_rCameraRotation = Quaternion.Euler(vEulerAngles);
        }

        if ((m_nFlags & MOUSE_LEFT_DOWN) != 0)
        {
            float dx = Input.GetAxis("Mouse X"), dy = Input.GetAxis("Mouse Y");
            if (dx == 0.0f && dy == 0.0f)
            {
                dx = m_vMouseOperation.x * 5;
                dy = m_vMouseOperation.y * 5;
            }
            else
            {
                m_vMouseOperation.x = 0.0f;//dx;
                m_vMouseOperation.y = 0.0f;//dy;
            }
            this.Translation(m_MouseDragSpeed * new Vector2(-dx, dy));
            m_nFlags |= MOVEMENT;
        }

        if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0)
        {
            Vector3 vForward = new Vector3(0.0f, 0.0f, Input.GetAxis("Mouse ScrollWheel"));
            vForward.Normalize();
            this.Move(this.CalculateWalkSpeed() * vForward);
            m_nFlags |= MOVEMENT;
        }

        if ((m_nFlags & (MOVEMENT | ROTATION)) == 0 && m_TestWindowsEdge)
        {
            Vector3 vDirection = Vector3.zero;
            if (Input.mousePosition.x <= m_MouseTolerance)
                vDirection.x -= 1.0f;
            if (Input.mousePosition.x >= Screen.width - m_MouseTolerance)
                vDirection.x += 1.0f;
            if (Input.mousePosition.y <= m_MouseTolerance)
                vDirection.y -= 1.0f;
            if (Input.mousePosition.y >= Screen.height - m_MouseTolerance)
                vDirection.y += 1.0f;
            if (Vector3.Dot(vDirection, vDirection) != 0.0f)
            {
                this.Move(this.CalculateWalkSpeed() * vDirection);
                m_nFlags |= MOVEMENT;
            }
        }
    }

    /// <summary>
    /// 滚轴
    /// </summary>
    protected void HandleTopViewMouse()
    {
        //if ((m_nFlags & (int)Flag.Mouse2Down) != 0)
        //{
        //    Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        //    if (m_vLastMouse != mousePos)
        //    {
        //        this.TranslationTopCam(m_vLastMouse - mousePos);
        //        m_vLastMouse = mousePos;
        //        m_nFlags |= ROTATION;
        //    }
        //}

        if ((m_nFlags & MOUSE_LEFT_DOWN) != 0)
        {
            float dx = Input.GetAxis("Mouse X");
            float dy = Input.GetAxis("Mouse Y");
            if (dx != 0.0f || dy != 0.0f)
            {
                float speed = m_MouseDragSpeed;
                //if (SceneController.currentMode == SceneController.layerMode)
                //{
                //    speed = speed * Constant.GlobalUnit * 4;
                //}
                this.TranslationTopCam(speed * new Vector2(-dx, dy));
                m_nFlags |= MOVEMENT;
            }
        }

        //俯视图下滚轴滚动
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            
        }
    }

    /// <summary>
    /// 
    /// </summary>

    protected void HandleNormalKeyboard()
    {
        //m_nFlags 同步服务器返回的值
        Vector3 vDirection = Vector3.zero;
        if ((m_nFlags & MOVEMENT_LEFT) != 0)
            vDirection.x -= 1.0f;
        if ((m_nFlags & MOVEMENT_RIGHT) != 0)
            vDirection.x += 1.0f;
        if ((m_nFlags & MOVEMENT_UP) != 0)
            vDirection.y += 1.0f;
        if ((m_nFlags & MOVEMENT_DOWN) != 0)
            vDirection.y -= 1.0f;
        if ((m_nFlags & (MOVEMENT_FORWARD)) != 0)
            vDirection.z += 1.0f;
        if ((m_nFlags & (MOVEMENT_BACKWARD)) != 0)
            vDirection.z -= 1.0f;
        if (Vector3.Dot(vDirection, vDirection) != 0.0f)
        {
            this.Move(this.CalculateWalkSpeed() * vDirection);
            m_nFlags |= MOVEMENT;
        }

        if ((m_nFlags & ROTATION) == 0)
        {
            Vector3 vEulerAngles = m_rCameraRotation.eulerAngles;
            float dx = 0.0f;
            float dy = 0.0f;
            if ((m_nFlags & INPUT_KEY_LEFT_ARROW) != 0)
                dx -= 1.0f;
            if ((m_nFlags & INPUT_KEY_RIGHT_ARROW) != 0)
                dx += 1.0f;
            if ((m_nFlags & INPUT_KEY_UP_ARROW) != 0)
                dy -= 1.0f;
            if ((m_nFlags & INPUT_KEY_DOWN_ARROW) != 0)
                dy += 1.0f;
            if (dx != 0.0f || dy != 0.0f)
            {
                vEulerAngles.y += m_RotationSpeed * dx;
                vEulerAngles.x += m_RotationSpeed * dy;
                m_rCameraRotation = Quaternion.Euler(vEulerAngles);
                m_nFlags |= ROTATION;
            }
            vEulerAngles.x += m_RotationSpeed * dy;
            if (vEulerAngles.x > 180.0f)
                vEulerAngles.x -= 360.0f;
            if (vEulerAngles.x > 60.0f)
                vEulerAngles.x = 60.0f;
            else if (vEulerAngles.x < -60.0f)
                vEulerAngles.x = -60.0f;
            m_rCameraRotation = Quaternion.Euler(vEulerAngles);
            m_nFlags |= ROTATION;
        }
    }

    protected void HandleTopViewKeyboard()
    {
    }

    // 获取上一帧的各个参数
    void GetLastInfo()
    {
        lastRotation = transform.rotation;   // 上一帧的姿态
        xlastDeg = xDeg;
        // ylastDeg = yDeg;
        lastDistance = Vector3.Distance(transform.position, m_Target.position);        // 上一帧的距离
        lastTagPos = m_Target.position;        // 上一帧的tag位置
    }

    // 设为上一帧的参数
    void ResetLastInfo()
    {
        desDistance = lastDistance;
        desiredRotation = lastRotation;
        transform.rotation = lastRotation;
        xDeg = xlastDeg;
        yDeg = Vector3.Angle(Vector3.up, transform.up);
        yDeg = ClampAngle(yDeg, minYDeg, maxYDeg);
        m_Target.position = lastTagPos;
    }

    // 最大最小值
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -180)
            angle += 360;
        if (angle > 180)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    // 视角转动
    void RotationView(Vector2 addV)
    {
        xDeg += addV.x * sensitivityX;
        yDeg -= addV.y * sensitivityY;
        yDeg = ClampAngle(yDeg, minYDeg, maxYDeg);
        desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
    }

    // 向前
    public void MoveForward(float value)
    {
        Vector3 vForward = m_rCameraRotation * Vector3.forward;
        m_vCameraPosition += value * vForward * forwardSensitivity * Time.deltaTime;
        this.TestPositionRange();
        //desDistance += add * forwardSensitivity * Mathf.Lerp(1.0f, 10.0f, (lerpDis) / (maxDistance - minDistance)) * Time.deltaTime;
    }

  


    protected void Move(Vector3 value)
    {
        m_vCameraPosition += this.transform.right * value.x + this.transform.up * value.y + this.transform.forward * value.z;
        this.TestPositionRange();
    }

    // 平移
    void Translation(Vector2 value)
    {
        float fPosY = m_vCameraPosition.y < camMinHeight ? camMinHeight : m_vCameraPosition.y > camMaxHeight ? camMaxHeight : m_vCameraPosition.y;

        Vector3 vRight = m_rCameraRotation * Vector3.right, vUp = m_rCameraRotation * Vector3.up;
        m_vCameraPosition += vRight * value.x * translationSensitivity * (1.0f / Screen.width) * fPosY;
        m_vCameraPosition -= vUp * value.y * translationSensitivity * (1.0f / Screen.height) * fPosY;
        this.TestPositionRange();

        m_Target.rotation = transform.rotation;
        m_Target.Translate(Vector3.right * value.x * translationSensitivity * (1.0f / Screen.width) * fPosY);
        m_Target.Translate(-transform.up * value.y * translationSensitivity * (1.0f / Screen.height) * fPosY, Space.World);
    }

    //平移俯视视角相机
    void TranslationTopCam(Vector2 addT)
    {
        float fPosY = m_vCameraPosition.y < camMinHeight ? camMinHeight : m_vCameraPosition.y > camMaxHeight ? camMaxHeight : m_vCameraPosition.y;
        float fSensitivity = 0.7f;

        m_vCameraPosition += Vector3.right * addT.x * fSensitivity * (1.0f / Screen.width) * fPosY;
        m_vCameraPosition += -transform.up * addT.y * fSensitivity * (1.0f / Screen.height) * fPosY;
        this.TestPositionRange();

        m_Target.transform.Translate(Vector3.right * addT.x * fSensitivity * (1.0f / Screen.width) * fPosY);
        m_Target.transform.Translate(-transform.up * addT.y * fSensitivity * (1.0f / Screen.height) * fPosY, Space.World);
    }

    // 移动
    void Move()
    {
        // 视角的平滑移动
        currentRotation = transform.rotation;
        // 避免网路延迟引起晃动，去掉差值
        currentRotation = desiredRotation;// Quaternion.Lerp(currentRotation, desiredRotation, 0.25f);
                                          // 修改：由rotate函数实现
                                          //transform.rotation = currentRotation;
                                          //if (Option.isController && transform.rotation != currentRotation)
                                          //{
                                          //    //Debug.LogWarning(DateTime.Now.ToString() + " send position: " + currentRotation);
                                          //    Messenger.MultiClientSynchronize.sendCameraRotate(currentRotation.x, currentRotation.y, currentRotation.z, currentRotation.w);
                                          //}
                                          // 修改：需求变更为任何机器都能控制所以取消控制判断

        rotate(currentRotation.x, currentRotation.y, currentRotation.z, currentRotation.w);
        // 修改- 距离大于最大值和小于最小值时的判断
        if (desDistance < m_MinDistance)
        {
            //target.position = target.position + currentRotation * Vector3.forward * (minDistance - desDistance);
            //currDistance = currDistance + (minDistance - desDistance);
            desDistance = m_MinDistance;
        }
        else if (desDistance > m_MaxDistance)
        {
            desDistance = m_MaxDistance;
        }
        // 距离平滑
        m_CurrentDistance = desDistance;//Mathf.SmoothDamp(currDistance, desDistance, ref forwardVelocity, smoothTime);
        //currDistance = Mathf.Lerp(currDistance, desDistance,Time.deltaTime * zoomDampening);
        // tag的位置
        DealTagSide();
        // 计算位置
        Vector3 position = m_Target.position - (currentRotation * Vector3.forward * m_CurrentDistance);
        if (position.y < camMinHeight)
        {
            ResetLastInfo();// 设为上一帧的参数
        }
        else
        {
            // 修改：由move函数实现
            //transform.position = position;
            //if (Option.isController && transform.position != position)
            //{
            //    //Debug.LogWarning(DateTime.Now.ToString() + " send position: " + position);
            //    Messenger.MultiClientSynchronize.sendCameraMove(position.x, position.y, position.z);
            //}
            // 修改：需求变更为任何机器都能控制所以取消控制判断
            move(position.x, position.y, position.z);
        }
        //control.Move(position - transform.position);
    }

    // 重设数据
    public void ResetData()
    {
        if (m_Target == null)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (transform.forward * m_CurrentDistance);
            m_Target = go.transform;
        }

        m_CurrentDistance = Vector3.Distance(this.transform.position, m_Target.position);
        //target.position = transform.position + (transform.forward * currDistance);
        transform.LookAt(m_Target);
        desDistance = m_CurrentDistance;
        currentRotation = transform.rotation;
        desiredRotation = currentRotation;
        // 初始化角度
        xDeg = Vector3.Angle(Vector3.right, transform.right);  //下面的是原来的代码，不知道为什么要加负号
        yDeg = Vector3.Angle(Vector3.up, transform.up);
        //xDeg = -Vector3.Angle(Vector3.right, transform.right);
        //yDeg = -Vector3.Angle(Vector3.up, transform.up);

    }

    // 处理Tag边界
    public void DealTagSide()
    {
        Vector3 tagPos = m_Target.position;
        // y方向
        if (m_Target.position.y < camMinHeight + m_MinDistance)
        {
            tagPos.y = camMinHeight + m_MinDistance;
            m_Target.position = tagPos;
        }
        else if (m_Target.position.y > camMaxHeight)
        {
            tagPos.y = camMaxHeight;
            m_Target.position = tagPos;
        }

        // x方向
        if (m_Target.position.x < canFlyRect.x)
        {
            tagPos.x = canFlyRect.x;
            m_Target.position = tagPos;
        }
        else if (m_Target.position.x > canFlyRect.x + canFlyRect.width)
        {
            tagPos.x = canFlyRect.x + canFlyRect.width;
            m_Target.position = tagPos;
        }
        // z方向
        if (m_Target.position.z < canFlyRect.y)
        {
            tagPos.z = canFlyRect.y;
            m_Target.position = tagPos;
        }
        else if (m_Target.position.z > canFlyRect.y + canFlyRect.height)
        {
            tagPos.z = canFlyRect.y + canFlyRect.height;
            m_Target.position = tagPos;
        }
    }

    // 跳转到点
    public void JumpToLookAtPos(Vector3 pos, float fDistance = 5.0f)
    {
        transform.LookAt(pos);
        transform.forward = pos - transform.position;

        m_Target.position = pos;
        //float distance = Vector3.Distance(transform.position,Object3DElement.selectingObject[0].transform.position) * 0.5f;
        //m_CurrentDistance = distance;
        //if (distance > centerMaxDis)
        //    distance = centerMaxDis;
        
        //desDistance = distance * 0.8f;
        //position = transform.position;
        //rotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;

        yDeg = transform.rotation.eulerAngles.x;
        xDeg = transform.rotation.eulerAngles.y;
        yDeg = ClampAngle(yDeg, minYDeg, maxYDeg);

        this.transform.position = pos - this.transform.forward * fDistance;// +transform.position * 0.5f;
        m_vCameraPosition = this.transform.position;
        m_rCameraRotation = this.transform.rotation;
        this.TestPositionRange();
    }

    public void resetP()
    {
        this.transform.position = positionReset.position;// +transform.position * 0.5f;
        this.transform.rotation = positionReset.rotation;
        m_vCameraPosition = this.transform.position;
        m_rCameraRotation = this.transform.rotation;
        this.TestPositionRange();
    }

    public void UpdatePosition(Transform p)
    {
        this.transform.position = p.position;// +transform.position * 0.5f;
        this.transform.rotation = p.rotation;
        m_vCameraPosition = this.transform.position;
        m_rCameraRotation = this.transform.rotation;
    }

    /// <summary>
    /// 2d和3d的切换
    /// </summary>

    

    void animationUpdate(bool go)
    {
        ResetData();
    }

    void animationEnd(string f)
    {
        //Debug.Log("end : " + f);
        ResetData();
    }

    public void SetTargetPosY(float y)
    {
        m_Target.position = new Vector3(m_Target.position.x, y, m_Target.position.z);
    }

    public void ResetTargetPos()
    {
        m_Target.position = oldtargetPos;
    }

    //protected void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    //{
    //    SceneController.instance.onRenderImage(sourceTexture, destTexture);
    //}

    public void move(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
        m_vCameraPosition = transform.position;
        this.TestPositionRange();
    }

    public void rotate(float x, float y, float z, float w)
    {
        transform.rotation = new Quaternion(x, y, z, w);
        m_rCameraRotation = transform.rotation;
    }

    /// <summary>恢复相机遮罩层 </summary>
    public void restoreMask()
    {
        currentCamera.cullingMask = mask;
        rayCast(c => true);
    }

    public void rayCast(Func<Camera, bool> callback)
    {
        //if (callback(secCamera.GetComponent<Camera>()))
        //{
        //    callback(currentCamera);
        //}
    }

    /// <summary>
    /// 检测屏幕的移动边界
    /// </summary>
    public void TestPositionRange()
    {
        return;
        CheckBound();
    }

    private void DefaultConstrain()
    {
        if (m_vCameraPosition.x < m_MinRange.x)
            m_vCameraPosition.x = m_MinRange.x;
        if (m_vCameraPosition.y < m_MinRange.y)
            m_vCameraPosition.y = m_MinRange.y;
        if (m_vCameraPosition.z < m_MinRange.z)
            m_vCameraPosition.z = m_MinRange.z;

        if (m_vCameraPosition.x > m_MaxRange.x)
            m_vCameraPosition.x = m_MaxRange.x;
        if (m_vCameraPosition.y > m_MaxRange.y)
            m_vCameraPosition.y = m_MaxRange.y;
        if (m_vCameraPosition.z > m_MaxRange.z)
            m_vCameraPosition.z = m_MaxRange.z;
    }

    private float CamConstrainScale = 1.0f;

    private void CheckBound()
    {
        return;
        //Transform t = MapUtil.GetAssetBox();

        //if (t != null)
        //{
        //    BoxCollider collider = t.GetComponent<BoxCollider>();
        //    if (collider)
        //    {
        //        // Bounds b = collider.bounds;

        //        Vector3 localScale = new Vector3(Mathf.Abs(t.localScale.x), Mathf.Abs(t.localScale.y), Mathf.Abs(t.localScale.z));
        //        Vector3 max = t.position + localScale / 2;
        //        Vector3 min = t.position - (localScale / 2);

        //        //max.x *= CamConstrainScale;
        //        //max.z *= CamConstrainScale;

        //        max *= CamConstrainScale;
        //        min *= CamConstrainScale;
        //        Vector3 size = max - min;
        //        size = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.z));
        //        Bounds b = new Bounds(t.position, size);

        //        //min.x *= CamConstrainScale;
        //        //min.z *= CamConstrainScale;

        //        //max = t.position + max;
        //        //min = t.position + min;

        //        //Debug.LogError(max + "____" + min + "____" + t.position);

        //        ConstrainCamera(b.min, b.max);
        //    }
        //    else
        //    {
        //        //Debug.LogError("地形碰撞盒没有找到");
        //        DefaultConstrain();
        //    }
        //}
        //else
        //{
        //    //Debug.LogError("地形碰撞盒gameobject没有找到");
        //    DefaultConstrain();
        //}
    }

    private float MulScale(float origin, float scaleFactor)
    {
        float result = 0.0f;
        if (origin < 0.0f)
        {
            
        }


        return result;
    }

    private void ConstrainCamera(Vector3 min, Vector3 max)
    {
        if (m_vCameraPosition.x < min.x)
            m_vCameraPosition.x = min.x;
        if (m_vCameraPosition.y < min.y)
            m_vCameraPosition.y = min.y;
        if (m_vCameraPosition.z < min.z)
            m_vCameraPosition.z = min.z;
        if (m_vCameraPosition.x > max.x)
            m_vCameraPosition.x = max.x;
        if (m_vCameraPosition.y > max.y)
            m_vCameraPosition.y = max.y;
        if (m_vCameraPosition.z > max.z)
            m_vCameraPosition.z = max.z;
    }

    public Vector3 ClampCalc(Vector3 pos)
    {
        if (pos.x < m_MinRange.x)
            pos.x = m_MinRange.x;
        if (pos.y < m_MinRange.y)
            pos.y = m_MinRange.y;
        if (pos.z < m_MinRange.z)
            pos.z = m_MinRange.z;
        return pos;
    }

    [SerializeField]
    public LayerMask SpeedControlMask = -1;

    protected float CalculateWalkSpeed()
    {

        #region 以前版本代码
        //if (m_nEnterRoomCount > 0)
        //    return m_MoveSpeed * m_DefaultMultiple; 

        //int nIndex = -1;
        // float fSpeed = m_MoveSpeed, fDist = Vector3.Distance(this.transform.position, Vector3.zero);

        //for (int i = 0; i < m_ControllerRanger.Count - 1; i++)
        //{
        //    if (fDist <= m_ControllerRanger[i].m_Distance)
        //    {
        //        nIndex = i;
        //        break;
        //    }
        //}
        //if (nIndex == 0)
        //{
        //    fSpeed *= m_ControllerRanger[0].m_Multiples;
        //}
        //else if (nIndex == -1)
        //{
        //    fSpeed *= m_ControllerRanger[m_ControllerRanger.Count - 1].m_Multiples;
        //}
        //else
        //{
        //    float fLerp = (fDist - m_ControllerRanger[nIndex].m_Distance) / Mathf.Abs(m_ControllerRanger[nIndex].m_Distance - m_ControllerRanger[nIndex + 1].m_Distance);
        //    fSpeed *= Mathf.Lerp(m_ControllerRanger[nIndex].m_Multiples, m_ControllerRanger[nIndex + 1].m_Multiples, fLerp);
        //} 
        #endregion
        #region
        //float multiples=1.0f;
        //float fSpeed = 0f;
        //Vector2 beChecktempPos=new Vector2(currentCamera.transform.position.x,currentCamera.transform.position.z);
        //bool isinBoundBox = CheckPointInPoly2(Bound1Box, beChecktempPos);
        //if (isinBoundBox)
        //{
        //    if (currentCamera.transform.position.y > 100 && currentCamera.transform.position.y<200)
        //    {
        //        multiples = _rangeNum2;
        //    }
        //    else if (currentCamera.transform.position.y > 30)
        //    {
        //        multiples = _rangeNum3;
        //    }
        //    else
        //    {
        //        multiples = _rangeNum4;
        //    }
        //}
        //else
        //    multiples = _rangeNum1;
        #endregion
        Ray ray = currentCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        float dis = 80;
        if (Physics.Raycast(ray, out hit, 1000, SpeedControlMask.value))
        {
            dis = Vector3.Distance(hit.point, this.transform.position);
            //Debug.Log("dis" + dis);

            if (dis < 0.5f)
                dis = 0.5f;

           // Debug.Log("Vector3.Distance" + Vector3.Distance(Vector3.zero, this.transform.position));
            //if (Vector3.Distance(Vector3.zero, this.transform.position) > 80 && SceneSystemMsg.Instance.selectSystem == -1)
            //    UIMap.instance.show();
             else if (dis > 200)
                dis = 200;
        }

        m_ScaleSpeed = dis * m_speedScaleSlope;
        return m_ScaleSpeed;
    }

    public Transform target { get { return m_Target; } }

    // 临时的解决方案（切换对摄像机的操控）
    public void SwitchCtrl(System.Action mouseHandler, System.Action keybardHandler)
    {
        if (mouseHandler != null)
        {
            m_HandleMouse = mouseHandler;
        }
        else
        {
            if (!currentCamera.orthographic)
            {
                m_HandleMouse = this.HandleNormalMouse;
            }
            else
            {
                m_HandleMouse = this.HandleTopViewMouse;
            }

            m_vCameraPosition = this.transform.position;
            m_rCameraRotation = this.transform.rotation;
        }

        if (keybardHandler != null)
        {
            m_HandleKeyboard = keybardHandler;
        }
        else
        {
            if (!currentCamera.orthographic)
            {
                m_HandleKeyboard = this.HandleNormalKeyboard;
            }
            else
            {
                m_HandleKeyboard = this.HandleTopViewKeyboard;
            }

            m_vCameraPosition = this.transform.position;
            m_rCameraRotation = this.transform.rotation;
        }
    }

    /// 第一人称碰撞检测。用于所在区域提示
    /// </summary>
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
       
    }

    public void SetControlMoveSpeed(float param)
    {
        m_MoveSpeed = param;
    }

    public void SetControlRotationSpeed(float param)
    {
        m_RotationSpeed = param;
    }

    void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
        {
            //Debug.Log("OnApplicationFocus");
        }
        else
        {
           // Debug.Log("Exit ApplicationFocus");
        }

        //if (SceneSystemMsg.Instance.selectSystem == -1)
        //    Enable();

    }

    public int flags { set { m_nFlags = value; } get { return m_nFlags; } }

    public float forward { get { return this.CalculateWalkSpeed(); } }

    public float sensitiveX { set { /** m_RotationSpeed = value;**/ } get { return m_RotationSpeed; } }

    public float sensitiveY { set { /** m_RotationSpeed = value;**/ } get { return m_RotationSpeed; } }

    public int enterRoomCount { set { m_nEnterRoomCount = value; } get { return m_nEnterRoomCount; } }
}
/*********************************************************************************************************
 * 创建日期:2012-2-8
 * 作者:zpba
 * 功能：在ipad中的飞行相机
 *    1.转视角
 *    2.移动（向前+平移）
 *********************************************************************************************************/