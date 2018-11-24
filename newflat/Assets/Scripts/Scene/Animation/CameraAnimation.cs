using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraAnimation
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="targetPostion"></param>
    /// <param name="targetRotation"></param>
    /// <param name="duringTime"></param>
    /// <param name="callBack"></param>
    /// <param name="ease"></param>
    public static void CameraMove(Camera camera, Vector3 targetPostion, Vector3 targeteulerAngles, float duringTime, System.Action callBack,
        DG.Tweening.Ease ease = Ease.OutQuart)
    {
        camera.orthographic = false;
        Sequence mySequence = DOTween.Sequence();

        Tweener move = camera.transform.DOMove(targetPostion, duringTime);
        move.SetEase(ease);

        Tweener roation = camera.transform.DOLocalRotate(targeteulerAngles, duringTime);
        roation.SetEase(ease);

        mySequence.Append(move);
        mySequence.OnUpdate(()=> {
          //  coc.SetCamerRotation();
           // coc.SetCameraPostion();

        });
        mySequence.Join(roation);
        mySequence.OnComplete(() =>
        {
            if (callBack != null)
            {
                callBack.Invoke();
            }

            

        });
    }


    /// <summary>
    /// 将centerPostion拉到屏幕中心点，相机移动过去
    /// </summary>
    /// <param name="centerPostion">拉取得点</param>
    /// <param name="duringTime"></param>
    /// <param name="callBack"></param>
    public static void RotationScreenCenter(Vector3 centerPostion,float duringTime,System.Action<Vector3,float> callBack)
    {
        Vector3 relativePos = centerPostion - Camera.main.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Camera.main.transform.DORotate(rotation.eulerAngles, duringTime).OnComplete(() => {
            if (callBack != null)
            {
                callBack.Invoke(centerPostion, duringTime);
            }

        });
    }
}
