using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

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
        string path = Application.streamingAssetsPath + "/UI/fadebg.png";
        ResourceUtility.Instance.GetHttpTexture(path, (result) => {

            GetComponent<Transform>().localScale = Vector3.zero;
            UIUtility.CreateSprite((Texture2D)result, GetComponent<Image>(), Vector2.zero);
        });
    }

    private void Update()
    {
       
    }

    private bool isFadeInState =false;
   /// <summary>
   /// 进入切屏
   /// </summary>
    public void FadeIn()
    {
        transform.SetAsLastSibling();
        //Fader.Instance.StopAllFadings();
        //int count = System.Enum.GetNames(typeof(ScrreenFade)).Length;
        //int randomValue = UnityEngine.Random.Range(1, count + 1);
        //InitFadeEffection(randomValue);
        //Fader.Instance.FadeIn(fadeInTime);
        isFadeInState = true;
        UIUtility.HideShowAllUI(false);
    }
    /// <summary>
    /// 退出切屏
    /// </summary>
    public void FadeOut(System.Action callBack)
    {
		if(!isFadeInState)
		{
            if (callBack != null)
            {
                UIUtility.HideShowAllUI(true);
                callBack.Invoke();
            }
            return;
		}
        //Fader.Instance.FadeOut(fadeOutTime);
        
		isFadeInState = false;
        DOVirtual.DelayedCall(fadeOutTime,()=> {
            if(callBack!=null)
            {
                UIUtility.HideShowAllUI(true);
                callBack.Invoke();
            }

        });
      
    }
     private void InitFadeEffection(int randomValue)
     {
         
        //if (randomValue == 1)
        //{
        //   // Debug.Log("SetupAsDefaultFader=");
        //    Fader.SetupAsDefaultFader();
        //}
        //else if (randomValue == 2)
        //{
        //   // Debug.Log("SetupAsSquaredFader=");
        //    Fader.SetupAsSquaredFader(10);
        //    Fader.Instance.SetColor(Color.red);
        //}
        //else if (randomValue == 3)
        //{
        //   // Debug.Log("SetupAsStripesFader=");
        //    Fader.SetupAsStripesFader(10, StripeScreenFader.Direction.HORIZONTAL_IN);
        //}
        //else if (randomValue == 4)
        //{
        //  //  Debug.Log("LinesScreenFader.Direction.IN_UP_DOWN");
        //    Fader.SetupAsLinesFader(LinesScreenFader.Direction.IN_UP_DOWN, linesImages);
        //}
        //else if (randomValue == 5)
        //{
        //   // Debug.Log("damageImage");
        //    Fader.SetupAsImageFader(damageImage);
       // }
     }

}

