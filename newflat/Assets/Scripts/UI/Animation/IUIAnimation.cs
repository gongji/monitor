using UnityEngine;
using System.Collections;
using System;

namespace animation {
    /// <summary>
    /// UI打开关闭动画
    /// </summary>
    public interface IUIAnimation {
        /// <summary>
        /// 显示动画
        /// </summary>
        void EnterAnimation(System.Action onComplete);

        /// <summary>
        /// 隐藏动画
        /// </summary>
        void ExitAnimation(System.Action onComplete);

        /// <summary>
        /// 重置动画
        /// </summary>
        void ResetAnimation();
    }
}