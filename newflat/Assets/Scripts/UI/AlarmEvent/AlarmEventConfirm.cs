using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using animation;
using DG.Tweening;
using Core.Common.Logging;
using System.Linq;

public  class AlarmEventConfirm: AlarmEventWindowBase
{
    private static ILog log = LogManagers.GetLogger("AlarmEventConfirm");


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
            
            if(string.IsNullOrEmpty(result))
            {
                log.Debug("GetRepairUserList data is null");
                return;
            }
            List<UserItem> itemList = Utils.CollectionsConvert.ToObject<List<UserItem>>(result);

            var dropItemList = from n in itemList
                           select new DropDownItem
                           {
                               id = n.useId,
                               name = n.userName

                           };

           
            GetComponentInChildren<DropDonwListUI>().InitData(dropItemList.ToList<DropDownItem>());

        });
    }

    public void ConfirmButton()
    {
        if(string.IsNullOrEmpty(GetComponentInChildren<DropDonwListUI>().SelectId))
        {
            UIUtility.ShowTips("维修人不能为空");
            return;
        }
        string eventContent = GetEventContet();
        if (string.IsNullOrEmpty(eventContent))
        {
            UIUtility.ShowTips("备注内容不能为空");
            return;
        }
        if(callBack!=null && aei!=null)
        {
            


            callBack.Invoke(aei, GetComponentInChildren<DropDonwListUI>().SelectId, eventContent);
            Hide();
        }
    }

    public string GetEventContet()
    {
        return GetComponentInChildren<InputField>().text;
    }
 
}
