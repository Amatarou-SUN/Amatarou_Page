using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//参考:https://kan-kikuchi.hatenablog.com/entry/Custom_Button/
[CustomEditor(typeof(MapManager))]//拡張するクラスを指定
public class ExampleScriptEditor : Editor
{

    /// <summary>
    /// InspectorのGUIを更新
    /// </summary>
    public override void OnInspectorGUI()
    {
        //元のInspector部分を表示
        base.OnInspectorGUI();

        //targetを変換して対象を取得
        MapManager mapScript = target as MapManager;

        //PublicMethodを実行する用のボタン
        if (GUILayout.Button("Load Map (Index = Mapindex)"))
        {
            mapScript.Load();
        }
    }

}
public class MapManager : MonoBehaviour
{
    public List<MapInfo> infos;
    public int mapindex;
    private GameObject LoadedMap;
    public void Load()
    {
        try
        {
            GameObject.DestroyImmediate(GameObject.Find("Map"));
        }
        catch
        {
            Debug.LogWarning("MapなんてObjectはもともと存在しませんでした。\nまだ一回も読み込まれていない可能性があります。");
        }
        LoadedMap = infos[mapindex].LoadMap();
    }
    public void Load(int index)
    {
        try
        {
            GameObject.DestroyImmediate(GameObject.Find("Map"));
        }
        catch
        {
            Debug.LogWarning("MapなんてObjectはもともと存在しませんでした。\nまだ一回も読み込まれていない可能性があります。");
        }
        LoadedMap = infos[index].LoadMap();
    }
}

[System.Serializable]
public class MapInfo
{
    public GameObject Instantiate_GameObject;
    public Transform Instantiate_Transform;
    public GameObject LoadMap()
    {
        GameObject InstansiateObject = GameObject.Instantiate(Instantiate_GameObject, Instantiate_Transform.position, Instantiate_Transform.rotation);
        InstansiateObject.name = "Map";
        return InstansiateObject;
    }
}

[System.Serializable]
public class MapInfos : List<MapInfo>
{
    public GameObject LoadMapIndex(int Index)
    {
        return this[Index].LoadMap();
    }
}
