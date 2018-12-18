using UnityEngine;
using State;
using UnityEngine.EventSystems;

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
        gameObject.AddComponent<CameraViewChangeManager>();
        //加载配置文件
        Config.Startload(this,
            ()=>{

                //查询场景是否有变化
                SceneData.IsExistNewScene((result) => {

                    //无新的场景
                    if(!result)
                    {
                        //初始化场景数据
                        Init();
                    }

                    else
                    {
                        SceneData.UpdateSceneData(() => {
                            Init();
                        });
                    }
                    
                });
            });

	}

    private void Init()
    {
        SceneData.Init3dAllObjectData(() => {
            BRPlatform bRPlatform = AppInfo.Platform;
            if (bRPlatform == BRPlatform.Browser)
            {
                stateMachineManager.SetAppState<BrowseStatus>();
            }
            else
            {
                stateMachineManager.SetAppState<EditStatus>();
            }
            //StartCoroutine(WebsocjetService.Instance.StartWebSocket());
            CameraInitSet.SystemInitCamera();
            Battlehub.UIControls.TreeViewControl.Instance.Init();
            ModelData.InitModelData();
            //FlyingTextMsg.instance.LoadFontAsset();



        });
    }

	void Update () {

        if ((EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()))
        {
            return;
        }

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
