using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using animation;
using DG.Tweening;

public  class AlarmEventConfirm: AlarmEventWindowBase
{
    public System.Action<AlarmEventItem,string,string> callBack;
    private void Start()
    {

        transform.Find("Title/close").GetComponent<Button>().onClick.AddListener(() => {

            Hide();
        });

        transform.Find("Confirm").GetComponent<Button>().onClick.AddListener(() => {

            ConfirmButton();
        });


        UserProxy.GetRepairUserList((result)=> {
            List<UserItem> itemList = Utils.CollectionsConvert.ToObject<List<UserItem>>(result);
            GetComponentInChildren<UserListUI>().InitData(itemList);

        });
    }

    public void ConfirmButton()
    {
        if(callBack!=null && aei!=null)
        {
            string eventContent = GetEventContet();


            callBack.Invoke(aei, GetComponentInChildren<UserListUI>().SelectUserId, eventContent);
            Hide();
        }
    }

    public string GetEventContet()
    {
        return "事件的内容";
    }
 
}
