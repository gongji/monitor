using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

        messge.GetComponent<Text>().font = FontResouceMsg.Instance.font;
        transform.Find("title").GetComponent<Text>().font = FontResouceMsg.Instance.font;
        password.GetComponent<InputField>().contentType = InputField.ContentType.Password;
        messge.enabled = false;
        
    }
	
    void Start()
    {
        userName.ActivateInputField();
    }
    public void Submit()
    {
        //  Debug.Log("Submit");
        string _userName = userName.text.Trim();
        string _password = password.text.Trim();
        if(string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
        {
            ShowErrorMessage("用户名或密码不能为空");
            return ;
        }
        UserProxy.PcUserLogin((result) => {
            UserItem userItem = Utils.CollectionsConvert.ToObject<UserItem>(result);
            //成功
            if(userItem!=null && userItem.state == 1)
            {
                SceneJump.JumpFirstPage();
                GameObject.Destroy(gameObject);
            }
            else
            {
                ShowErrorMessage("账号错误，请检查后重试");
            }

        },(error)=> {
           
           ShowErrorMessage("服务器位未知错误！");
            

        }, _userName, _password);
       
    }

    private void ShowErrorMessage(string content)
    {
        messge.text = content;
        messge.enabled = true;

        DOVirtual.DelayedCall(3.0f,()=>{
            messge.enabled = false;

        });
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
