using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//�Q�l:https://kan-kikuchi.hatenablog.com/entry/Custom_Button/
[CustomEditor(typeof(MapManager))]//�g������N���X���w��
public class ExampleScriptEditor : Editor
{

    /// <summary>
    /// Inspector��GUI���X�V
    /// </summary>
    public override void OnInspectorGUI()
    {
        //����Inspector������\��
        base.OnInspectorGUI();

        //target��ϊ����đΏۂ��擾
        MapManager mapScript = target as MapManager;

        //PublicMethod�����s����p�̃{�^��
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
            Debug.LogWarning("Map�Ȃ��Object�͂��Ƃ��Ƒ��݂��܂���ł����B\n�܂������ǂݍ��܂�Ă��Ȃ��\��������܂��B");
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
            Debug.LogWarning("Map�Ȃ��Object�͂��Ƃ��Ƒ��݂��܂���ł����B\n�܂������ǂݍ��܂�Ă��Ȃ��\��������܂��B");
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
