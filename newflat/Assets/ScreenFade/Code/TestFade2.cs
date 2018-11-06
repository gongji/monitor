using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFade2 : MonoBehaviour {

	

    public Texture[] linesImages;

    public Texture damageImage;

    public GameObject currentScne;

    public GameObject loading;


    private bool isFlag = true;
    IFader f;
     void OnGUI()
    {
        

        if (GUI.Button(new Rect(10, 70, 50, 30), "start"))
        {
            isFlag = !isFlag;
            Fader.Instance.StopAllFadings();
            int count = System.Enum.GetNames(typeof(ScrreenFade)).Length;
            int randomValue = UnityEngine.Random.Range(1, count+1);
            InitFadeEffection(randomValue);

            f = Fader.Instance.FadeIn(1.0f);
            StartCoroutine(Wait());
           // Fader.Instance.SetColor(Color.black).FadeIn().Pause().FadeOut();


           // Fader.Instance.FadeIn(1.0f).StartCoroutine(this, "Wait");

            //加载场景
          //  Fader.Instance.FadeIn().Pause().LoadLevel(0).FadeOut(0);


           // Fader.Instance.LoadLevel(1);
         


          
        }
       
    }


    private void SetVisible()
     {
        
        currentScne.SetActive(isFlag);
        loading.SetActive(!isFlag);
        
     }
    private void ShowCurrrentScene()
     {

     }
    private IEnumerator Wait()
     {
         yield return new WaitForSeconds(1.5f);
         SetVisible();
         Debug.Log("携程完毕");
         f.FadeOut(1.0f);
     
     }

    private void InitFadeEffection(int randomValue)
     {
       //  Fader.SetupAsStripesFader(10, StripeScreenFader.Direction.HORIZONTAL_LEFT);
        // return;
         Fader.SetupAsSquaredFader(10);
         Fader.Instance.SetColor(Color.red);

         return;
         Debug.Log("randomValue=" + randomValue);
         if (randomValue == 1)
        {
            Debug.Log("SetupAsDefaultFader=");
            Fader.SetupAsDefaultFader();
        }
        else if (randomValue == 2)
        {
            Debug.Log("SetupAsSquaredFader=");
            Fader.SetupAsSquaredFader(10);
            Fader.Instance.SetColor(Color.red);
        }
        else if (randomValue == 3)
        {
            Debug.Log("SetupAsStripesFader=");
            Fader.SetupAsStripesFader(10, StripeScreenFader.Direction.HORIZONTAL_IN);
        }
        else if (randomValue == 4)
        {
            Debug.Log("LinesScreenFader.Direction.IN_UP_DOWN");
            Fader.SetupAsLinesFader(LinesScreenFader.Direction.IN_UP_DOWN, linesImages);
        }
        else if (randomValue == 5)
        {
            Debug.Log("damageImage");
            Fader.SetupAsImageFader(damageImage);
        }
     }

}


public enum ScrreenFade
{
    //默认的颜色
    DefaultEffect = 1,
    //方块 
    SquaredEffect = 2,
    StripedEffect = 3,
    LinesEffect = 4,
    ImageEffect = 5

}