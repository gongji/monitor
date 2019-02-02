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
    /// ab model task
    /// </summary>
	public class TexturelDownloadTask : TaskBase
    {
        protected string _npath;
      
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

		public TexturelDownloadTask(string id, string npath) : base(id)
        {
          
            this._npath = npath;
            _loadProgress = 0;
            _wwwProgress = 0;
        }

        public override IEnumerator Excute()
        {
            if (OnStart != null) { OnStart(); }


            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(_npath))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    log.Error(uwr.error);
                }
                else
                {
                    
                    Texture2D texture2d = DownloadHandlerTexture.GetContent(uwr);
                    _loadProgress = 1;
                    _data = texture2d;
                    if (OnFinish != null)
                    {
                        OnFinish();
                    }
                }
            }

        }

    }


}