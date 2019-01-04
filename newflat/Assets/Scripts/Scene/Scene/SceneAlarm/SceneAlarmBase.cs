using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneAlarmBase : MonoBehaviour {

    public string sceneId = string.Empty;
    public abstract void Alarm();
    public abstract void Restore();
}
