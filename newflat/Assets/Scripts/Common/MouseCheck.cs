#define ENABLE_DELAY_CLICK
using Core.Common.Logging;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseCheck : MonoBehaviour
{ 
    private static ILog log = LogManagers.GetLogger("MouseType");

    //鼠标左键是否发生双击事件
    public static bool DOUBLE_CLICK =false;

    //鼠标左键是否发生单击事件
    public static bool CLICK =false;

    //鼠标左键是否按住状态
    public static bool LONG_PRESS =false;

    //鼠标悬停
    public static bool MOUSE_OVER =false;

    //鼠标离开
    public static bool MOUSE_OUT =false;

    //鼠标左键点击次数
    protected int m_nClickCount = 0;

    //上次鼠标左键点击时间
    protected float m_fLastClick = 0.0f;

    //上次鼠标左键点击坐标
    protected Vector3 m_vLastMousePosition;

    //判断点击间隔
    protected float m_fClickDuration = 0.3f;

    //判断按住间隔
    protected float m_fPressDuration = 0.5f;

    //是否禁用鼠标事件检测
    public bool isDisable = false;

    public static Vector3 doubleHitPoint;

    public static Vector3 clickHitPoint;
    public static Transform doubleTransform;
    public static Transform clickHitTransform;

    #if ENABLE_DELAY_CLICK
    protected bool m_bDelayClick = false;
    #endif

    void Update()
    {
        CLICK = DOUBLE_CLICK = false;
        if ((EventSystem.current!=null && EventSystem.current.IsPointerOverGameObject()))
        {
            clickHitTransform = null;
            return;
        }
       
        if (isDisable)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.realtimeSinceStartup - m_fLastClick > m_fClickDuration)
                m_nClickCount = 0;
            if (m_nClickCount == 0)
                m_vLastMousePosition = Input.mousePosition;

            m_fLastClick = Time.realtimeSinceStartup;
        }

        if (Input.GetMouseButton(0) && Time.realtimeSinceStartup - m_fLastClick >= m_fPressDuration)
            LONG_PRESS = true;
        else
            LONG_PRESS = false;

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 v = m_vLastMousePosition - Input.mousePosition;
			
            if (Time.realtimeSinceStartup - m_fLastClick < m_fPressDuration)
            {
                if (Vector3.Dot(v, v) <= 25.0f)
                {
                    ++m_nClickCount;
                }
                else
                {
                    m_nClickCount = 1;
                }
                    
            }

           
            if (m_nClickCount == 1)
            {
#if ENABLE_DELAY_CLICK
                m_bDelayClick = true;
#else
                CLICK = true;
#endif
            }
            else if (m_nClickCount >= 2)
            {
                DOUBLE_CLICK = true;
#if ENABLE_DELAY_CLICK
                m_bDelayClick = false;
#endif
            }
           

            m_fLastClick = Time.realtimeSinceStartup;
           
        }

#if ENABLE_DELAY_CLICK
        if (m_bDelayClick)
        {
            if (Time.realtimeSinceStartup - m_fLastClick > m_fClickDuration)
            {
                CLICK = true;
                m_bDelayClick = false;
            }
        }
#endif

        CheckHitTransform(Input.mousePosition);
    }

    /// <summary>
    /// 获取检测鼠标单击或者的对象
    /// </summary>
    /// <param name="postion">鼠标点击的位置</param>
    private void CheckHitTransform(Vector3 postion)
    {
       
        Ray ray = Camera.main.ScreenPointToRay(postion);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue,1 << Constant.SceneLayer | 1 << Constant.EquipmentLayer))
        {
           // Debug.Log(hit.transform);
            clickHitTransform = hit.transform;
            clickHitPoint = hit.point;
        }
    }
}
