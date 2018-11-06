using UnityEngine;
using System.Collections;
using UnityEditor;

public class PrefabExport {

    /// <summary>
    /// prefeb 预制物导出
    /// </summary>
    [MenuItem("Tools/设备打包")]
    public static void ExportResource()
    {
        string path = EditorUtility.SaveFolderPanel("Save model", "assetbundles", "");
        if (path.Length != 0)
        {
            GameObject[] objects = Selection.gameObjects;
            foreach (GameObject obj in objects)
            {

                string objectPath = path + "/" + obj.name + ".unity3d";
                UnityEngine.Object[] selection = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
                // EditorTool.AddColliderScript(Selection.activeGameObject);
                BuildPipeline.BuildAssetBundle(Selection.activeObject, Selection.objects, objectPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows);

                // BuildPipeline.bui

                // public static bool BuildAssetBundle(Object mainAsset, Object[] assets, string pathName, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform);
                //Obsolete public static bool BuildAssetBundle(Object mainAsset, Object[] assets, string pathName, out uint crc, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform);



                //BuildPipeline.BuildAssetBundles("E:/output", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);

            }
            Debug.Log("export OK");
        }
    }
}
