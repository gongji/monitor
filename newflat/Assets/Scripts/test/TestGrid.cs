using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour {

	void Update () {
		
        if(Input.GetKeyDown(KeyCode.A))
        {
            Show();
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            GridMsg.Instance.Hide();
        }
	}

    public void  Show()
    {
        string url = "Grid/TestPointCenter";
        int[] colums = new int[] {100,200,197 };
        string[] titles = new string[] {"number","name","state"};

        List<List<string>> result = new List<List<string>>();
        for(int i=0;i<5;i++)
        {
            List<string> dataItem = new List<string>();
            dataItem.Add("number" + i);
            dataItem.Add("name" + i);
            dataItem.Add("state" + i);
            result.Add(dataItem);
        }
        GridMsg.Instance.Show<NormalGrid>(url, 500, 290, "alarm information", "Title/close", colums, titles, result);
    }
}
