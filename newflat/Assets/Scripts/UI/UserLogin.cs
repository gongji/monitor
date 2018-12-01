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

        messge.GetComponent<Text>().font = FontResouce.Instance.font;
        transform.Find("title").GetComponent<Text>().font = FontResouce.Instance.font;
        password.GetComponent<InputField>().contentType = InputField.ContentType.Password;
        messge.enabled = false;
    }
	
    public void Submit()
    {
        //  Debug.Log("Submit");
        string _userName = userName.text.Trim();
        string _password = password.text.Trim();
        UserProxy.UserLogin((result) => {
            UserItem userItem = Utils.CollectionsConvert.ToObject<UserItem>(result);
            //成功
            if(userItem!=null)
            {
                GameObject.Destroy(gameObject);
            }
            else
            {
                messge.enabled = true;
            }

        },(error)=> {
            GameObject.Destroy(gameObject);

        }, _userName, _password);
       
    }

    private void ShowErrorMessage(string content)
    {

    }

    public void Reset()
    {
        
        userName.text = "";
        password.text = "";
       // Debug.Log("Reset");
    }

    private void Update()
    {
       transform.SetAsLastSibling();

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Submit();
        }
    }
}
