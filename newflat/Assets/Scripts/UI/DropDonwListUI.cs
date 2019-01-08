using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 获取维修人列表
/// </summary>
public class DropDonwListUI : MonoBehaviour {

    private List<UserItem> userList;
    public void InitData(List<UserItem> userList)
    {
        this.userList = userList;
        GetComponent<Dropdown>().options.Clear();
        DropDownDataItem optiondata = new DropDownDataItem();
        optiondata.id = "";
        optiondata.text ="请选择";
        GetComponent<Dropdown>().options.Add(optiondata);
        foreach (UserItem item in userList)
        {
             optiondata = new DropDownDataItem();
            optiondata.id = item.useId;
            optiondata.text = item.userName;
            GetComponent<Dropdown>().options.Add(optiondata);
        }
    }

    private string selecId = ""; 

    public string SelectId
    {
        get
        {
            return selecId;
        }
    }
    public void ChangeSelect()
    {
        int index = GetComponent<Dropdown>().value;
        if(index==0)
        {
            selecId = "";
            return;
        }
        if(userList!=null && userList.Count>1)
        {

            selecId = userList[index+1].useId;
        }

        Debug.Log("selectId="+ selecId);
    }
}

public class DropDownDataItem: Dropdown.OptionData
{
    public string id;
}

