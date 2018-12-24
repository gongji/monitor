using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 镜头控制
/// </summary>
public class CameraManage : MonoBehaviour
{

    /// <summary>
    /// 相机的观察点  
    /// </summary>
    public Transform target;
    //次选观察点,用来计算大致位置
    public Transform targetSecondary;

    public float distance = 5;
    //主相机与观察点之间的距离
    /// <summary>
    /// 是否开启鼠标控制
    /// </summary>
    public bool isMouseControl = true;

    /// <summary>
    /// 相机旋转角X
    /// </summary>
    public float EulerAngles_x { get { return eulerAngles_x; } set { eulerAngles_x = value; } }

    /// <summary>
    /// 相机旋转角Y
    /// </summary>
    public float EulerAngles_y { get { return eulerAngles_y; } set { eulerAngles_y = value; } }

    /// <summary>
    /// 相机旋转角Z
    /// </summary>
    public float EulerAngles_z { get { return eulerAngles_z; } set { eulerAngles_z = value; } }

    /// <summary>
    /// 相机与观察点的距离
    /// </summary>
    public float Distance
    {
        get
        {
            return distance;
        }
        set
        {
            distance = value;
            distanceTo = value;
        }
    }
    //水平滚动相关
    public float distanceMax = 100;
    //主相机与目标物体之间的最大距离
    public float distanceMin = 1;
    //主相机与目标物体之间的最小距离
    public float xRotaSpeed = 70.0f;
    //主相机水平方向旋转速度



    //垂直滚动相关
    public int yMaxLimit = 90;
    //最大y（单位是角度）
    public int yMinLimit = -10;
    //最小y（单位是角度）
    public float yRotaSpeed = 70.0f;
    //主相机纵向旋转速度



    //滚轮相关
    public float MouseScrollWheelSensitivity = 20;
    //鼠标滚轮灵敏度（备注：鼠标滚轮滚动后将调整相机与目标物体之间的间隔）
    public LayerMask CollisionLayerMask;


    //平移相关
    public float xMoveSpeed = 2.0f;
    //主相机水平方向移动速度
    public float yMoveSpeed = 2.0f;
    //主相机纵向方向移动速度
    private Vector3 targetV3;


    private float timeOld;
    private Transform trantarget;

    // Use this for initialization
    [SerializeField]
    private float eulerAngles_x;
    [SerializeField]
    private float eulerAngles_y;
    [SerializeField]
    private float eulerAngles_z;
    [SerializeField]
    public float distanceTo;

    void Awake()
    {
        //Vector3 eulerAngles = this.transform.eulerAngles;//当前物体的欧拉角  
        //this.eulerAngles_x = eulerAngles.y;
        //this.eulerAngles_y = eulerAngles.x;
        //this.eulerAngles_z = eulerAngles.z;
        target = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        target.GetComponent<MeshRenderer>().enabled = false;
        Init();
        if (target == null)
        {
            float d = Vector3.Distance(transform.position, targetSecondary.position);
            Vector3 point = transform.TransformPoint(new Vector3(0, 0, d));
            target = new GameObject().transform;
            target.parent = transform.parent;
            target.name = "CamTarget";
            target.transform.localScale = new Vector3(1, 1, 1);
            target.transform.position = point;
            target.rotation = Quaternion.Euler(Vector3.zero);
//            distanceTo = d;
//            this.distance = d;
        }
        else
        {
            distanceTo = this.distance;
        }

    }


    private void OnDestroy()
    {
        if(target!=null)
        {
            GameObject.DestroyImmediate(target.gameObject);
        }
    }



    private void Init()
    {
        Vector3 eulerAngles = this.transform.eulerAngles;//当前物体的欧拉角  
        this.eulerAngles_x = eulerAngles.y;
        this.eulerAngles_y = eulerAngles.x;
        this.eulerAngles_z = eulerAngles.z;
    }

    /// <summary>
    /// 设置观察角度
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public void OnSetAngle(float x, float y, float z)
    {
        EulerAngles_x = x;
        EulerAngles_y = y;
        EulerAngles_z = z;
    }


    private bool isEnable = true;
    public void SetEnable(bool isEnable)
    {
        if(isEnable)
        {
            Init();
        }
        this.isEnable = isEnable;
    }

