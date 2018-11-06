
using System;
using System.Timers;

namespace Br.Utils
{
    public class LazyExecUtility
    {
        private Timer time = null;
        private Action action = null;

        private SyncManager syncManager = null;

        public LazyExecUtility() {

        }

        public LazyExecUtility(SyncManager syncManager)
        {
            this.syncManager = syncManager;
        }

        public void CancelExec() {
            if (time != null)
            {
                time.Stop();
                time.Close();
                time.Dispose();
                time = null;
            }
            action = null;
        }

        public void Exec(Action action, int lazyTime) {
            CancelExec();
            this.action = action;
            time = new Timer(lazyTime);
            time.AutoReset = false;
            //time.Interval = lazyTime;
            time.Elapsed += ExecAction;
            time.Start();
        }

        private void ExecAction(object sender, ElapsedEventArgs e) {
            if (syncManager != null)
            {
                syncManager.Add(() =>
                {
                    if (action != null)
                    {
                        action();
                        action = null;
                    }
                });
            }
            else {
                if (action != null)
                {
                    action();
                    action = null;
                }
            }

            if (time != null)
            {
                time.Close();
                time.Dispose();
                time = null;
            }
        }
    }
}
