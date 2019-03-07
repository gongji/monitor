#define ENABLE_DELAY_CLICK
using Core.Common.Logging;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseCheckUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    private static ILog log = LogManagers.GetLogger("MouseCheckUI");

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
                OnDoubleClick();
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
                OnClick();
                m_bDelayClick = false;
            }
        }
#endif

    }


    private bool isMouseOver = false;
   public  void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void  OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }


    private  void OnDoubleClick()
    {
        if(isMouseOver)
        {
            Debug.Log("OnDoubleClick");
        }
    }

    private void OnClick()
    {
        if(isMouseOver)
        {
            Debug.Log("OnClick");
        }
       
    }

}
