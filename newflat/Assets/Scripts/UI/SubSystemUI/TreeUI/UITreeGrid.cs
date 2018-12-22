/***********************************
    **  日期:
    **  姓名:jss
    **  审阅:jss
    **  功能:
    **  备注:
************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UITreeGrid : MonoBehaviour
{
    public int index;
    public Vector2 cellSize = new Vector2(100, 50);
    public List<GameObject> childs = new List<GameObject>();
    public List<GameObject> BranchButtonchilds = new List<GameObject>();
    public List<GameObject> Arrowchilds = new List<GameObject>();
    private TreeManager treeManage;
    private int max;
    public int p;
   
    public void OnInit(TreeManager param)
    {
        this.treeManage = param;
        max = treeManage.MaxCount;
        childs.ForEach(o =>
            {
                if (o.GetComponent<UIBranchButton>() != null)
                {
                    if (!BranchButtonchilds.Contains(o))
                        BranchButtonchilds.Add(o);
                }
                else
                {
                    if (!Arrowchilds.Contains(o))
                        Arrowchilds.Add(o);
                }
                    
            });
       // Debug.Log(BranchButtonchilds.Count);
        if (BranchButtonchilds.Count > max)
        {
            for (int i = max; i < BranchButtonchilds.Count; i++)
            {
                BranchButtonchilds[i].SetActive(false);
            }
        }
        p = 0;
        ControlArrow();
    }

    public void OnArrowUpClicked()
    {
        if (p > 0)
        {
            p--;
        }
        ControlArrow();
        ShowSplit(p, p + treeManage.MaxCount);
        Debug.Log("OnArrowUpClicked");
    }

    public void OnArrowDownClicked()
    {
        if (p < (BranchButtonchilds.Count - max))
        {
            p++;
        }
        ControlArrow();
        ShowSplit(p, p + treeManage.MaxCount);
        Debug.Log("OnArrowDownClicked");
    }

    private void ControlArrow()
    {
        if (Arrowchilds.Count != 2)
            return;
        if (p == 0)
        {
            Arrowchilds[0].GetComponent<Button>().interactable = false;
            Arrowchilds[1].GetComponent<Button>().interactable = true;
        }
        else if (p == BranchButtonchilds.Count - max)
        {
            Arrowchilds[0].GetComponent<Button>().interactable = true;
            Arrowchilds[1].GetComponent<Button>().interactable = false;
        }
        else
        {
            Arrowchilds[0].GetComponent<Button>().interactable = true;
            Arrowchilds[1].GetComponent<Button>().interactable = true;            
        }
    }

    private void ShowSplit(int p1, int p2)
    {
        if (BranchButtonchilds.Count < p2)
            return;
        if (p1 < 0)
            return;
        List<GameObject> _list = BranchButtonchilds.GetRange(p1, p2 - p1);
        _list.ForEach(o => o.SetActive(true));


        if (p1 != 0)
        {
            List<GameObject> _list2 = BranchButtonchilds.GetRange(0, p1);
            _list2.ForEach(o => o.SetActive(false));            
        }
        if (p2 != BranchButtonchilds.Count)
        {
            List<GameObject> _list3 = BranchButtonchilds.GetRange(p2, BranchButtonchilds.Count - p2);
            _list3.ForEach(o => o.SetActive(false));
        }

    }
}
