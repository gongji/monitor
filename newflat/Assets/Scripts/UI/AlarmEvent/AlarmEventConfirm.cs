using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using animation;

public  class AlarmEventConfirm: AlarmEventWindowBase
{
    public System.Action<AlarmEventItem,string> callBack;
    private void Start()
    {

        //foreach(Button buttton in GetComponentsInChildren<Button>())
        //{
        //    buttton.onClick.AddListener(() =>
        //    {
        //        Hide();
        //    });
        //}

        UserProxy.GetRepairUserList((result)=> {
            List<UserItem> itemList = Utils.CollectionsConvert.ToObject<List<UserItem>>(result);
            GetComponentInChildren<UserListUI>().InitData(itemList);

        });
    }

    public void ConfirmButton()
    {
        if(callBack!=null && aei!=null)
        {
            callBack.Invoke(aei, GetComponentInChildren<UserListUI>().SelectUserId);
        }
    }
 
}
