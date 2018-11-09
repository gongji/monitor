using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    void Awake()
    {
        //Debug.Log("123");
        AppFacade app=new AppFacade();
        app.StartUp(gameObject);
    }
}
