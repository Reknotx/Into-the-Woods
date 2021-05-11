using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class WorldGenerator : SingletonPattern<WorldGenerator>
{
    public int WorldRows; // Just the number of rows the world's rooms will be arranged in.
    public int WorldColumns; // Same for columns.
    public int minimumJourney; // The minimum distance between the home and boss tiles.
    public float WorldXScale; // Scale of grid on the X axis. Used for instantiating the game objects.
    public float WorldZScale; // Scale of grid on the Z axis.
    private Transform WorldRoomsParent; // Empty object that will be used for organization in the hierarchy. All rooms parented to this!

    public bool manualSeed; // Do you want to force a certain seed?
    public int seed; // What exact randomization seed to use.

    public WorldRoomContainer roomContainer; // The ScriptableObject that contains all of our room lists.

    // Borders for the world.
    public GameObject NormalSizeBorder;
    public GameObject AdventureSizeBorder;

    private List<GameObject> FieldRoomPicks = new List<GameObject>(); // The current list of picked field rooms for this world.
    //public GameObject[,] roomArrange; // Our selected rooms to instantiate, in the arrangement we're building.
    private GameObject[,] roomInstances; // The rooms we've instantiated as based FieldRoomPicks.
    private Room[,] roomScripts; // The instances of roomInstances' scripts, "Room".

    private Room spawnRoom; // The instance of the spawn Room.
    private Room bossRoom; // The instance of the boss Room.

    private List<Room> criticalPath = new List<Room>();
    private List<Room> branchingPaths = new List<Room>();

    public TMPro.TMP_Text debug_SeedView; // Debug canvas that displays the seed for sake of playtesters.

    protected override void Awake()
    {
        base.Awake();

        PrefsCheck();
        // Check the PlayerPrefs for any special modifiers to change the parameters.

        SafetyCheck();
        // Ensures that variables aren't set to values that will cause errors.

        if (manualSeed)
            Random.InitState(seed);
        else
        {
            seed = Random.Range(0, 100000000);
            Random.InitState(seed);
        }

        // Debug view
        if (debug_SeedView != null)
            debug_SeedView.text = "Seed: " + seed + ".";

        GenerateRoomList();

        GeneratePath(spawnRoom, bossRoom);

        for (int i = 0; i < 3; i++)
        {
            int attempts = 0;
            while (true)
            {
                int x = Random.Range(0, WorldColumns);
                int y = Random.Range(0, WorldColumns);
                if (roomScripts[x, y] != bossRoom && !criticalPath.Contains(roomScripts[x, y]) && !branchingPaths.Contains(roomScripts[x, y]))
                {
                    GeneratePath(spawnRoom, roomScripts[x, y]);
                    break;
                }

                attempts++;

                if (attempts >= (WorldColumns * WorldRows))
                {
                    break;
                }
            }
        }

        //Debug.Log(criticalPath.Count);
        //Debug.Log(branchingPaths.Count);
        ConnectEverything();

        RemoveDoors();

        BuildBorder();
    }

    #region Setup
    /// Author: Paul Hernandez
    /// Date: 3/30/2021
    /// <summary>
    /// Gets preferences from local files using Unity's PlayerPrefs.
    /// </summary>
    void PrefsCheck()
    {
        WorldRows = PlayerPrefs.GetInt("PWorldRows", 4);
        WorldColumns = PlayerPrefs.GetInt("PWorldColumns", 4);

        //WorldRows = 4;
        //WorldColumns = 4;

        print("Seeds are currently being manually set in WorldGenerator. Uncomment the comment block in PrefsCheck to enable PlayerPrefs seed entry.");
        // CURRENTLY TURNED OFF
        /*
        if (PlayerPrefs.GetInt(PrefTags.UseSeed, 0) == 1)
        {
            manualSeed = true;
            seed = PlayerPrefs.GetInt(PrefTags.Seed);
        }
        else
        {
            manualSeed = false;
        }
        */
    }

    /// Author: Paul Hernandez
    /// Date: 2/5/2021
    /// <summary>
    /// Makes sure you're not using variables that will give errors or crashes. 
    /// </summary>
    void SafetyCheck()
    {
        WorldRoomsParent = new GameObject("WorldRoomsParent").transform;

        //roomArrange = new GameObject[WorldColumns, WorldRows];
        roomInstances = new GameObject[WorldColumns, WorldRows];
        roomScripts = new Room[WorldColumns, WorldRows];

        if (WorldXScale == 0)
        {
            WorldXScale = 1;
            Debug.Log("WorldXScale set to " + WorldXScale);
        }
        if (WorldZScale == 0)
        {
            WorldZScale = 1;
            Debug.Log("WorldZScale set to " + WorldZScale);
        }
        if (WorldRows < 1)
        {
            WorldRows = 1;
            Debug.Log("WorldRows set to " + WorldRows);
        }
        if (WorldColumns < 1)
        {
            WorldColumns = 1;
            Debug.Log("WorldColumns set to " + WorldColumns);
        }
        if (minimumJourney > (WorldColumns + WorldRows - 2))
        {
            minimumJourney = 1;
            Debug.Log("minimumJourney is too large! Set to " + minimumJourney);
        }

    }
    #endregion

    #region MainWorldGeneration
    /// Author: Paul Hernandez
    /// Date: 2/4/2021
    /// <summary>
    /// This should generate a list of room prefabs to use, then place everything.
    /// This specific function might get rewritten to be cleaner.
    /// STILL NEEDS WALL/DOOR GENERATION.
    /// </summary>
    public void GenerateRoomList()
    {
        // Step 1: Select random Field rooms.
        // Choose random prefabs to use for the field rooms. ("Field" meaning not spawn or boss room.)
        // Our choices will be stored in FieldRoomPicks.

        for (int i = 0; i < ((WorldRows * WorldColumns ) - 2); i++)
        {
            while (true)
            {
                int index = Random.Range(0, roomContainer.roomPrefabs.Count);

                if (!FieldRoomPicks.Contains(roomContainer.roomPrefabs[index]))
                {
                    if (roomContainer.roomPrefabs[index] != null)
                    {
                        FieldRoomPicks.Add(roomContainer.roomPrefabs[index]);
                        break;
                    }
                    else
                    {
                        // Adding an error message on null entries because we keep losing 
                        // our level prefabs SOMEHOW. - Paul, 5/4/2021.
                        print("roomPrefabs[" + index + "] was null. Did you forget some room prefabs?");
                        i--; // Subtract loop counter so we try again.
                        break;
                    }
                }
            }
        }


        // =====================================================================
        // Step 2: Instantiate Spawn and Boss rooms.
        // Find viable positions for the two rooms. Then, instantiate them.
        // Place their script instances in an array.

        Vector2Int intendedStart = new Vector2Int();
        Vector2Int intendedEnd = new Vector2Int();
        do
        {
            intendedStart = new Vector2Int(Random.Range(0, WorldColumns), Random.Range(0, WorldRows));
            intendedEnd = new Vector2Int(Random.Range(0, WorldColumns), Random.Range(0, WorldRows));

        } while (((Mathf.Abs(intendedStart.x - intendedEnd.x)) + (Mathf.Abs(intendedStart.y - intendedEnd.y))) < minimumJourney);

        // Instantiate them both immediately!
        // Link the instances to the new vars.
        // Place them in array RoomInstances.

        GameObject temp = null;

        // Spawn Room.
        temp = Instantiate(roomContainer.spawnRoom, new Vector3(intendedStart.x * WorldXScale, 0f, intendedStart.y * WorldZScale), Quaternion.identity);
        roomInstances[intendedStart.x, intendedStart.y] = temp;
        roomInstances[intendedStart.x, intendedStart.y].transform.SetParent(WorldRoomsParent);
        spawnRoom = temp.GetComponentInChildren<Room>();
        spawnRoom.gridPosition = intendedStart;

        // Boss Room.
        temp = Instantiate(roomContainer.bossRooms[Random.Range(0, roomContainer.bossRooms.Count)], new Vector3(intendedEnd.x * WorldXScale, 0f, intendedEnd.y * WorldZScale), Quaternion.identity);
        roomInstances[intendedEnd.x, intendedEnd.y] = temp;
        roomInstances[intendedEnd.x, intendedEnd.y].transform.SetParent(WorldRoomsParent);
        bossRoom = temp.GetComponentInChildren<Room>();
        bossRoom.gridPosition = intendedEnd;
        bossRoom.IsBossRoom = true;


        // =====================================================================
        // Step 3: Instantiate Field rooms randomly.
        
        ///Run through FieldRoomPicks until empty
        while (FieldRoomPicks.Count != 0)
        {
            int index = Random.Range(0, FieldRoomPicks.Count());

            while (true)
            {
                int x = Random.Range(0, WorldRows);
                int y = Random.Range(0, WorldColumns);

                if (roomInstances[x, y] == null)
                {
                    GameObject temp2 = null;
                    ///Instantiate the obj and store the reference
                    ///Do NOT get the script reference yet
                    ///DO adjust positioning in world as is needed
                    temp2 = Instantiate(FieldRoomPicks[index], new Vector3(x * WorldXScale, 0f, y * WorldZScale), Quaternion.identity);
                    roomInstances[x, y] = temp2;
                    roomInstances[x, y].transform.SetParent(WorldRoomsParent);
                    FieldRoomPicks.RemoveAt(index);
                    break;
                }
            }
        }
        

        ///Spawn in the rest of the rooms and do rest of work.
        for (int x = 0; x < WorldRows; x++)
        {
            for (int y = 0; y < WorldColumns; y++)
            {
                ///Obtain script reference and store in roomScripts array
                ///Then do the rest of the finalization 
                ///
                roomScripts[x, y] = roomInstances[x, y].transform.GetChild(1).GetComponent<Room>();
                roomScripts[x, y].gridPosition = new Vector2(x, y);
                
            }
        }
    }

    #endregion


    #region AStar
    ///<summary>Generates the path.</summary>
    public void GeneratePath(Room startRoom, Room endRoom)
    {
        ///Let's start with the crit path to test
        //paths.Add("Main Path", Algo(startRoom, endRoom));
        //Debug.Log("Start room: " + startRoom.gameObject.transform.parent.name);
        //Debug.Log("End room: " + endRoom.gameObject.transform.parent.name);

        List<Room> tempList = Algo(startRoom, endRoom);

        if (endRoom == bossRoom)
        {
            criticalPath.AddRange(tempList);

            foreach (Room room in criticalPath)
            {
                if (room.gameObject.TryGetComponent(out NightRoom night))
                {
                    night.gameObject.GetComponent<NightRoom>().enabled = false;
                }
            }
        }
        else
        {
            branchingPaths.AddRange(tempList);
        }

        for (int i = 0; i < tempList.Count; i++)
        {
            Room currRoom = tempList[i];
            //Debug.Log("Current room grid position " + currRoom.gridPosition.ToString());

            if (i < tempList.Count - 1)
            {
                ///A next room exists
                Room nextRoom = tempList[i + 1];
                ConnectRooms(currRoom, nextRoom);
            }
        }
    }


    public List<Room> Algo(Room startRoom, Room endRoom)
    {
        List<Room> frontier = new List<Room>();

        HashSet<Room> explored = new HashSet<Room>();

        frontier.Add(startRoom);

        foreach (Room room in roomScripts)
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

                    if (neighbor == bossRoom && endRoom != bossRoom)
                    {
                        continue;
                    }
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

        if (currentRoom == startRoom)
        {
            path.Add(currentRoom);
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
        if (gridPosX > 0) neighbors.Add(roomScripts[gridPosX - 1, gridPosZ]);

        ///Checking right
        if (gridPosX < WorldColumns - 1) neighbors.Add(roomScripts[gridPosX + 1, gridPosZ]);

        ///Checking up
        if (gridPosZ < WorldRows - 1) neighbors.Add(roomScripts[gridPosX, gridPosZ + 1]);

        ///Checking down
        if (gridPosZ > 0) neighbors.Add(roomScripts[gridPosX, gridPosZ - 1]);

        return neighbors;
    }

    public int GetDistCost(Room roomA, Room roomB)
    {
        int distZ = (int)Mathf.Abs(roomA.gridPosition.y - roomB.gridPosition.y);
        int distX = (int)Mathf.Abs(roomA.gridPosition.x - roomB.gridPosition.x);

        if (distX > distZ) return 10 * distZ + 10 * (distX - distZ);
        else return 10 * distX + 10 * (distZ - distX);

    }


    public void ConnectEverything()
    {
        for (int x = 0; x < WorldColumns; x++)
        {
            for (int y = 0; y < WorldRows; y++)
            {
                Room currRoom = roomScripts[x, y];
                bool connectionMade = false;

                if (criticalPath.Contains(currRoom) || branchingPaths.Contains(currRoom))
                {
                    continue;
                }

                List<Room> neighbors = Neighbors(currRoom);

                foreach (Room neighbor in neighbors)
                {
                    if (neighbor == bossRoom) continue;

                    if (criticalPath.Contains(neighbor) || branchingPaths.Contains(neighbor))
                    {
                        ConnectRooms(currRoom, neighbor);
                        branchingPaths.Add(currRoom);
                        connectionMade = true;
                        break;
                    }
                }

                if (connectionMade == false)
                {
                    int index = Random.Range(0, neighbors.Count);

                    ConnectRooms(currRoom, neighbors[index]);
                }

            }
        }
    }

    public void RemoveDoors()
    {
        for (int x = 0; x < WorldColumns; x++)
        {
            for (int y = 0; y < WorldRows; y++)
            {
                roomScripts[x, y].RemoveDoors();
            }
        }
    }

    /// <summary>
    /// Connects two rooms together based on their grid position.
    /// </summary>
    /// <param name="currRoom"></param>
    /// <param name="nextRoom"></param>
    private void ConnectRooms(Room currRoom, Room nextRoom)
    {
        if (nextRoom.gridPosition.x == currRoom.gridPosition.x)
        {
            ///On same Column so we need to look at
            ///north or south
            if (currRoom.gridPosition.y > nextRoom.gridPosition.y)
            {
                ///Current room is above the next room so
                ///The south door of the current room and the north
                ///door of the next are marked as true
                currRoom.connections[Room.Direction.South] = true;
                nextRoom.connections[Room.Direction.North] = true;
            }
            else
            {
                currRoom.connections[Room.Direction.North] = true;
                nextRoom.connections[Room.Direction.South] = true;
            }
        }
        else if (nextRoom.gridPosition.y == currRoom.gridPosition.y)
        {
            ///On same row so we need to look at
            ///east or west
            if (currRoom.gridPosition.x < nextRoom.gridPosition.x)
            {
                ///current room is to the left of the next room
                ///So the east door of the current room and the west
                ///door of the next room need to be marked as true
                currRoom.connections[Room.Direction.East] = true;
                nextRoom.connections[Room.Direction.West] = true;
            }
            else
            {
                currRoom.connections[Room.Direction.West] = true;
                nextRoom.connections[Room.Direction.East] = true;
            }
        }
    }
    #endregion

    private void BuildBorder()
    {
        if (PlayerPrefs.GetInt(PrefTags.PWorldRows) == 5)
        {
            Instantiate(AdventureSizeBorder);
        }
        else
        {
            Instantiate(NormalSizeBorder);
        }
    }

}
