﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全局单例类，更新温湿度
/// </summary>
public class WenshiduTimer : MonoSingleton<WenshiduTimer> {

    public  void StartTimer()
    {
        StartCoroutine(Start());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    private IEnumerator Start()
    {
        while(true)
        {
            UpdateData();
            yield return new WaitForSeconds(5.0f);
        }
    }

    private void UpdateData()
    {
        WenShiduDataUpdate[]  wenshidus =  GameObject.FindObjectsOfType<WenShiduDataUpdate>();
        if(wenshidus.Length>0)
        {
            //接口未定暂时这样写
            WenshiDuProxy.GetWenShiduList(null, (successCallBack) => {

               List<WenShiduItemData>  list =  Utils.CollectionsConvert.ToObject<List<WenShiduItemData>>(successCallBack);
                foreach(WenShiduItemData itemData in list)
                {
                   GameObject itemObject =   EquipmentData.FindGameObjectById(itemData.id);
                    if(itemObject != null && itemObject.GetComponent<WenShiduDataUpdate>()!=null)
                    {
                        itemObject.GetComponent<WenShiduDataUpdate>().UpdateaData(itemData.temperatureValue, itemData.humidity);
                    }
                }
            }, (failture) => {

            });
        }
    }
}