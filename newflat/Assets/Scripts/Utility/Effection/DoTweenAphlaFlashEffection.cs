using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 透明度
/// </summary>
public class DoTweenAphlaFlashEffection : MonoBehaviour
{
    Renderer[] renders;
    private System.Action callBack;


    private string property;
    private List<Tweener> tweeners = new List<Tweener>();
    //_Color
    public void Flash(Color color, string property = "_EmissionColor")
    {
        StopAllTask();
        tweeners.Clear();
        dic.Clear();
        this.property = property;
        Save(property);

        foreach (Material _mat in dic.Keys)
        {
            Tweener tweener =  _mat.DOBlendableColor(color, property, 1.0f);
            tweener.SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            tweeners.Add(tweener);
        }

    }

    private void Update()
    {
        foreach (Material _mat in dic.Keys)
        {

           Debug.Log(_mat.GetColor(property));
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
                //Debug.LogError(m.GetColor(property));
                dic.Add(m, m.GetColor(property));
            }

        }
    }

    private Dictionary<object, object> dic = new Dictionary<object, object>();
    

    private void ResetColor()
    {
        foreach (Material _mat in dic.Keys)
        {

            _mat.SetColor(property, (Color)dic[_mat]);
        }
    }

    public void StopAllTask()
    {
        foreach (Tweener t in tweeners)
        {
           
           t.Kill();
           


        }
        ResetColor();
   
       // GameObject.Destroy(this);
    }
}
