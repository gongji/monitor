using Core.Common.Logging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KongtiaoGrounp : EquipmentChildControl
{

    private ILog log = LogManagers.GetLogger("KongtiaoGrounp");
    public override void Alarm(int state = 0)
    {
        base.Alarm(state);
    }
    public override void CancleAlarm()
    {
        base.CancleAlarm();
    }
    public override void SelectEquipment()
    {
        base.SelectEquipment();
    }

    public override void CancelEquipment()
    {
        base.CancelEquipment();
    }

}
