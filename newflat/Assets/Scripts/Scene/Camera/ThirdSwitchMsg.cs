using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdSwitchMsg : MonoBehaviour {

    public Camera oriCamera;

    public GameObject thirdGameObject;

    public Camera thirdCamera;

    public static ThirdSwitchMsg instacne;
    private void Awake()
    {
        thirdGameObject.SetActive(false);
        instacne = this;
    }
    
    public void  SwitchNormalCamera()
    {
        thirdGameObject.gameObject.SetActive(false);

        oriCamera.transform.position = thirdCamera.transform.position;
        oriCamera.transform.rotation = thirdCamera.transform.rotation;
        oriCamera.gameObject.SetActive(true);
        
    }

    public void SwitchThirdPerson()
    {
        oriCamera.gameObject.SetActive(false);
        thirdGameObject.SetActive(true);

        GameObject point = ManyouMsg.GetManYouPoint();
        if (point == null)
        {
            thirdGameObject.transform.position = oriCamera.transform.position - Vector3.up * 0.6f;
        }
        else
        {
            thirdGameObject.transform.position = point.transform.position;
        }
        
    }
}
