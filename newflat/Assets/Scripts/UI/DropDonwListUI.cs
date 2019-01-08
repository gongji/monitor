using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDonwListUI : MonoBehaviour {

    private List<DropDownItem> dropDownList;
    public void InitData(List<DropDownItem> dropDownList)
    {
        this.dropDownList = dropDownList;
        GetComponent<Dropdown>().options.Clear();
        DropDownDataItem optiondata = new DropDownDataItem();
        optiondata.id = "";
        optiondata.text ="请选择";
        GetComponent<Dropdown>().options.Add(optiondata);
        foreach (DropDownItem item in dropDownList)
        {
             optiondata = new DropDownDataItem();
            optiondata.id = item.id;
            optiondata.text = item.name;
            GetComponent<Dropdown>().options.Add(optiondata);
        }
        dropDownList.Insert(0, null);
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
        //Debug.Log("index="+ index);
        if(index==0)
        {
           // Debug.Log("select");
            selecId = "";
            return;
        }
        if(dropDownList != null && dropDownList.Count>1)
        {

            selecId = dropDownList[index].id;
        }

      //  Debug.Log("selectId="+ selecId);
    }
}

public class DropDownDataItem: Dropdown.OptionData
{
    public string id;
}

