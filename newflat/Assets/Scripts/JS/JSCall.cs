using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class JSCall : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void BindEquipment(string equipmentid);

    [DllImport("__Internal")]
    private static extern void SaveSwitchData(string type, string sceneid);

    [DllImport("__Internal")]
    private static extern void OpenCamera(string equipmentid );


    [DllImport("__Internal")]
    private static extern void InitSceneFinish();

    [DllImport("__Internal")]
    private static extern void PrintFloatArray(float[] array, int size);

    [DllImport("__Internal")]
    private static extern int AddNumbers(int x, int y);

    [DllImport("__Internal")]
    private static extern string StringReturnValueFunction();

    [DllImport("__Internal")]
    private static extern void BindWebGLTexture(int texture);

    void Start()
    {
        //Hello();

        //HelloString("This is a string.");

        //float[] myArray = new float[10];
        //PrintFloatArray(myArray, myArray.Length);

        //int result = AddNumbers(5, 7);
        //Debug.Log(result);

        //Debug.Log(StringReturnValueFunction());

        //var texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
        //BindWebGLTexture(texture.GetNativeTextureID());
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    _SaveSwitchData("123","scene");
        //}
    }

    public void _SaveSwitchData(string type, string sceneid)
    {
        SaveSwitchData(type, sceneid);
    }

    public void _BindEquipment(string equipmentid)
    {
        BindEquipment(equipmentid);
    }

    public void _OpenCamera(string equipmentid)
    {
        OpenCamera(equipmentid);
    }


    /// <summary>
    /// 只能接收一个参数
    /// </summary>
    /// <param name="paramater"></param>
    public void OpenScene(string paramater)
    {
        string[] result = paramater.Split(',');
        string type = result[0];
        string sceneid = result[1];
        Debug.Log(type +","+ sceneid);
        ExternalSceneSwitch.Instance.SwitchScene(type, sceneid);
    }


    public void _InitSceneFinish()
    {
        InitSceneFinish();
    }


}
