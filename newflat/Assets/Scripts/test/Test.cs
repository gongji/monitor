using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    public Transform t1 = null;
	void Start () {

        Debug.Log(Camera.allCameras);
        foreach(Camera c in Camera.allCameras)
        {
            Debug.Log(c.name);
        }
       
	}

    // Update is called once per frame
    //void Update()
    //{

    //    Transform t = FindObjUtility.GetParentObject(t1);
    //    Debug.Log(t);
    //}

    public void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Debug.Log("OnMouseDown");
        UIElementCommandBar.instance.SelectEquipment(gameObject);
       // UIElementCommandBar.instance.Show();
    }


}
