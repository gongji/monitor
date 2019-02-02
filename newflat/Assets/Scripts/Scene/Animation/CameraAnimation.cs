using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraAnimation
{
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

            camera.GetComponent<CameraObjectController>().SetCameraPostion();



        });
    }

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
