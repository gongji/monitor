#if UNITY_EDITOR
using System;
using UnityEditor;

namespace Br.Utils
{

    public class FinishCompiling : EditorWindow {

        private Action action = null;

        void Update()
        {
            if (EditorApplication.isCompiling)
            {
                if (action != null) {
                    action();
                }
            }
        }

        public void SetCallBack(Action action) {
            this.action = action;
        }
    }

}
#endif