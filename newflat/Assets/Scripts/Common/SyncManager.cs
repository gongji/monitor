using System;
using System.Collections.Generic;
using System.Threading;

namespace Br.Utils
{
    /// <summary>
    /// Action同步器
    /// <para>
    ///     在多线程交互的情况下使方法都在主线程处理
    ///     需要在所属MonoBehaviour的Update方法中调用Exec方法
    /// </para>
    /// </summary>
    public class SyncManager : ISyncManager
    {
        /// <summary>
        /// 队列
        /// </summary>
        private Queue<Action> queue;
        private readonly ReaderWriterLockSlim rwlock = new ReaderWriterLockSlim();
        private Action<Action> processCallback;

        public SyncManager() : this(null)
        {

        }

        public SyncManager(Action<Action> processCallback) {
            queue = new Queue<Action>();
            if (processCallback == null) {
                processCallback = (a) =>
                {
                    a();
                };
            }
            this.processCallback = processCallback;
        }

        /// <summary>
        /// 需要在所属MonoBehaviour的Update方法中调用
        /// </summary>
        public void Exec()
        {
            int len;
            rwlock.EnterWriteLock();
            len = queue.Count;
            rwlock.ExitWriteLock();
            while (len>0)
            {
                processCallback(queue.Dequeue());
                len--;
            }
        }

        /// <summary>
        /// 增加要执行的Action
        /// </summary>
        /// <param name="action"></param>
        public void Add(Action action)
        {
            rwlock.EnterWriteLock();
            queue.Enqueue(action);
            rwlock.ExitWriteLock();
        }

        /// <summary>
        /// 增加要执行的Action
        /// </summary>
        /// <param name="action"></param>
        public void Close()
        {
            rwlock.EnterWriteLock();
            queue.Clear();
            processCallback = null;
            rwlock.ExitWriteLock();
        }
    }
}