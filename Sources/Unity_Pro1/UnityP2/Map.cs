using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{ 
    public List<MapInfo> infos;
    public int mapindex;
    private void Start()
    {
        infos[mapindex].LoadMap();
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
