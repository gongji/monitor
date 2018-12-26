using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour {
    public Transform centerTransform;

    private MeshRenderer[] mrs;

    private Dictionary<MeshRenderer, Vector3> dic = new Dictionary<MeshRenderer, Vector3>();
    void Start()
    {
        mrs = GetComponentsInChildren<MeshRenderer>();

        Debug.Log(mrs.Length);
        foreach (MeshRenderer mr in mrs)
        {
            mr.gameObject.AddComponent<MeshCollider>();
            dic.Add(mr, mr.transform.position);
        }

        // GetComponent<MeshCollider>().

    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    UpdatePostion(0.1f);
        //}

        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    UpdatePostion(-0.1f);
        //}
    }


    public void UpdatePostion(float distacne)
    {
        foreach (MeshRenderer mr in mrs)
        {
            mr.transform.position += (mr.transform.GetComponent<MeshCollider>().bounds.center - centerTransform.position) * distacne;
        }
    }

}
