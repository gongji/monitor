﻿using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SceneContext {

    public static  Transform sceneBox;
    public static List<Object3dItem> sceneDataList;

    public static Object3dItem currentSceneData;

    public static int offestIndex = 0;
    //color and full  down builder
    public static string areaBuiderId = "";

    public static int FloorGroup = 0;

    public static CameraViewItem CurrrentcameraView = new CameraViewItem();

}
