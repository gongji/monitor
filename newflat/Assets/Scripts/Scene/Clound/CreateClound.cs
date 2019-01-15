using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateClound : MonoBehaviour {


    public Transform[] ts;
    List<Vector4> shaderValue = new List<Vector4>();

    private float commonValue = 50.0f;
	void Awake () {
       // CreateMesh();
      
	}

    public void InitComponent()
    {
         if(GetComponent<MeshRenderer>()==null)
       {
           gameObject.AddComponent<MeshRenderer>();
       }

       if(GetComponent<MeshFilter>()==null)
       {
           gameObject.AddComponent<MeshFilter>();
       }
    }

  //  private float width = 10.0f;
   // private float length = 15.0f;
   
	
	void Update () {

        shaderValue.Clear();
       
       if(ts!=null)
       {
            foreach (Transform t in ts)
            {
                Vector2 uv = ConvertLocalCoordinateToUVCoordinate(t.position, transform.position);
                Vector4 pointData = new Vector4(uv.x, uv.y, float.Parse(t.name));
                shaderValue.Add(pointData);

            }
       }
        
     //  AddVector4Value();
        SetValue();
	}

    

    

    public float width = 10.0f; 

     public float length = 15.0f; 
    public void CreateMesh()
    {
        Vector3[] newVertices = { new Vector3(-length / 2, 0, -width / 2), new Vector3(length / 2, 0, -width / 2), new Vector3(length / 2, 0, width / 2), new Vector3(-length / 2, 0, width / 2) };
        Vector2[] newUV = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector3(0, 1) };
        int[] newtriangles = { 0, 2, 1, 0, 3, 2 };

        Mesh mesh = new Mesh();
        mesh.Clear();
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newtriangles;
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = new Material(Shader.Find("Custom/TemperatureField"));
    }



    Vector2 ConvertLocalCoordinateToUVCoordinate(Vector3 pos, Vector3 planeCenter)
    {
       
        float tempU = (pos.x - planeCenter.x) / (length / 2.0f);
        float u = 0.5f + tempU / 2.0f;
     
        float tempV = (pos.z - planeCenter.z) / (width / 2.0f);
        float v = 0.5f + tempV / 2.0f;

        return new Vector2(u,v);
    }


    public void ChangeTemptureValue()
    {

        foreach (var item in ts)
        {
            item.name = ChangeValue(item.name).ToString();
        }
    }

    private float ChangeValue(string name)
    {
        float temptureValue = float.Parse(name);
        if(temptureValue>32)
        {
            return 100.0f;
        }else if(temptureValue>28)
        {
             return 80.0f;
        }
        else if(temptureValue>25)
        {
            return 60.0f;
        }else if(temptureValue>20)
        {
            return 30.0f;
        }
        else if(temptureValue>15)
        {
             return 10.0f;
        }

        return 5.0f;
    }

    private void AddVector4Value()
    {

        if (shaderValue.Count>10)
        {
            shaderValue = shaderValue.GetRange(0, 10);
        }
        else
        {
            int length = (10 - shaderValue.Count);
            for(int i=0;i<length;i++)
            {
                Vector4 v=new Vector4(0,0,commonValue);
                shaderValue.Add(v);
            }
        }
    }
    private  void SetValue()
    {
        GetComponent<MeshRenderer>().material.SetFloat("_Point1", commonValue);
        GetComponent<MeshRenderer>().material.SetFloat("_Point2", commonValue);
        GetComponent<MeshRenderer>().material.SetFloat("_Point3", commonValue);
        GetComponent<MeshRenderer>().material.SetFloat("_Point4", commonValue);

        GetComponent<MeshRenderer>().material.SetInt("_Points_Num", shaderValue.Count);

        GetComponent<MeshRenderer>().material.SetVectorArray("_PointsArray", shaderValue.ToArray());
      
    }
}
