using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public int columns = 4;
    public int rows = 4;

    private Room[,] grid;

    private void Start()
    {
        ///After the world has been generated assign the proper values
        for (int x = 0; x < columns; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                ///This is for setting the max connection values
                ///for the corner rooms. Value needs to be 2.
                if ((x == 0 || x == columns - 1) && (z == 0 || z == rows - 1))
                    grid[x, z].MaxConnections = 2;

                ///This is for setting the max connection values
                ///for the center rooms. Value needs to be 4.
                else if ((x > 0 && x < columns - 1) && (z > 0 && z < rows - 1))
                    grid[x, z].MaxConnections = 4;

                ///This is for setting the max connection values
                ///for the edge rooms. Value needs to be 3.
                else
                    grid[x, z].MaxConnections = 3;

            }
        }
    }

    public List<Room> Algo(Room startRoom, Room endRoom)
    {
        List<Room> frontier = new List<Room>();

        HashSet<Room> explored = new HashSet<Room>();

        frontier.Add(startRoom);

        foreach (Room room in grid)
        {
            room.gCost = 0;
        }

        while (frontier.Count > 0)
        {
            Room currentRoom = frontier[0];

            for (int i = 1; i < frontier.Count; i++)
            {
                if (frontier[i].fCost < currentRoom.fCost
                    || (frontier[i].fCost == currentRoom.fCost && frontier[i].hCost < currentRoom.hCost))
                {
                    currentRoom = frontier[i];
                }
            }

            frontier.Remove(currentRoom);
            explored.Add(currentRoom);

            if (currentRoom == endRoom)
            {
                return RetracePath(startRoom, endRoom);
            }

            foreach (Room neighbor in Neighbors(currentRoom))
            {
                if (explored.Contains(neighbor)) continue; 

                int currentCost = currentRoom.gCost;

                if (currentCost < neighbor.gCost || !frontier.Contains(neighbor))
                {
                    neighbor.gCost = currentCost;
                    neighbor.hCost = GetDistCost(neighbor, endRoom);
                    neighbor.parent = currentRoom;
                    if (!explored.Contains(neighbor)) frontier.Add(neighbor);
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Traces the path that we have created.
    /// </summary>
    /// <param name="startRoom"></param>
    /// <param name="endRoom"></param>
    /// <returns></returns>
    private List<Room> RetracePath(Room startRoom, Room endRoom)
    {
        List<Room> path = new List<Room>();
        
        Room currentRoom = endRoom;
        
        while (currentRoom != startRoom)
        {
            path.Add(currentRoom);
            currentRoom = currentRoom.parent;
        }
        
        path.Reverse();
        
        return path;
    }

    /// <summary> 
    /// Returns a list of the rooms surrounding the room we are looking at.
    /// </summary>
    /// <param name="room">The room whose neighbors we want to get.</param>
    /// <returns>A list of rooms that neighbor the <paramref name="room"/></returns>
    private List<Room> Neighbors(Room room)
    {
        List<Room> neighbors = new List<Room>();

        int gridPosX = (int)room.gridPosition.x;
        int gridPosZ = (int)room.gridPosition.y;

        ///Checking left
        if (gridPosX > 0) neighbors.Add(grid[gridPosX - 1, gridPosZ]);

        ///Checking right
        if (gridPosX < columns - 1) neighbors.Add(grid[gridPosX + 1, gridPosZ]);

        ///Checking up
        if (gridPosZ < rows - 1) neighbors.Add(grid[gridPosX, gridPosZ + 1]);

        ///Checking down
        if (gridPosZ > 0) neighbors.Add(grid[gridPosX, gridPosZ - 1]);

        return neighbors;
    }

    public int GetDistCost(Room roomA, Room roomB)
    {
        int distZ = (int)Mathf.Abs(roomA.gridPosition.y - roomB.gridPosition.y);
        int distX = (int)Mathf.Abs(roomA.gridPosition.x - roomB.gridPosition.x);

        if (distX > distZ) return 10 * distZ + 10 * (distX - distZ);
        else return 10 * distX + 10 * (distZ - distX);

    }
}
