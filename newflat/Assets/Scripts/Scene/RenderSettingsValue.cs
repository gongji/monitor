using UnityEngine;
using System.Collections;

/// <summary>
/// 渲染设置参数
/// </summary>
public class RenderSettingsValue : MonoBehaviour
{
    //天空盒
    [SerializeField]
    public Material skyMaterial;
    //环境光模式
    [SerializeField]
    public UnityEngine.Rendering.AmbientMode ambientMode;

    //环境光的强度
    [SerializeField]
    public float ambientIntensity;

    //环境光颜色
    [SerializeField]
    public Color ambientLight;

    //环境光的天空盒颜色
    [SerializeField]
    public Color ambientSkyColor;

    //赤道颜色
    [SerializeField]
    public Color ambientEquatorColor;
    //地面颜色
    [SerializeField]
    public Color ambientGroundColor;

    //是否启用雾效

    [SerializeField]
    public bool isfog;

    [SerializeField]
    public Color fogColor;

    [SerializeField]
    public FogMode fogmode;

    [SerializeField]
    public float fogStartDistance;


    [SerializeField]
    public float fogEndDistance;

     [SerializeField]
    public float fogDensityValue;


    /// <summary>
    /// 设置渲染参数
    /// </summary>
    public void SetRenderSettings()
    {

        //Debug.Log("bbbbb");
        //Skybox skybox = SceneController.currentCamera.GetComponent<Skybox>();
        //if (skybox !=null)
        //{
        //    Debug.Log("SetRenderSettings:"+SceneController.currentCamera.name);
        //    skybox = SceneController.currentCamera.gameObject.AddComponent<Skybox>();
        //    Material currentMat = SkyDataManeger.Instance.currentMode ? skyMaterial : SkyDataManeger.Instance.NightDayMat;
        //    skybox.material = currentMat;
        //}
        //Material currentMat = SkyDataManeger.Instance.currentMode ? skyMaterial : SkyDataManeger.Instance.NightDayMat;
        
        ////Color temp = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f);
        ////skyMaterial.SetColor("_Tint", temp);

        ////skyMaterial.SetFloat("_Exposure", 1);

        RenderSettings.skybox = skyMaterial;



        RenderSettings.ambientMode = ambientMode;
        //if (!SkyDataManeger.Instance.currentMode)
        //{
        //    return;
        //} 
        if (ambientMode == UnityEngine.Rendering.AmbientMode.Trilight)
            
        {
            RenderSettings.ambientSkyColor = ambientSkyColor;
            RenderSettings.ambientEquatorColor = ambientEquatorColor;
            RenderSettings.ambientGroundColor = ambientGroundColor;

        }
        else if(ambientMode == UnityEngine.Rendering.AmbientMode.Flat)
        {
            RenderSettings.ambientLight = ambientLight;
        }

        RenderSettings.ambientIntensity = ambientIntensity;
        RenderSettings.fog = isfog;

        if (isfog)
        {
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogMode = fogmode;
            if (fogmode == FogMode.Linear)
            {
                 RenderSettings.fogStartDistance =fogStartDistance;
                 RenderSettings.fogEndDistance =fogEndDistance;
            }
            else
            {
                RenderSettings.fogDensity = fogDensityValue;
            }

        }
        
    }

    /// <summary>
    /// 切换到非园区模式
    /// </summary>
    public  void SwitchNoArea(bool flag)
    {
        if (flag)
        {
            RenderSettings.skybox = null;
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(207/255.0f,183/255.0f,183/255.0f,0.7f);
        }
        else
        {
            SetRenderSettings();
        }
    }


    private float m_rotationSpeed = 1;
    private void Update()
    {
       //float rotation = skyMaterial.GetFloat("_Rotation");
       //rotation += rotation + Time.deltaTime * m_rotationSpeed;
       //skyMaterial.SetFloat("_Rotation", rotation % 360);

    }
    public static void SetNoAreaEffction()
    {
        RenderSettings.skybox = null;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(207 / 255.0f, 183 / 255.0f, 183 / 255.0f, 0.7f);
    }

}
