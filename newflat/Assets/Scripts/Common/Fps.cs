using UnityEngine;
using System.Collections;

public class Fps : MonoSingleton<Fps>
{
    //private float updateInterval = 0.5f;  
    //private float accum = 0.0f;   
    //private float frames = 0;   
    //private float timeleft;   
    //private int fpsCount = 0;


    private int fpsCount = 0;

    private float curTime;
    private int curCount;

    public int FPS
    {
        get { return fpsCount; }
    }
 
    void Start()  
    {  
        //timeleft = updateInterval;  
        curTime = 0.0f;
        curCount = 0;
    }  

    void OnGUI () 
    {
    
         string isFPS = Config.parse("isFPS");

        if (isFPS.Equals("1"))
        {
            return;
        }

        GUIStyle style = new GUIStyle(); //实例化一个新的GUIStyle，名称为style ，后期使用
        style.fontSize = 20; //字体的大小设置数值越大，字越大，默认颜色为黑色 
        style.normal.textColor = new Color(0, 2, 4); //设置文本的颜色为 新的颜色(0,0,0)修改值-代表不同的颜色,值为整数 我个人觉得有点像RGB的感觉
      //  GUI.Label(new Rect(10, 10, 200, 20), "test", style); 


        GUI.Label(new Rect((int)(Screen.width * 0.5), (int)(Screen.height * 0.5), 500, 500), fpsCount.ToString() +"帧", style);
    }
  
    void Update()  
    {  
        //timeleft -= Time.deltaTime;  
        //accum += Time.timeScale / Time.deltaTime;  
        //++frames;  
  
        //if (timeleft <= 0.0)  
        //{  
        //    fpsCount = (int)(accum/frames);  
        //    timeleft = updateInterval;  
        //    accum = 0.0f;  
        //    frames = 0;  
       
        
        curTime += Time.deltaTime;
        curCount += 1;

        fpsCount = (int)(curCount / curTime);

        if (curTime >= 1.0f) {
            curTime = 0;
            curCount = 0;
        }
    }  
}  

