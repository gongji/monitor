
using UnityEngine;

using System.Collections;
using UnityEngine.EventSystems;

public class CameraRotatoAround : MonoBehaviour
{
    public float speedx = 4.0f;
    public float speedy = 15.0f;

    [Header("target")]
    public Transform target;

    public float distance = 7.0f;
    private float eulerAngles_x;

    private float eulerAngles_y;


    public float distanceMax = 10;

    public float distanceMin = 3;

    public int yMaxLimit = 60;

    public int yMinLimit = -10;


    public float MouseScrollWheelSensitivity = 1.0f;

    public LayerMask CollisionLayerMask;

    private Vector3 last;
    private Vector3 now;


    private bool isEnabel = true;
    private Vector3 postion =Vector3.zero;
    public Vector3 Postion
    {
        get
        {
            return postion;
        }
    }
    private Quaternion _quaternion = Quaternion.identity;
    public Quaternion quaternion
    {
        get
        {
            return _quaternion;
        }
    }
    public void SetEnable(bool isEnabel = true)
    {
        if(isEnabel)
        {
            Init();
        }
        this.isEnabel = isEnabel;
        //禁用
        if(!isEnabel)
        {
            postion = transform.position;
            _quaternion = transform.localRotation;

        }
    }


    public Transform cube;
    void Start()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        //target = cube;
        //Vector3 eulerAngles = this.transform.eulerAngles;

        //this.eulerAngles_x = eulerAngles.y;

        //this.eulerAngles_y = eulerAngles.x;

    }

    public void Init()
    {
        Vector3 eulerAngles = this.transform.eulerAngles;

        this.eulerAngles_x = eulerAngles.y;

        this.eulerAngles_y = eulerAngles.x;
    }


    float mouse_x;
    float mouse_y;
    Vector3 vector_c;

    private void FixedUpdate()
    {
        Check();
        if (!isEnabel)
        {
            return;
        }

        if (EventSystem.current!=null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }


        if (Input.GetMouseButton(1))
        {
            isRotate = false;
            transform.Translate(Vector3.left * Input.GetAxis("Mouse X") * 15f*Time.deltaTime);
            transform.Translate(Vector3.up * Input.GetAxis("Mouse Y")*-1f * 15f*Time.deltaTime);
            ismove = true;
            wait = true;
            now = transform.position;
        }

        if (ismove == false)
        {
            time += 0.1f;
            transform.position= Vector3.Lerp(now, last, time);
            if (time >= 1)
            {
                ismove = true;
                wait = false;
                time = 0;
            }
        }
        UpdateMoveRoation();


    }

    private bool isRotate=true;
    private bool ismove = true;
    private bool wait = false;
    private float time = 0;
    void UpdateMoveRoation()
    {
        if (!isEnabel)
        {
            return;
        }

        if (this.target != null)
        {
            if (Input.GetMouseButton(0))
            {
               
                mouse_x = Input.GetAxis("Mouse X");
                mouse_y = Input.GetAxis("Mouse Y");
                Cxy = Mathf.Sqrt(mouse_x * mouse_x + mouse_y * mouse_y);
               // Debug.Log(Cxy);
                if (Cxy == 0f)
                {
                    
                    Cxy = 1f;
                }
                ismove = false;
                isRotate = true;
                Ondrag = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                Ondrag = false;
            }
            this.distance = Mathf.Clamp(this.distance - (Input.GetAxis("Mouse ScrollWheel") * MouseScrollWheelSensitivity), (float)this.distanceMin, (float)this.distanceMax);

            if (isRotate)
            {
                if (wait==false)
                {
                    vector_c = new Vector3(mouse_x, mouse_y, 0) * Rigid();

                    this.eulerAngles_x += (vector_c.x * this.distance * speedx) * 0.02f;

                    this.eulerAngles_y -= (vector_c.y * speedy) * 0.02f;

                    this.eulerAngles_y = ClampAngle(this.eulerAngles_y, (float)this.yMinLimit, (float)this.yMaxLimit);

                    Quaternion quaternion = Quaternion.Euler(this.eulerAngles_y, this.eulerAngles_x, (float)0);


                    RaycastHit hitInfo = new RaycastHit();

                    if (Physics.Linecast(this.target.position, this.transform.position, out hitInfo, this.CollisionLayerMask))

                    {
                        this.distance = hitInfo.distance - 0.05f;
                    }

                    Vector3 postion = ((Vector3)(quaternion * new Vector3((float)0, (float)0, -this.distance))) + this.target.position;

                    this.transform.rotation = quaternion;
                    this.transform.position = postion;

                    last = transform.position;
                }
               
            }          
        }
    }

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

    private bool Ondrag = false;

    public float Speed = 2f;

    private float Tempspeed;

    private float Cxy;

    float Rigid()
    {
        if (Ondrag)
        {
            Tempspeed = Speed;
        }
        else
        {
            if (Tempspeed > 0)
            {
                Tempspeed -= Speed * 0.3f * Time.deltaTime / (0.5f * Cxy);
            }
            else
            {
                Tempspeed = 0;
            }
        }
        return Tempspeed;
    }




    public void Check()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width/2.0f,Screen.height/2.0f,0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f, 1 << Constant.SceneLayer))
        {
            Debug.Log(hit.transform);
            cube.position = hit.point;
        }

    }

}

