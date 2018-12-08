using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SystemCore.Task
{
    /// <summary>
    /// www场景下载任务
    /// </summary>
	public class SceneDownloadTask : TaskBase
    {
       
        /// <summary>
        /// 下载路径
        /// </summary>
        protected string _npath;
        /// <summary>
        /// 下载工具
        /// </summary>
        protected WWW _www;
        protected float _loadProgress;
        protected float _wwwProgress;

        public override float Progress
        {
            get
            {
                if (_www != null)
                {
                    _wwwProgress = _www.progress;
                }
                _progress = (_loadProgress + _wwwProgress) / 2;
                return _progress;
            }
        }

		public SceneDownloadTask(string id, string npath) : base(id)
        {
          
            this._npath = npath;
            _loadProgress = 0;
            _wwwProgress = 0;
        }

        public override IEnumerator Excute()
        {
            if (OnStart != null) { OnStart(); }
            Debug.Log("_npath="+ _npath);
            _www = new WWW(_npath);
            yield return _www;
            if (_www.isDone)
            {
                AssetBundleCreateRequest abcr = AssetBundle.LoadFromMemoryAsync(_www.bytes);
                while (!abcr.isDone)
                {

                    yield return 0;
                }
                string sceneName = FileUtils.GetFileName(_npath);
                if (abcr.assetBundle == null)
                {
                    Debug.LogError("场景加载出错，请检查：" + sceneName + ":path=" + _npath);

                    if (OnFinish != null)
                    {
                        OnFinish();
                    }
                    yield break;
                }
                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                if (asyncOperation == null)
                {
                   
                    Debug.LogError("场景加载出错，请检查：" + sceneName + ":path=" + _npath);
                    if (OnFinish != null)
                    {
                        OnFinish();
                    }
                    yield break;
                }
                asyncOperation.allowSceneActivation = true;
                while (true)
                {
                    if (asyncOperation.isDone)
                    {

                        SceneParse.DoSceneGameObject(sceneName, _id);
                        _loadProgress = 1;
                        if (OnFinish != null)
                        {
                            OnFinish();
                        }
                        break;
                    }
                    yield return 0;
                }

                //Debug.Log("======================================回调成功");
            }
        }

       

    }

   

    


}