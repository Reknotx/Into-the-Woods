using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will contain all the game's rooms,
/// to be referenced by WorldGenerator during world creation.
/// </summary>


[CreateAssetMenu]
public class WorldRoomContainer : ScriptableObject
{
    public GameObject spawnRoom;
    public List<GameObject> bossRooms = new List<GameObject>();
    public List<GameObject> roomPrefabs = new List<GameObject>();

}
