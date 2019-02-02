using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdSwitchMsg : MonoBehaviour {

    public Camera oriCamera;

    public GameObject thirdGameObject;

    public GameObject child;

    public Camera thirdCamera;

    public static ThirdSwitchMsg instacne;
    protected Texture2D m_FirstPersonIcon = null;

    private void Awake()
    {
        child.SetActive(false);
        instacne = this;
    }
    private void Start()
    {
        m_FirstPersonIcon = Resources.Load("UI/frist_target") as Texture2D;
    }

    private bool isFlyCameraMode = true;
    public bool IsFlyCameraMode
    {
        get

        {
            return isFlyCameraMode;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && !isFlyCameraMode)
        {
            if(cameraModeUI!=null)
            {
                SwitchMode(cameraModeUI);
            }
        }
    }


    private Transform cameraModeUI = null;

    public void SwitchMode(Transform cameraModeUI)
    {
        this.cameraModeUI = cameraModeUI;
        if (isFlyCameraMode)
        {
            cameraModeUI.GetComponentInChildren<Text>().text = "人物模式";
            ThirdSwitchMsg.instacne.SwitchThirdPerson();
            UIUtility.ShowTips("您已进入人物模式，按Q键切换键飞行模式。");
        }
        else
        {
            cameraModeUI.GetComponentInChildren<Text>().text = "飞行模式";
            ThirdSwitchMsg.instacne.SwitchNormalCamera();

        }

        isFlyCameraMode = !isFlyCameraMode;
    }


    public void  SwitchNormalCamera()
    {
        child.gameObject.SetActive(false);

        // oriCamera.transform.position = thirdCamera.transform.position;
        // oriCamera.transform.rotation = thirdCamera.transform.rotation;
       
        oriCamera.gameObject.SetActive(true);
        CameraInitSet.ResetCameraPostion();

    }

    public void SwitchThirdPerson()
    {
        oriCamera.gameObject.SetActive(false);
        child.SetActive(true);

        GameObject point = ThirdManyouMsg.GetManYouPoint();
        if (point == null)
        {
            thirdGameObject.transform.position = oriCamera.transform.position - Vector3.up * 0.6f;
        }
        else
        {
            thirdGameObject.transform.position = point.transform.position + Vector3.up *1.5f;
        }
        
    }


    private void OnGUI()
    {
        if (!isFlyCameraMode)
        {
            Cursor.visible = false;

#if UNITY_EDITOR
            int nSize = 32;
#else
           int nSize = Screen.width > 1920 ? (Screen.width * 32) / 1920 : 32;
#endif


            DrawCursor(new Vector3(Screen.width >> 1, (Screen.height + nSize) >> 1), m_FirstPersonIcon, nSize);
        }
        else
        {
            Cursor.visible = true;
        }
    }

    private void DrawCursor(Vector3 mousePosition, Texture2D texture, int nIconSize)
    {
        GUI.DrawTexture(new Rect(mousePosition.x - (nIconSize >> 1), Screen.height - mousePosition.y - (nIconSize >> 1), nIconSize, nIconSize), texture);
    }
}
