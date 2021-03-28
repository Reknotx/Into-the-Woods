using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWorldGenerator : MonoBehaviour
{
    public TempWorldRoomContainer roomContainer;

    private int columns = 4;
    private int rows = 4;

    public GameObject[,] grid;

    void Start()
    {
        grid = new GameObject[4, 4];
        GenerateRooms();
    }


    void GenerateRooms()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                Vector3 pos = new Vector3(x * 20, 0f, z * 20);

                GameObject temp = Instantiate(roomContainer.roomPrefabs[Random.Range(0, roomContainer.roomPrefabs.Count)],
                                              pos,
                                              Quaternion.identity);

                grid[x, z] = temp;
            }
        }


    }

}
