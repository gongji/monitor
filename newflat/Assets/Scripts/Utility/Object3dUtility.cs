using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using System.Reflection;
using DataModel;
using System.IO;
using System.Runtime.Serialization;

public static  class Object3dUtility
{
    /// <summary>
    /// 计算GameObject的BoxColliderVert的八个顶点
    /// </summary>
    /// <param name="gameObject">3d对象</param>
    /// <returns></returns>
    public static List<Vector3> CalculateBoxColliderVert(GameObject obj)
    {
        List<Vector3> pointList = new List<Vector3>();
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        if (colliders.Length != 0)
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue), max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            for (int i = 0; i < colliders.Length; i++)
            {
                min.x = min.x > colliders[i].bounds.min.x ? colliders[i].bounds.min.x : min.x;
                min.y = min.y > colliders[i].bounds.min.y ? colliders[i].bounds.min.y : min.y;
                min.z = min.z > colliders[i].bounds.min.z ? colliders[i].bounds.min.z : min.z;

                max.x = max.x < colliders[i].bounds.max.x ? colliders[i].bounds.max.x : max.x;
                max.y = max.y < colliders[i].bounds.max.y ? colliders[i].bounds.max.y : max.y;
                max.z = max.z < colliders[i].bounds.max.z ? colliders[i].bounds.max.z : max.z;
            }

            min = obj.transform.position + (obj.transform.position - min) ;
            max = obj.transform.position + (max - obj.transform.position) ;

            pointList.Add(obj.transform.position);

            pointList.Add(min);
            pointList.Add(new Vector3(min.x, min.y, max.z));
            pointList.Add(new Vector3(min.x, max.y, min.z));
            pointList.Add(new Vector3(min.x, max.y, max.z));

