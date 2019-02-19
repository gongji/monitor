using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using DigitalSalmon.Fade;

public class FadeManager : MonoBehaviour {

    private float fadeInTime = 1.0f;
    private float fadeOutTime = 1.0f;

    public static FadeManager Instance;
    void Awake()
    {
        Instance = this;

    }
    private void Start()
    {
        //string path = Application.streamingAssetsPath + "/UI/fadebg.png";
        //ResourceUtility.Instance.GetHttpTexture(path, (result) => {

        //    GetComponent<Transform>().localScale = Vector3.zero;
        //    UIUtility.CreateSprite((Texture2D)result, GetComponent<Image>(), Vector2.zero);
        //});

        DOVirtual.DelayedCall(8f, () =>
        {
            effects = new System.Collections.Generic.List<FadeEffect>();
            string path = Application.streamingAssetsPath +  "/prefeb/" + PlatformMsg.instance.currentPlatform.ToString() + "/fade";
            ResourceUtility.Instance.GetHttpAssetBundle(path, (bundle) => {

                string[] names = bundle.GetAllAssetNames();
                // Debug.Log(a.GetAllAssetBundles().Length);
                foreach (string name in names)
                {

                    FadeEffect item = bundle.LoadAsset<FadeEffect>(name);
                    effects.Add(item);

                }
            });
        });


    }

    [SerializeField]
    protected FadePostProcess fadePostProcess;

    [SerializeField]
    protected List<FadeEffect> effects;

    private int effectIndex = 0;
    private void SwitchFadeEffect()
    {
        if (effects == null  || effects.Count == 0 )
        {

            return;
        }
        effectIndex++;
        if (effectIndex >= effects.Count) effectIndex = 0;

        fadePostProcess.AssignEffect(effects[effectIndex]);
    }


    private void Update()
    {
       
    }

    private bool isFadeInState =false;

    private bool disableFade = true;
   /// <summary>
   /// 进入切屏
   /// </summary>
    public void FadeIn()
    {
        if(disableFade)
        {
            return;
        }
        UIUtility.HideShowAllUI(false);
        SwitchFadeEffect();
        fadePostProcess.FadeDown(false, null);
        isFadeInState = true;
       
    }
    /// <summary>
    /// 退出切屏
    /// </summary>
    public void FadeOut(System.Action callBack)
    {
        if (disableFade)
        {
            if(callBack!=null)
            {
                callBack.Invoke();
            }
            return;
        }

        if (!isFadeInState)
		{
           
            if (callBack != null)
            {
                UIUtility.HideShowAllUI(true);
                callBack.Invoke();
            }
            return;
		}
        isFadeInState = false;
        DOVirtual.DelayedCall(1.0f, () => {
            SwitchFadeEffect();
            fadePostProcess.FadeUp(false, null);
            DOVirtual.DelayedCall(1.0f, () => {

               
                if (callBack != null)
                {
                 
                    callBack.Invoke();
                }

                DOVirtual.DelayedCall(1.5f, () => {
              
            
                     UIUtility.HideShowAllUI(true);

                });

            });


        });



       

       
      
    }


}

