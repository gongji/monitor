using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MessageBoxDelegate(string returenMsg);
public class MessageBox
{

    public static GameObject messageBoxClone;
    private static string path = "UI/MessageBox";

	// Use this for initialization
	void Start () {
		
	}
    public static void Show(string title, string msg)
    {
        Show(title, msg, Vector2.zero, MessageBoxButtonState.OK, null);
    }
    public static void Show(string title, string msg,Vector2 pos)
    {
        Show(title, msg, pos, MessageBoxButtonState.OK, null);
    }
    public static void Show(string title, string msg, MessageBoxDelegate buttonEvent)
    {
        Show(title, msg, Vector2.zero, MessageBoxButtonState.OK,buttonEvent);
    }
    public static void Show(string title, string msg, MessageBoxButtonState state, MessageBoxDelegate buttonEvent)
    {
        Show(title,msg, Vector2.zero,state,buttonEvent);
    }
    public static void Show(string title,string msg,Vector3 pos, MessageBoxButtonState state, MessageBoxDelegate buttonEvent)
    {
        if (messageBoxClone == null)
        {
            Object messageObj = Resources.Load(path);
            messageBoxClone = GameObject.Instantiate(messageObj) as GameObject;
            messageBoxClone.transform.SetParent(UIUtility.GetRootCanvas());

            messageBoxClone.transform.localScale = new Vector3(1, 1, 1);
            messageBoxClone.transform.localRotation = Quaternion.Euler(0, 0, 0);
            messageBoxClone.GetComponent<RectTransform>().anchoredPosition3D = pos;
        }
        UIMesageBoxControl uiMessage = messageBoxClone.GetComponent<UIMesageBoxControl>();

        uiMessage.Open();
        uiMessage.Clear();
        uiMessage.SetButtonState(state);
        uiMessage.SetTitle(title);
        uiMessage.SetMessage(msg);
        uiMessage.SetButtonEvent(buttonEvent);
    }

}
