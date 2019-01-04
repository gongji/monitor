using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserListUI : MonoBehaviour {

	// Use this for initialization
	void Start () {

 
    }

    private List<UserItem> userList;
    public void InitData(List<UserItem> userList)
    {
        this.userList = userList;
        GetComponent<Dropdown>().options.Clear();
        foreach(UserItem item in userList)
        {
            DropDownDataItem optiondata = new DropDownDataItem();
            optiondata.id = item.userid;
            optiondata.text = item.userName;
            GetComponent<Dropdown>().options.Add(optiondata);
        }
    }

    private string selectUserId = string.Empty; 

    public string SelectUserId
    {
        get
        {
            return selectUserId;
        }
    }
    public void ChangeSelect()
    {
        int index = GetComponent<Dropdown>().value;
        if(userList!=null && userList.Count>0)
        {
            selectUserId = userList[index].userid;
        }
    }
}

public class DropDownDataItem: Dropdown.OptionData
{
    public string id;
}


public class UserItem
{
    public string userName;

    public string userid;
}
