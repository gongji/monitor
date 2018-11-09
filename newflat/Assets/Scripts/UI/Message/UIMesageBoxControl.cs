using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MessageBoxButtonState
{
    OK,
    OK_Cancel,
    None
}
public class UIMesageBoxControl : MonoBehaviour {

    private Text text_title;
    private Text text_message;
    private Button btn_ok;
    private Button btn_cancel;
    private MessageBoxButtonState currentState = MessageBoxButtonState.None;
    private MessageBoxDelegate onBtnEvent;

    private Vector2 btn_ok_pos = new Vector2(0f,22f);
    private Vector2 btn_ok_both_pos = new Vector2(-70.6f,22f);


    // Use this for initialization
    void Awake () {

        text_message = transform.Find("Text_Message").GetComponent<Text>();
        text_title = transform.Find("Text_Title").GetComponent<Text>();
        if (text_message == null) Debug.LogError("MessageBox`s  text_message is null");
        if (text_title == null) Debug.LogError("MessageBox`s  text_title is null");

        btn_ok = transform.Find("Btn_OK").GetComponent<Button>();
        btn_cancel = transform.Find("Btn_Cancel").GetComponent<Button>();
        if (btn_ok == null) Debug.LogError("MessageBox`s  btn_ok is null");
        if (btn_cancel == null) Debug.LogError("MessageBox`s  btn_cancel is null");
    }
    private void Start()
    {
        btn_ok.onClick.AddListener(()=> { OnOK(); });
        btn_cancel.onClick.AddListener(()=> { OnCancel(); });
    }

    void OnGUI()
    {
        if (gameObject.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return))
            {
                OnOK();
            }
           
        }
    }

    public void OnOK()
    {
        if (onBtnEvent != null)
        {
            onBtnEvent("ok");
        }
        Close();

    }

    public void OnCancel()
    {
        if (onBtnEvent != null)
        {
            onBtnEvent("cancel");
        }
        Close();
    }


    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetMessage(string msg)
    {
        if (text_message)
        {
            text_message.text = msg;
        }
    }
    public void SetTitle(string content)
    {
        if (text_title)
        {
            text_title.text = content;
        }
    }

    public void Clear()
    {
        if (text_message)
        {
            text_title.text = "";
            text_message.text = "";
        }

        //if (onBtnEvent != null)
        //{
        //    onBtnEvent = null;
        //}
    }

    public void SetButtonState(MessageBoxButtonState state)
    {
        if (state == MessageBoxButtonState.OK)
        {
            btn_ok.gameObject.SetActive(true);
            btn_cancel.gameObject.SetActive(false);
           // btn_ok.gameObject.GetComponent<RectTransform>().anchoredPosition = btn_ok_pos;
        }
        else if (state == MessageBoxButtonState.OK_Cancel)
        {
            btn_ok.gameObject.SetActive(true);
            btn_cancel.gameObject.SetActive(true);
           // btn_ok.gameObject.GetComponent<RectTransform>().anchoredPosition = btn_ok_both_pos;
        }
    }

    public void SetButtonEvent(MessageBoxDelegate boxDelegate)
    {
        this.onBtnEvent = boxDelegate;
    }

}
