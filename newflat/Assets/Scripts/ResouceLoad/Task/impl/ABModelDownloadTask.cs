using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace SystemCore.Task
{
    /// <summary>
    /// ab模型包下载任务
    /// </summary>
	public class ABModelDownloadTask : TaskBase
    {
       
        /// <summary>
        /// 下载路径
        /// </summary>
        protected string _npath;
        /// <summary>
        /// 下载工具
        /// </summary>
        protected UnityWebRequest unityWebRequest;
        protected float _loadProgress;
        protected float _wwwProgress;

        public override float Progress
        {
            get
            {
                if (unityWebRequest != null)
                {
                    _wwwProgress = unityWebRequest.downloadProgress;
                }
                _progress = (_loadProgress + _wwwProgress) / 2;
                return _progress;
            }
        }

        private string code = string.Empty;
		public ABModelDownloadTask(string id, string fullpath,string code) : base(id)
        {
          
            this._npath = fullpath;
            this.code = code;
            _loadProgress = 0;
            _wwwProgress = 0;
        }

        public override IEnumerator Excute()
        {
            if (OnStart != null) { OnStart(); }


            Debug.Log("_npath="+ _npath);
            using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(_npath))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    log.Error(uwr.error);
                }
                else
                {
                    
                    AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
                    _loadProgress = 1;
                    _data = bundle.LoadAsset<GameObject>(code);
                    if (OnFinish != null)
                    {
                        OnFinish();
                    }



                }
            }

        }

    }


}