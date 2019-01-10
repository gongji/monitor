using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserLogin : MonoBehaviour {

    private InputField userName;
    private InputField password;
    private Text messge;
    void  Awake () {
        transform.SetParent(UIUtility.GetRootCanvas());
        transform.localPosition = Vector3.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;
        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        userName = transform.Find("userName").GetComponent<InputField>();
        password = transform.Find("password").GetComponent<InputField>();
        transform.Find("reset").GetComponent<Button>().onClick.AddListener(Reset);
        transform.Find("submit").GetComponent<Button>().onClick.AddListener(Submit);
        messge = transform.Find("messge").GetComponent<Text>();
        messge.enabled = false;

    }
	
    public void Submit()
    {
        Debug.Log("Submit");
        GameObject.Destroy(gameObject);
    }

    public void Reset()
    {
        Debug.Log("Reset");
    }

    private void Update()
    {
       transform.SetAsLastSibling();
    }
}