            pointList.Add(max);
            pointList.Add(new Vector3(max.x, min.y, min.z));
            pointList.Add(new Vector3(max.x, min.y, max.z));
            pointList.Add(new Vector3(max.x, max.y, min.z));
        }

        return pointList;
    }

    /// <summary>
    /// 取box的八个顶点
    /// </summary>
    /// <param name="boxcollider"></param>
    /// <returns></returns>
    public static Vector3[]  GetBoxColliderVertexPositions(BoxCollider boxcollider)

    {

        Vector3[] vertices = new Vector3[8];
            //下面4个点
        vertices[0] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, -boxcollider.size.y, boxcollider.size.z) * 0.5f);
        vertices[1] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, -boxcollider.size.y, boxcollider.size.z) * 0.5f);
        vertices[2] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, -boxcollider.size.y, -boxcollider.size.z) * 0.5f);
        vertices[3] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, -boxcollider.size.y, -boxcollider.size.z) * 0.5f);
        //上面4个点
        vertices[4] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, boxcollider.size.y, boxcollider.size.z) * 0.5f);
        vertices[5] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, boxcollider.size.y, boxcollider.size.z) * 0.5f);
        vertices[6] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, boxcollider.size.y, -boxcollider.size.z) * 0.5f);
        vertices[7] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, boxcollider.size.y, -boxcollider.size.z) * 0.5f);

        return vertices;
       
    }


    /// <summary>
    /// 取box中心平行的四个顶点
    /// </summary>
    /// <param name="boxcollider"></param>
    /// <returns></returns>
    public static Vector3[] GetBoxColliderVertex(BoxCollider boxcollider)

    {

        Vector3[] vertices = new Vector3[4];
        //下面4个点
        vertices[0] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x * 0.5f, boxcollider.center.y, boxcollider.size.z * 0.5f) );
        vertices[1] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x * 0.5f, boxcollider.center.y, boxcollider.size.z * 0.5f) );
        vertices[2] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x * 0.5f, boxcollider.center.y, -boxcollider.size.z * 0.5f) );
        vertices[3] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x * 0.5f, boxcollider.center.y, -boxcollider.size.z * 0.5f) );
        //上面4个点
       

        return vertices;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameObject">3d对象面向相机</param>
    /// <param name="isXaxis">X轴面向相机</param>
    /// <param name="isYaxis">Y轴面向相机</param>
    /// <param name="isZaxis">Z轴面向相机</param>
    /// <param name="camera">相机</param>
    public static void LookAtCamera(GameObject obj, bool isXaxis, bool isYaxis, bool isZaxis, Camera camera)
    {
        obj.transform.forward = camera.transform.position - obj.transform.position;
        Vector3 eulerAngles = obj.transform.eulerAngles;
        if (!isXaxis)
            eulerAngles.x = 0.0f;
        if (!isYaxis)
            eulerAngles.y = 0.0f;
        if (!isZaxis)
            eulerAngles.z = 0.0f;
        obj.transform.eulerAngles = eulerAngles;
    }


    /// <summary>
    /// 3d对象Y轴始终注视相机
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="isXaxis"></param>
    /// <param name="isYaxis"></param>
    /// <param name="isZaxis"></param>
    /// <param name="camera"></param>
    public static void LookAtCamera2(GameObject obj, Camera camera)
    {
        Vector3 direction = Vector3.up;
        obj.transform.rotation = Quaternion.LookRotation(obj.transform.position - camera.transform.position, direction);
    }

    /// <summary>
    /// 判断3d对象是否位于相机的正前方
    /// </summary>
    /// <param name="gameObject">3d对象</param>
    /// <param name="camera">面向的相机</param>
    public static bool IsCameraForword(GameObject gameObject, Camera camera)
    {
        Transform cameraTrans = camera.transform;
        Vector3 vDirection = gameObject.transform.position - cameraTrans.position;
        if (Vector3.Dot(vDirection, cameraTrans.forward) <= Constant.EPSILON_E4)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 判断3d对象是否位于相机的正前方
    /// </summary>
    /// <param name="gameObject">3d对象</param>
    /// <param name="camera">面向的相机</param>
    public static bool IsCameraForword2(GameObject gameObject, Camera camera)

    {
        //Debug.Log(gameObject.name);
        Vector3 vScreenPosition = camera.WorldToViewportPoint(gameObject.transform.position);
       // Debug.Log(vScreenPosition);
        if (vScreenPosition.x < 0.0f || vScreenPosition.x > 1.0f || vScreenPosition.y < 0.0f || vScreenPosition.y > 1.0f)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 判断3d对象是否位于相机的正前方
    /// </summary>
    /// <param name="gameObject">3d对象</param>
    /// <param name="camera">面向的相机</param>
    public static bool IsCameraForword3(Vector3 _position, Camera camera)
    {
        Vector3 vScreenPosition = camera.WorldToViewportPoint(_position);
        if (vScreenPosition.x < 0.0f || vScreenPosition.x > 1.0f || vScreenPosition.y < 0.0f || vScreenPosition.y > 1.0f)
        {
            return false;
        }

        return true;
    }


    /// <summary>
    /// 判断target对象是否在camera的可视范围之内
    /// </summary>
    /// <param name="target"></param>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static bool IsVisible(GameObject target, Camera camera)
    {
        Plane[] cps = GeometryUtility.CalculateFrustumPlanes(camera);

        bool isVisible = GeometryUtility.TestPlanesAABB(cps, target.GetComponent<Collider>().bounds);

        return isVisible;
    }


    /// <summary>
    /// 设置3d对象随着相机的距离进行缩放
    /// </summary>
    /// <param name="gameObject">需要缩放3d对象</param>
    /// <param name="cameraPoison">相机的位置</param>
    /// <param name="m_MinScale">最小缩放</param>
    /// <param name="m_MaxScale">最大缩放</param>
    /// <param name="m_CameraRange"></param>
    public static void Object3dScale(GameObject gameObject, Vector3 cameraPoison, Vector3 m_MinScale, Vector3 m_MaxScale, Vector2 m_CameraRange)
    {
        
        float fLerp = Mathf.Clamp01((Vector3.Distance(gameObject.transform.position, cameraPoison) - m_CameraRange.x) / (m_CameraRange.y - m_CameraRange.x));
        gameObject.transform.localScale = Vector3.Lerp(m_MinScale, m_MaxScale, fLerp);
    }



    /// <summary>设置一个物体的层包含其子物体</summary>
    public static void SetLayerValue(int layer, GameObject go, bool includingDevice = false)
    {
        if (!go)
        {
            return;
        }
        Transform[] ts = go.GetComponentsInChildren<Transform>(true);
        foreach (Transform tf in ts)
        {
           
           tf.gameObject.layer = layer;
            
        }
       
    }

    /// <summary>
    /// 控制物体（包含子物体）的MeshRenderer显示或者隐藏
    /// </summary>
    /// <param name="t"></param>
    /// <param name="isEnable"></param>
    public static void EnbleMeshRenderer(Transform t, bool isEnable)
    {
        MeshRenderer[] meshs = t.GetComponentsInChildren<MeshRenderer>();
        MeshRenderer _mesh = t.GetComponent<MeshRenderer>();
        if (_mesh != null)
            _mesh.enabled = isEnable;
        for (int i = 0; i < meshs.Length; i++)
        {
            meshs[i].enabled = isEnable;
        }
    }

    /// <summary>
    /// 控制显示隐藏（包含子物体的碰撞）
    /// </summary>
    /// <param name="t">T.</param>
    /// <param name="isEnable">If set to <c>true</c> is enable.</param>
    public static void EnbleCollider(Transform t, bool isEnable)
    {
        Collider[] meshs = t.GetComponentsInChildren<Collider>();
        Collider _mesh = t.GetComponent<Collider>();
        if (_mesh != null)
            _mesh.enabled = isEnable;
        for (int i = 0; i < meshs.Length; i++)
        {
            meshs[i].enabled = isEnable;
        }
    }

 
    /// <summary>
    /// 根据colliderContainer的boxColledr生成五面体
    /// </summary>
    /// <param name="colliderContainer"></param>
    public static void CreateFivePlane(GameObject colliderContainer, int layer, float globalUnit)
    {
        if (colliderContainer == null)
        {
            return;
        }
        BoxCollider srcCollder = colliderContainer.GetComponent<BoxCollider>();


        if (srcCollder == null)
        {
            srcCollder = colliderContainer.AddComponent<BoxCollider>();
        }

        Bounds BoxBound = srcCollder.transform.GetComponent<MeshRenderer>().bounds;
        Vector3 boxVector3 = BoxBound.size;

        //生成下边面
        GameObject buttomPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        buttomPlane.gameObject.AddComponent<BoxCollider>();
        Bounds planeBound = buttomPlane.GetComponent<MeshRenderer>().bounds;
        buttomPlane.name = "buttomPlane";
        buttomPlane.layer = layer;
        Vector3 planeVector3 = planeBound.size;
        buttomPlane.transform.parent = colliderContainer.transform;
        //将y的值缩为1，否则的话会倾斜
        buttomPlane.transform.localScale = new Vector3((boxVector3.x / planeVector3.x) / globalUnit, 1, (boxVector3.z / planeVector3.z) / globalUnit);
        buttomPlane.transform.localPosition = new Vector3(0, 0, (-boxVector3.y / 2) / globalUnit);

    }

    private static  Bounds maxBound;

    /// <summary>
    /// >获取子节点最大的包围盒
    /// </summary>
    /// <param name="parentTransform"></param>
    /// <returns></returns>
    public static Bounds GetMaxBounds(Transform parentTransform)
    {
        Transform[] childs = parentTransform.GetComponentsInChildren<Transform>();
       
        float maxBoundsValue = 0.0f;
        foreach (Transform temp in childs)
        {
            MeshRenderer meshRenderer = temp.GetComponent<MeshRenderer>();

            if (meshRenderer)
            {
                Bounds bound = meshRenderer.GetComponent<MeshRenderer>().bounds;

                if ((bound.size.x * bound.size.y * bound.size.z) > maxBoundsValue)
                {
                    maxBoundsValue = bound.size.x * bound.size.y * bound.size.z;
                    maxBound = bound;
                }
            }
            else
            {
                MeshFilter mf = temp.GetComponent<MeshFilter>();

                if (mf != null && mf.sharedMesh)
                {
                    if ((mf.sharedMesh.bounds.size.x * mf.sharedMesh.bounds.size.y * mf.sharedMesh.bounds.size.z) > maxBoundsValue)
                    {
                        maxBoundsValue = mf.sharedMesh.bounds.size.x * mf.sharedMesh.bounds.size.y * mf.sharedMesh.bounds.size.z;
                        maxBound = mf.sharedMesh.bounds;
                    }
                }

            }
        }
        return maxBound;

        
    }

    /// <summary>
    /// 获取碰撞盒的世界坐标
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    public static Vector3 GetWorldPostion(Transform colliderTransform)
    {
        BoxCollider boxCollider = colliderTransform.gameObject.GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            Vector3 pos = colliderTransform.TransformPoint(boxCollider.center);
            return pos;
        }

        return Vector3.zero;             
    }


    /// <summary>
    /// 比较对象的属性是否相同。
    /// </summary>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    /// <returns></returns>
    public static bool IsCompareObjectProperty(EquipmentItem item1, EquipmentItem item2)
    {
        
        System.Type type1 = item1.GetType();
        FieldInfo[] fieldInfos1 = type1.GetFields();

        System.Type type2 = item2.GetType();

        foreach (var f in fieldInfos1)
        {
            //字段名称
            string fieldName = f.Name;

            FieldInfo fieldInfo2 = type2.GetField(fieldName);
            //字段类型
           // string fieldType = f.FieldType.ToString();

            object fieldValue1 = f.GetValue(item1);

            object fieldValue2 = fieldInfo2.GetValue(item2);

            bool isSame = true;
            if (f.FieldType  == typeof(System.Single))
            {
                isSame = Mathf.Approximately((float)fieldValue1, (float)fieldValue2);
                //Debug.Log(fieldName + ":" + isSame.ToString());
            }
            else
            {
                isSame = fieldValue1.Equals(fieldValue2);

               // Debug.Log(fieldName + ":" + isSame.ToString());
            }

            if(!isSame)
            {
                return false;
            }
        }

        return true;
    }

    public static EquipmentItem DeepClone()
    {
        return null;
        //using (Stream objectStream = new MemoryStream())
        //{
        //    IFormatter formatter = new BinaryFormatter();
        //    formatter.Serialize(objectStream, this);
        //    objectStream.Seek(0, SeekOrigin.Begin);
        //    return formatter.Deserialize(objectStream) as Employee;
        //}
    }


}
