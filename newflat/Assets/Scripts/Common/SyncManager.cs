using System;
using System.Collections.Generic;
using System.Threading;

namespace Br.Utils
{
    public class SyncManager : ISyncManager
    {
      
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

      
    
        public void Add(Action action)
        {
            rwlock.EnterWriteLock();
            queue.Enqueue(action);
            rwlock.ExitWriteLock();
        }
        public void Close()
        {
            rwlock.EnterWriteLock();
            queue.Clear();
            processCallback = null;
            rwlock.ExitWriteLock();
        }
    }
}