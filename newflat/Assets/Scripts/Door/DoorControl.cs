using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour
{
    public enum Type
    {
        Rotation,
        Translate,
    }

    protected enum State
    {
        Open,
        Opening,
        Close,
        Closing,
    }

    public enum RotationAxis
    {
        x,
        y,
        z,
    }

    public enum RotationDirection
    {
        Clockwise,
        CounterClockwise,
    }

    public Type m_Type = Type.Rotation;

    public RotationAxis m_RotationAxis = RotationAxis.x;
    public RotationDirection m_RotationDirection = RotationDirection.Clockwise;
    public int m_RotationAngle = 90;

    public Transform m_TranslatePosition;
    
    //public float m_TranslateDistances = 1.0f;
    public float m_TotalInterval = 1.0f;

    protected State m_State = State.Close;
    protected float m_fRunningTime = 0.0f;

    protected Quaternion m_SrcRotation;
    protected Quaternion m_DstRotation;

    protected Vector3 m_vSrcPosition;
    protected Vector3 m_vDstPosition;

    void Start()
    {
        m_SrcRotation = this.transform.localRotation;
        m_vSrcPosition = this.transform.position;

        string[]  names = transform.name.Split('_');
    
        //旋转类型
        string type = names[0];

        if(!type.ToLower().Equals("l") && !type.ToLower().Equals("t"))
        {
            GameObject.DestroyImmediate(this);
            return;
        }
        if (type.ToLower().Equals("l"))
        {
            m_Type = Type.Translate;
        }


        // strs = name.Split('_');
        //旋转轴和增减
       string  endStr = names[1];


        this.Recalc(endStr);

        m_State = State.Close;
    }

    private IEnumerator OpenDoor()
    {
        m_fRunningTime = 0.0f;
        while (m_fRunningTime < m_TotalInterval)
        {
            m_fRunningTime += Time.deltaTime;
            float fLerp = m_fRunningTime / m_TotalInterval;
            if (m_Type == Type.Rotation)
            {
                this.transform.localRotation = Quaternion.Lerp(m_SrcRotation, m_DstRotation, fLerp);
            }

            else
            {
                this.transform.position = Vector3.Lerp(m_vSrcPosition, m_vDstPosition, fLerp);
            }
            yield return 0;
        }

    }

    private IEnumerator CloseDoor()
    {
        m_fRunningTime = m_TotalInterval;
        while (m_fRunningTime >0)
        {
            m_fRunningTime -= Time.deltaTime;
            float fLerp = m_fRunningTime / m_TotalInterval;
            if (m_Type == Type.Rotation)
            {
                this.transform.localRotation = Quaternion.Lerp(m_SrcRotation, m_DstRotation, fLerp);
            }
                
            else
            {
                this.transform.position = Vector3.Lerp(m_vSrcPosition, m_vDstPosition, fLerp);
            }

            yield return 0;
        }
       
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Open();
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            Close();
        }
    }

    public void Open()
    {
        if (m_State == State.Open)
        {
            return ;
        }
        m_State = State.Open;
        StartCoroutine(OpenDoor());
    }

    public void Close()
    {
        if (m_State == State.Close)
        {
            return;
        }

        m_State = State.Close;
        StartCoroutine(CloseDoor());
    }

    protected void Recalc(string endStr)
    {
        if (endStr.Length<2)
        {
            return;
        }
        //旋转轴
        string axisStr = endStr.Substring(1, 1).ToLower();
        //增加还是减小角度
        string addDecrease = endStr.Substring(0, 1).ToLower();

        float defaultMove = 1;
        float defaultAngle = 90;
        if (addDecrease == "s")
        {
            defaultAngle = -90;
            defaultMove =  -1;
        }
        if (m_Type == Type.Rotation)
        {
            //设置旋转轴
            Vector3 vRotationAxis = Vector3.right;
            if (axisStr == RotationAxis.y.ToString())
                vRotationAxis = Vector3.up;
            else if (axisStr == RotationAxis.z.ToString())
                vRotationAxis = Vector3.forward;

            //旋转
            m_DstRotation = m_SrcRotation * Quaternion.AngleAxis(defaultAngle, vRotationAxis);
        }
        else
        {
            Bounds bound = GetComponent<MeshRenderer>().bounds;
            if (axisStr == RotationAxis.y.ToString())
            {
                
                 m_vDstPosition = m_vSrcPosition + new Vector3(0,bound.size.y *  defaultMove,0);
            }
            else if (axisStr == RotationAxis.x.ToString())

            {
                m_vDstPosition = m_vSrcPosition + new Vector3(bound.size.x * defaultMove,0,0);
            }
            else
            {
                m_vDstPosition = m_vSrcPosition + new Vector3(0, 0, bound.size.z);
            }
               
        }
    }

}
