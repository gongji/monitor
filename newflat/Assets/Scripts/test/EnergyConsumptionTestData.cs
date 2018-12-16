using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyConsumptionTestData  {


    public static List<EnergyConsumptionItem>  GetTestData()
    {

        List<EnergyConsumptionItem> result = new List<EnergyConsumptionItem>();
        EnergyConsumptionItem eci = new EnergyConsumptionItem();
        eci.id = "229";
        eci.startR = (byte) Random.Range(0, 255);
        eci.startG = (byte)Random.Range(0, 255);
        eci.startB = (byte)Random.Range(0, 255);
        eci.endR = (byte)Random.Range(0, 255);
        eci.endG = (byte)Random.Range(0, 255);
        eci.endB = (byte)Random.Range(0, 255);
        result.Add(eci);


        eci = new EnergyConsumptionItem();
        eci.id = "234";
        eci.startR = (byte)Random.Range(0, 255);
        eci.startG = (byte)Random.Range(0, 255);
        eci.startB = (byte)Random.Range(0, 255);
        eci.endR = (byte)Random.Range(0, 255);
        eci.endG = (byte)Random.Range(0, 255);
        eci.endB = (byte)Random.Range(0, 255);
        result.Add(eci);




        eci = new EnergyConsumptionItem();
        eci.id = "237";
        eci.startR = (byte)Random.Range(0, 255);
        eci.startG = (byte)Random.Range(0, 255);
        eci.startB = (byte)Random.Range(0, 255);
        eci.endR = (byte)Random.Range(0, 255);
        eci.endG = (byte)Random.Range(0, 255);
        eci.endB = (byte)Random.Range(0, 255);
        result.Add(eci);


        eci = new EnergyConsumptionItem();
        eci.id = "238";
        eci.startR = (byte)Random.Range(0, 255);
        eci.startG = (byte)Random.Range(0, 255);
        eci.startB = (byte)Random.Range(0, 255);
        eci.endR = (byte)Random.Range(0, 255);
        eci.endG = (byte)Random.Range(0, 255);
        eci.endB = (byte)Random.Range(0, 255);
        result.Add(eci);

        eci = new EnergyConsumptionItem();
        eci.id = "239";
        eci.startR = (byte)Random.Range(0, 255);
        eci.startG = (byte)Random.Range(0, 255);
        eci.startB = (byte)Random.Range(0, 255);
        eci.endR = (byte)Random.Range(0, 255);
        eci.endG = (byte)Random.Range(0, 255);
        eci.endB = (byte)Random.Range(0, 255);
        result.Add(eci);
      // UnityEngine.Debug.Log(Utils.CollectionsConvert.ToJSON(result));
        return result;
    }
}
