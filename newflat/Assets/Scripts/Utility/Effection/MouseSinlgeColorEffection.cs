using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MouseSinlgeColorEffection : MonoBehaviour
{
    Renderer[] renders;
    private System.Action callBack;
    private string property;
    private List<Tweener> tweeners = new List<Tweener>();
    //_Color
    public void SetEffection(Color color, string property = "_EmissionColor")
    {
        dic.Clear();
        this.property = property;
        Save(property);

        foreach (Material _mat in dic.Keys)
        {
            _mat.SetColor(property, color);
        }
    }


    private void Save(string property)
    {
        MeshRenderer[] mrs = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in mrs)
        {
            Material[] ms = mr.materials;
            foreach (Material m in ms)
            {
                dic.Add(m, m.GetColor(property));
            }

        }
    }

    private Dictionary<object, object> dic = new Dictionary<object, object>();
     public void ResetColor()
    {
        foreach (Material _mat in dic.Keys)
        {

            _mat.SetColor(property, (Color)dic[_mat]);
        }
    }

   
}
