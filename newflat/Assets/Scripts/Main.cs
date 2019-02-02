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

    private InitScripts initScripts = null;
    void Start () {

        initScripts = gameObject.AddComponent<InitScripts>();
        initScripts.AddScirpts();
        //load config
        Config.Startload(this,
            ()=>{

                //check scene change
                SceneData.IsExistNewScene((result) => {

                    //no change
                    if(!result)
                    {
                        //int scene data
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

            if(initScripts!=null)
            {
                initScripts.InitDataAndLogin();
            }

           
           
        });
    }

	void Update () {

        if ((EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()))
        {
           // Debug.Log("ui");
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