    private Vector3 preEulerAngles = Vector3.zero;
    private float preDistance = 0.0f;
    public void Swith2D(Bounds box,Transform boxTransform)
    {
        SetEnable(false);
        isRotation = false;
        preEulerAngles = transform.localEulerAngles;
        preDistance = distance;
        //target.position = new Vector3(box.center.x, target.position.y, box.center.z);
        //transform.localEulerAngles = new Vector3(90, 0, 0);
        //transform.position = new Vector3(box.center.x, 30, box.center.z);
        CalculateCameraPostionRoation(box, boxTransform);
        SetEnable(true);

    }

    private void CalculateCameraPostionRoation(Bounds box,Transform boxTransform)
    {

   
        Vector3 size = box.size;

        //Debug.Log(size);
        //Debug.Log(box.position);
        float maxWidth = size.z;
        if (size.x > size.z)
        {
            maxWidth = size.x;
        }
        Camera camera = Camera.main;

        GameObject vCamera = new GameObject();
        vCamera.name = "vCamera";
        float distance = camera.farClipPlane;
        float frustumHeight = 2.0f * camera.farClipPlane * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);

        float frustumWidth = frustumHeight * camera.aspect * 1.0f;


        float _distance = maxWidth * camera.farClipPlane / frustumHeight;

