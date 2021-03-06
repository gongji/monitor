﻿using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadChildEquipment : MonoBehaviour {

    public void  StartLoad(string equipmentId,System.Action callBack)
    {
        ITEquipment3dProxy.SearchITEquipmentData(equipmentId,(result)=> {

            List<EquipmentItem> equipments = Utils.CollectionsConvert.ToObject<List<EquipmentItem>>(result);
            if(equipments!=null && equipments.Count>0)
            {
                EquipmentData.DownLoadModel(equipments, () => {

                    InitCreateEquipment.StartCreateEquipment(equipments, callBack);

                });
            }
           


        },null);
    }

    
	
	
}
