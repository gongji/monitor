using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBatchCopy : MonoBehaviour {

    public Transform target;

    //机柜的宽度
    float equipmnetWidth = 0.0f;

    //间隙宽度
    private float spaceWidth = 0.001f;

    private Vector3 defaultEulerAngles = Vector3.zero;

    private void Awake()
    {
        target = gameObject.transform;
        defaultEulerAngles = target.localEulerAngles;
    }

    void Start () {

        equipmnetWidth = target.GetComponent<BoxCollider>().bounds.size.x;
       
	}
    private bool isCreate = false;

   
    public void SetCreateState()
    {
        isCreate = true;
    }
    void Update() {
       
        if (isCreate)
        {
            StartCreate();
        }

        float offest = Input.GetAxis("Mouse ScrollWheel");

        if(Input.GetKey (KeyCode.LeftControl) && (offest >= 0.01f || offest <= -0.01f))
        {
            //Debug.Log(offest);
            if (offest >= 0.01f)
            {
                spaceWidth = spaceWidth + spaceWidth/4.0f;
            }
            else if (offest <= -0.01f)
            {
                spaceWidth = spaceWidth - spaceWidth / 4.0f;
            }
            Vector3 mouseTargetPostion = GetDrawPoint();
            Create(mouseTargetPostion);
        }

        //取消键
        if(Input.GetKey(KeyCode.Escape))
        {
            OperateControlManager.Instance.CurrentState = OperateControlManager.EquipmentEditState.None;
            DeleteList();
            target.localEulerAngles = defaultEulerAngles;
            GameObject.Destroy(this);
        }
	}
    private Vector3 lastMousePosition = Vector3.zero;
    private bool first = true;
    private void StartCreate()
    {
        Vector3 mouseTargetPostion = GetDrawPoint();
        if(Input.GetKey(KeyCode.LeftShift))
        {
            mouseTargetPostion = SetShiftPosition(mouseTargetPostion,target.position);
        }

        float distance = 0.0f;
        if (first)
        {
            distance = Vector3.Distance(target.position, mouseTargetPostion);
            if(distance> (equipmnetWidth * 2))
            {
                Create(mouseTargetPostion);
            }
        }
        else
        {
            distance = Vector3.Distance(lastMousePosition, mouseTargetPostion);
            if(distance > (equipmnetWidth * 2))
            {
                Create(mouseTargetPostion);
            }
        }

        first = false;
    }


    List<GameObject> gs = new List<GameObject>();
    private void Create(Vector3 mouseTargetPostion)
    {
        DeleteList();

        Vector3 relative = mouseTargetPostion - target.position;

        target.forward = relative;
        target.transform.localRotation = target.transform.localRotation * Quaternion.Euler(0, 90, 0);
      

        float equipmentSpace = equipmnetWidth + spaceWidth;
        float distance = Vector3.Distance(target.position, mouseTargetPostion);

        int num = (int)(distance / (equipmentSpace));
        //Debug.Log(num);

        for (int i = 0; i < num-1; i++)
        {
            GameObject coloneEquipment = GameObject.Instantiate(target.gameObject);
            GameObject.Destroy(coloneEquipment.GetComponent<EquipmentBatchCopy>());
            coloneEquipment.transform.parent = transform.parent;
            coloneEquipment.transform.localScale = target.transform.localScale;
            if (gs.Count == 0)
            {
                coloneEquipment.transform.position = target.position - target.right * (equipmentSpace);
            }
            else
            {
                coloneEquipment.transform.position = gs[gs.Count - 1].transform.position - target.right * (equipmentSpace);
            }

            float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
          //  coloneEquipment.transform.localEulerAngles = new Vector3(0, angle+90, 0);
            Vector3 localEulerAngles = new Vector3(0, angle + 90, 0);
            coloneEquipment.transform.localEulerAngles = new Vector3(FormatUtil.FloatFomart(localEulerAngles.x, 1), 
                FormatUtil.FloatFomart(localEulerAngles.y, 1), FormatUtil.FloatFomart(localEulerAngles.z, 1));
            gs.Add(coloneEquipment);

        }
        lastMousePosition = mouseTargetPostion;
    }

    private void DeleteList()
    {
        foreach(GameObject  g in gs)
        {
            GameObject.Destroy(g);
        }
        gs.Clear();
    }

    private void OnDestroy()
    {
        if(gs!=null && gs.Count>0)
        {
           // Debug.Log("增加了："+ gs.Count);
            foreach(GameObject temp  in gs)
            {
                Object3DElement.AddNewItem(temp.GetComponent<Object3DElement>());
            }
        }
    }

    private Vector3 GetDrawPoint()
    {
       
        Vector3 v = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 9))
        {
            return hit.point;
        }
        return v;
    }

    private Vector3 SetShiftPosition(Vector3 mousePotion,Vector3 targetPostion)
    {
        float xOffeset = Mathf.Abs(mousePotion.x- targetPostion.x);
        float zOffeset = Mathf.Abs(mousePotion.z - targetPostion.z);
        if(xOffeset< zOffeset)
        {
            mousePotion = new Vector3(targetPostion.x, mousePotion.y, mousePotion.z);
        }
        else
        {
            mousePotion = new Vector3(mousePotion.x, mousePotion.y, targetPostion.z);
        }

        return mousePotion;
    }

    /// <summary>
    /// 复制单个设备
    /// </summary>
    /// <param name="sourceEquipment"></param>
    public static void CopySinlgeEquipment(Transform  sourceEquipment)
    {
        GameObject coloneEquipment = GameObject.Instantiate(sourceEquipment.gameObject);
        coloneEquipment.name = "colone_" + sourceEquipment.name;
        coloneEquipment.transform.parent = sourceEquipment.parent;
        coloneEquipment.transform.localScale = sourceEquipment.localScale;
        coloneEquipment.transform.localRotation = sourceEquipment.localRotation;
        Vector3 boundSize = sourceEquipment.GetComponent<BoxCollider>().bounds.size;
        coloneEquipment.transform.position = sourceEquipment.transform.position + sourceEquipment.right * (boundSize.x);
        coloneEquipment.GetComponent<Object3DElement>().equipmentData.name = coloneEquipment.name;
       
        Object3DElement.AddNewItem(coloneEquipment.GetComponent<Object3DElement>());
    }



}