        Quaternion rot = Quaternion.identity;
        if (size.x > size.z)
        {
            transform.position = box.center - boxTransform.up * (_distance + size.z / 2.0f);
            rot = Quaternion.LookRotation(-1* boxTransform.up, transform.up);
            transform.localRotation = rot;
            //vCamera.transform.RotateAround(box.position, Vector3.right, 30f);

        }
        else
        {

            transform.position = box.center + boxTransform.right * (_distance + size.x / 2.0f);
            rot = Quaternion.LookRotation(-1 * boxTransform.up, -1*transform.right);
            //transform.localRotation = rot;
            //vCamera.transform.RotateAround(box.position, Vector3.forward, 30f);

        }
       


    }

    public void Swith3D()
    {
        SetEnable(false);
        transform.localEulerAngles = preEulerAngles;
        distance = preDistance;
        Init();
        SetEnable(true);
    }

  
    [HideInInspector]
    public bool isRotation = true;
    void LateUpdate()
    {
        if (this.target != null && isEnable)
        {
            
            if (Input.GetKey(KeyCode.Mouse1) && isMouseControl && isRotation)
            {
                viewpointActivityKey = false;
                this.eulerAngles_x += ((Input.GetAxis("Mouse X") * this.xRotaSpeed)) * 0.02f;
                this.eulerAngles_y -= (Input.GetAxis("Mouse Y") * this.yRotaSpeed) * 0.02f;
            }

            if (Input.GetKey(KeyCode.Mouse2) && isMouseControl)
            {
                targetV3 = new Vector3(((Input.GetAxis("Mouse X") * this.xMoveSpeed)) * distance * 0.02f, (Input.GetAxis("Mouse Y") * this.yMoveSpeed) * distance * 0.02f, 0);
                target.transform.position -= this.transform.TransformDirection(targetV3);
            }


            this.eulerAngles_y = ClampAngle(this.eulerAngles_y, (float)this.yMinLimit, (float)this.yMaxLimit);
            ChangeviewpointActivity();

            Quaternion quaternion = Quaternion.Euler(this.eulerAngles_y, this.eulerAngles_x, this.eulerAngles_z);
            if (Input.GetAxis("Mouse ScrollWheel") != 0 && isMouseControl)
            {
                distanceTo = Mathf.Clamp(this.distance - (Input.GetAxis("Mouse ScrollWheel") * MouseScrollWheelSensitivity * (distance / 10)), (float)this.distanceMin, (float)this.distanceMax);
            }
            if (this.distance != distanceTo)
            {       
                this.distance = Mathf.SmoothDamp(this.distance, distanceTo, ref yVelocity, 0.3f);
            }
            //  this.distance = Mathf.Clamp(this.distance - (Input.GetAxis("Mouse ScrollWheel") * MouseScrollWheelSensitivity * (distance/10)), (float)this.distanceMin, (float)this.distanceMax);
          
            if (GetComponent<Camera>().orthographic)
            {
                GetComponent<Camera>().orthographicSize = distance * 0.5f;
            }

            //从目标物体处，到当前脚本所依附的对象（主相机）发射一个射线，如果中间有物体阻隔，则更改this.distance（这样做的目的是为了不被挡住）  

            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Linecast(this.target.position, this.transform.position, out hitInfo, this.CollisionLayerMask))
            {
                this.distance = hitInfo.distance - 0.05f;
            }
            Vector3 vector = ((Vector3)(quaternion * new Vector3((float)0, (float)0, -this.distance))) + this.target.position;

            //更改主相机的旋转角度和位置  
            this.transform.rotation = quaternion;
            this.transform.position = vector;


            Ray ray;
            RaycastHit hit;
            LayerMask mask;
            ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            mask = 1 << LayerMask.NameToLayer("TransformManage");
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (Time.time - timeOld < 0.3f)
                    {
                        AlignObjects(hit.collider.gameObject);

                    }
                    timeOld = Time.time;
                }
            }
        }
        else
        {
        //    target = new GameObject().transform;
        //    target.name = "CamTarget";
        //    target.transform.localScale = new Vector3(1, 1, 1);
        //    target.transform.position = new Vector3(0, 0, 1000f);
        }
        if (keyCenter)
        {
            distance = Mathf.SmoothDamp(distance, toDistance, ref yVelocity, 0.3f);
            if (Mathf.Abs(yVelocity) - 0f < 15f || Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                keyCenter = false;
            }
        }
    }

    public float yVelocity = 0.0f;
    private bool keyCenter = false;
    private float toDistance;


 

    //把角度限制到给定范围内
    public float ClampAngle(float angle, float min, float max)
    {
        while (angle < -360)
        {
            angle += 360;
        }

        while (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }

    /// <summary>
    /// 物体对齐视图中
    /// </summary>
    /// <param name="obj"></param>
    public void AlignObjects(GameObject obj)
    {
        trantarget = obj.transform;
        float lenghtMax = 0;
        if (trantarget.GetComponent<Renderer>())
        {
            lenghtMax = trantarget.GetComponent<Renderer>().bounds.size.x;
            if (lenghtMax < trantarget.GetComponent<Renderer>().bounds.size.y)
            {
                lenghtMax = trantarget.GetComponent<Renderer>().bounds.size.y;
            }
            else if (lenghtMax < trantarget.GetComponent<Renderer>().bounds.size.z)
            {
                lenghtMax = trantarget.GetComponent<Renderer>().bounds.size.z;
            }
        }
        //else if (trantarget.GetComponent<BoxCollider>())
        //{
        //    lenghtMax = trantarget.GetComponent<BoxCollider>().bounds.size.x;
        //    if (lenghtMax < trantarget.GetComponent<BoxCollider>().bounds.size.y)
        //    {
        //        lenghtMax = trantarget.GetComponent<BoxCollider>().bounds.size.y;
        //    }
        //    else if (lenghtMax < trantarget.GetComponent<BoxCollider>().bounds.size.z)
        //    {
        //        lenghtMax = trantarget.GetComponent<BoxCollider>().bounds.size.z;
        //    }
        //}
        else if (trantarget.GetComponent<Collider>())
        {
            lenghtMax = trantarget.GetComponent<Collider>().bounds.size.x;
            lenghtMax = trantarget.GetComponent<Collider>().bounds.size.x;
            if (lenghtMax < trantarget.GetComponent<Collider>().bounds.size.y)
            {
                lenghtMax = trantarget.GetComponent<Collider>().bounds.size.y;
            }
            else if (lenghtMax < trantarget.GetComponent<Collider>().bounds.size.z)
            {
                lenghtMax = trantarget.GetComponent<Collider>().bounds.size.z;
            }
        }

      

        toDistance = lenghtMax * 2;
        keyCenter = true;

        CenterObj(obj);
    }



    private bool viewpointActivityKey = false;
    private Vector2 specialAngle;
    private float currentVelocity = 1;
    //设置视图视角
    public void SetViewpoinActivity(Vector2 angle)
    {
        viewpointActivityKey = true;
        specialAngle = angle;
        //   this.eulerAngles_x = angle.y;
        // this.eulerAngles_y = angle.x;
    }

    void ChangeviewpointActivity()
    {
        if (viewpointActivityKey)
        {
            this.eulerAngles_x = Mathf.SmoothStep(this.eulerAngles_x, specialAngle.y, 0.3f);
            this.eulerAngles_y = Mathf.SmoothStep(this.eulerAngles_y, specialAngle.x, 0.3f);

            if (Vector2.Distance(specialAngle, new Vector2(eulerAngles_y, eulerAngles_x)) < 0.1f)
            {
                this.eulerAngles_x = specialAngle.y;
                this.eulerAngles_y = specialAngle.x;
                viewpointActivityKey = false;
            }
        }
    }

    public void CenterObj(GameObject obj)
    {
        iTween.MoveTo(target.gameObject, iTween.Hash("position", obj.transform.position, "time", 1, "loopType", iTween.LoopType.none, "easeType", iTween.EaseType.easeOutQuart));
    }
}
