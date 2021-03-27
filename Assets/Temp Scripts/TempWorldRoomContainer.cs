using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class TempWorldRoomContainer : ScriptableObject
{
    public List<GameObject> roomPrefabs = new List<GameObject>();
    public GameObject spawnRoom;
    public GameObject bossRoom;
}
