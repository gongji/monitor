using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadChildEquipment : MonoBehaviour {

    public void  StartLoad(string equipmentId,System.Action callBack)
    {
        ITEquipment3dProxy.SearchITEquipmentData(equipmentId,(result)=> {

            List<EquipmentItem> equipments = Utils.CollectionsConvert.ToObject<List<EquipmentItem>>(result);

            EquipmentData.DownLoadModel(equipments, () => {

                BrowserCreateEquipment.StartCreateEquipment(equipments, callBack);

            });


        },null);
    }

    
	
	
}
