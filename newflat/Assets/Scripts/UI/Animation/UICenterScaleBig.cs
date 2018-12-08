using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace animation
{
    /// <summary>
    /// 从中心点的缩放动画
    /// </summary>
    public class UICenterScaleBig : IUIAnimation
    {
        private GameObject target;
        private float duration;

        public UICenterScaleBig(GameObject target, float duration = 0.2f)
        {
            this.target = target;
            this.duration = duration;
        }

        public void EnterAnimation(System.Action callBack)
        {
            MaskManager.Instance.Show();
            target.transform.localScale = Vector3.zero;
            target.transform.SetAsLastSibling();
            target.transform.DOScale(Vector3.one, duration).OnComplete(() => {

                if (callBack != null)
                {
                    callBack.Invoke();
                }
            });

        }

        public void ExitAnimation(System.Action callBack)
        {
            MaskManager.Instance.Hide();
            target.transform.localScale = Vector3.one;
            target.transform.DOScale(Vector3.zero, duration).OnComplete(() => {

                if (callBack != null)
                {
                    callBack.Invoke();
                }

            });

        }

        public void ResetAnimation()
        {

        }

    }
}
