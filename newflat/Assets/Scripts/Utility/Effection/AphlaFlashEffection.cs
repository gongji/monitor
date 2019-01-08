using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自发光颜色
/// </summary>
public class AphlaFlashEffection : MonoBehaviour
{
    Renderer[] renders;
    private System.Action callBack;

    //void Start()
    //{
    //   Flash(1000,null);
    //}
    public void Flash()
    {
        //if (callBack!=null)
        //{
        //    callBack.Invoke();
        //}
       

        renders = GetComponentsInChildren<Renderer>();
        if(gameObject.activeSelf)
        {
            StartCoroutine(StartFlash());
        }
        
    }

    private Dictionary<object, object> dic = new Dictionary<object, object>();
    private IEnumerator StartFlash()
    {
        dic.Clear();
        MeshRenderer[] mrs  = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in mrs)
        {
            Material[] ms = mr.materials;
            foreach (Material m in ms)
            {
                m.EnableKeyword("_EMISSION");
                dic.Add(m, m.GetColor("_EmissionColor"));
            }
          
        }
       
        float _val = 0.8f;
        float _val1 = 0;
        float _offest = (_val - 0.2f) / 5;
        bool _dir = true;
        float tempTime = Time.realtimeSinceStartup;
    
        while (true)
        {
            foreach (Material _mat in dic.Keys)
            {

                if (_val1 < _val && _dir)
                {
                    _val1 += _offest;
                }
                else
                {
                    _dir = false;
                    _val1 -= _offest;
                    if (_val1 < 0.2f)
                        _dir = true;
                }
                _mat.SetColor("_EmissionColor", new Color(0, _val1, _val1));

                gameObject.GetComponentInChildren<MeshRenderer>().material = _mat;
            }


            yield return new WaitForSeconds(0.1f);

            //if ((Time.realtimeSinceStartup - tempTime) > totalTime)
            //{
            //    if (callBack != null)
            //    {
            //        callBack.Invoke();
            //    };
            //    StopAllTask();
            //    yield break;
            //}
        }
      

    }

    private void ResetColor()
    {
        foreach (Material _mat in dic.Keys)
        {

            _mat.SetColor("_EmissionColor", (Color)dic[_mat]);
        }
    }

    public void StopAllTask()
    {
        
        //foreach (Renderer mr in renders)
        //{
        //    mr.enabled = true;
        //}
        ResetColor();
        StopAllCoroutines();
        GameObject.Destroy(this);
    }
}
