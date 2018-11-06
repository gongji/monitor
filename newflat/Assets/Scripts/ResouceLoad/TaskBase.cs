
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Core.Common.Logging;

namespace SystemCore.Task
{
    /// <summary>
    /// 任务基类
    /// </summary>
    public abstract class TaskBase : ITask
    {
        protected string _id;
        protected float _progress;
        protected object _data;
        protected ILog log;

        public TaskBase(string id)
        {
            log = LogManagers.GetLogger(GetType().ToString());
            this._id = id;
            this._progress = 0;
        }



        public virtual object Data { get { return _data; } }
        public virtual float Progress { get { return _progress; } }
        public string ID { get { return _id; } }
        public Action OnStart { get; set; }
        public Action OnFinish { get; set; }
        public Action<string> OnError { get; set; }

        public abstract IEnumerator Excute();


        public class IOThreadPool
        {
            public bool finishFlg = false;
            static private bool flgInitThreadPool;

            public IOThreadPool(string lpath, byte[] bytes, Action<string, byte[]> func)
            {
                if (!flgInitThreadPool)
                {
                    ThreadPool.SetMaxThreads(20, 20);
                    flgInitThreadPool = true;
                }

                ThreadPool.QueueUserWorkItem((object state) => {

                    func(lpath, bytes);
                    //Debug.Log("线程id :" + Thread.CurrentThread.ManagedThreadId.ToString());
                    finishFlg = true;
                });
            }

            public IEnumerator WaitForFinish()
            {
                while (finishFlg == false)
                {
                    yield return 0;
                }
                yield break;
            }
        }




        public class IOThread
        {
            Thread th;
            public bool finishFlg = false;
            public IOThread(string lpath, byte[] bytes, Action<string, byte[]> func)
            {
                th = new Thread(() => {
                    func(lpath, bytes);
                    finishFlg = true;
                    //Debug.Log("现成关闭了");
                    System.Threading.Thread.CurrentThread.Abort();
                });
                th.IsBackground = false;
                th.Start();
            }

            public IEnumerator WaitForFinish()
            {
                while (finishFlg == false)
                {
                    yield return 0;
                }
                yield break;
            }
        }

    }
}