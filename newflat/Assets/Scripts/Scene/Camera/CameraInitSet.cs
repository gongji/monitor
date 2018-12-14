using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 相机的初始化设置
/// </summary>
public static class CameraInitSet {

    private static Vector3 cameraPostion = Vector3.zero;
    private static Quaternion cameraRoation = Quaternion.identity;

    private static string _sceneid = string.Empty;

    private static Transform _box = null;

    private static float _cameraMoveTime = 0.5f;
    public static void StartSet(string sceneid, Transform box, float cameraMoveTime,System.Action callBack)
    {
        _sceneid = sceneid;
        _box = box;
        _cameraMoveTime = cameraMoveTime;

        if (AppInfo.currentView == ViewType.View2D)
        {

            ViewSwitch.instance.Switch2D(box);
            if(box!=null)
            {
                SetIsEnbaleCamera(box.gameObject, true);
            }
            
            if (callBack!=null)
            {
                callBack.Invoke();
            }
            return;
        }

     
        CameraViewData.GetCurrentSceneCameraView((result) =>
        {
            CameraViewItem cameraView = result;
            if (cameraView != null)
            {
                // Debug.Log("数据库读取");
                cameraPostion = new Vector3(cameraView.x, cameraView.y, cameraView.z);
                cameraRoation = Quaternion.Euler(cameraView.rotationX, cameraView.rotationY, cameraView.rotationZ);
            }
            else
            {
                // Debug.Log("本地拉取");

                CalculateCameraPostionRoation(box);
            }

            CameraMove(cameraMoveTime, callBack, box);


        });

    }

    private static void CameraMove(float cameraMoveTime, System.Action callBack, Transform box)
    {
        CameraAnimation.CameraMove(Camera.main, cameraPostion, cameraRoation.eulerAngles, cameraMoveTime, () =>
        {
            // Debug.Log(box.name);
            if(_box!=null)
            {
                SetIsEnbaleCamera(_box.gameObject, true);
            }
            
            if (callBack != null)
            {
                callBack.Invoke();
            }


        });
    }


    private static void CalculateCameraPostionRoation(Transform box)
    {
        if(!box)
        {
            return;
        }
        bool isEnable = box.GetComponent<BoxCollider>().enabled;
        box.GetComponent<BoxCollider>().enabled = true;
        Vector3 size = box.GetComponent<BoxCollider>().bounds.size;

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


        float _distance = maxWidth * camera.farClipPlane / frustumWidth;

        Quaternion rot = Quaternion.identity;
        if (size.x > size.z)
        {
            vCamera.transform.position = box.position - box.transform.forward * (_distance + size.z / 2.0f);
            rot = Quaternion.LookRotation(box.forward, Vector3.up);
            vCamera.transform.localRotation = rot;
            vCamera.transform.RotateAround(box.position, Vector3.right, 30f);

        }
        else
        {

            vCamera.transform.position = box.position + box.transform.right * (_distance + size.x / 2.0f);
            rot = Quaternion.LookRotation(-1 * box.right, Vector3.up);
            vCamera.transform.localRotation = rot;
            vCamera.transform.RotateAround(box.position, Vector3.forward, 30f);
        }
        cameraRoation = vCamera.transform.rotation;
        cameraPostion = vCamera.transform.position;
        box.GetComponent<BoxCollider>().enabled = isEnable;
        // Debug.Log(cameraPostion);
        GameObject.Destroy(vCamera);
    }


    public static void SetIsEnbaleCamera(GameObject box, bool isEnable)
    {
       // Debug.Log(box);
        CameraObjectController coc = Camera.main.gameObject.GetComponent<CameraObjectController>();
        //CameraRotatoAround acc = Camera.main.gameObject.GetComponent<CameraRotatoAround>();
        //if(acc==null)
        //{
        //    acc = Camera.main.gameObject.AddComponent<CameraRotatoAround>();
        //}
        //acc.target = box.transform;
        //float _distance = Vector3.Distance(box.transform.position, Camera.main.transform.position);
        //acc.distance = _distance;
        //acc.distanceMax = _distance * 2.0f;
        //acc.distanceMin = 0.0f;

        //if(_distance<5)
        //{
        //    acc.speedx = 20.0f;
        //    acc.MouseScrollWheelSensitivity = 0.4f;
        //}
        //else
        //{
        //    acc.speedx = 4.0f;
        //    acc.MouseScrollWheelSensitivity = 2.0f;

        //}

        if (isEnable && coc!=null)
        {
            IState mCurrentState = Main.instance.stateMachineManager.mCurrentState;
            if (mCurrentState is AreaState)
            {
                coc.SetSpeedReset();
            }
            else if (mCurrentState is FloorState)
            {
                coc.SetSpeedFloorChange();
            }
            else if (mCurrentState is RoomState)
            {
                coc.SetSpeedRoomChange();
            }
            if (box != null)
            {
                //Debug.Log("aaaaaaaaaa");
                SceneContext.sceneBox = box.transform;
            }
            float yvalue = CaluteCameraRangeHeight.GetCameraHeight();
           // coc.SetBox(box.GetComponent<BoxCollider>(), 2,yvalue);
        }
        //coc.SetEnable(isEnable);

    }

    public static void ResetCameraPostion()
    {
        StartSet(_sceneid, _box, _cameraMoveTime, null);
    }

    /// <summary>
    /// 系统的初始化视角
    /// </summary>
    public static void SystemInitCamera()
    {
        string sceneid = SceneData.GetIdByNumber(Constant.Main_dxName.ToLower());
        // string   sql = "sceneId = " + sceneid + " and (equipId is null or equipId = 0)";
        string sql = "sceneId = -1 and (equipId is null or equipId = 0)";
        CameraViewData.CallProxyGetViewData(sql, (cameraView) => {

            
            Vector3 cameraPostion = new Vector3(cameraView.x, cameraView.y, cameraView.z);
            Quaternion cameraRoation = Quaternion.Euler(cameraView.rotationX, cameraView.rotationY, cameraView.rotationZ);
            Camera.main.transform.position = cameraPostion;
            Camera.main.transform.rotation = cameraRoation;
            Camera.main.GetComponent<CameraObjectController>().SetCameraPostion();


        });


       
    }


    /// <summary>
    /// 设置绕中心点旋转
    /// </summary>
    /// <param name="box"></param>
    public static void SetRotationCamera(Transform  centerTransform)
    {
        CameraObjectController coc = Camera.main.gameObject.GetComponent<CameraObjectController>();
        coc.enabled = false;
        CameraRotatoAround acc = Camera.main.gameObject.GetComponent<CameraRotatoAround>();
        if (acc == null)
        {
            acc = Camera.main.gameObject.AddComponent<CameraRotatoAround>();
        }
        acc.target = centerTransform;
        float _distance = Vector3.Distance(centerTransform.position, Camera.main.transform.position);
        acc.distance = _distance;
        acc.distanceMax = _distance * 2.0f;
        acc.distanceMin = 0.0f;
        acc.speedx = 0.5f;
        acc.MouseScrollWheelSensitivity = 5.0f;
    }

    public static void SetObjectCamera()
    {
        CameraRotatoAround acc = Camera.main.gameObject.GetComponent<CameraRotatoAround>();
        if (acc != null)
        {
            GameObject.DestroyImmediate(acc);
        }

        CameraObjectController coc = Camera.main.gameObject.GetComponent<CameraObjectController>();
        if(coc!=null)
        {
            coc.enabled = true;
        }
        
    }
}
