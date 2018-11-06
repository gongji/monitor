using UnityEngine;
using State;


public class Main : MonoBehaviour {

	public static Main instance { get; private set; }
	[HideInInspector]
    public StateMachineManager stateMachineManager = new StateMachineManager();
    
    void Awake()
    {
    	instance = this;
       
    }
	void Start () {
		///end
        gameObject.AddComponent<DownLoader>();
        gameObject.AddComponent<MouseCheck>();
        gameObject.AddComponent<CameraViewManager>();
        //加载配置文件
        Config.Startload(this,
            ()=>{

                SceneData.Init3dObjectData(() => {
                    //Main.instance.stateMachineManager.SwitchStatus<RoomState>("juliusuo_sn_f1_fj1");
                });

                BRPlatform bRPlatform = AppInfo.Platform;
                if(bRPlatform == BRPlatform.Browser)
                {
                    stateMachineManager.SetAppState<BrowseStatus>();
                }
                else
                {
                    stateMachineManager.SetAppState<EditStatus>();
                }
              

            });

	}
	
	// Update is called once per frame
	void Update () {
		 stateMachineManager.OnUpdate();
	}
	
	void FixedUpdate()
     {
         stateMachineManager.OnFixedUpdate();
     }
     void LateUpdate()
     {
         stateMachineManager.OnLateUpdate();
     }
}
