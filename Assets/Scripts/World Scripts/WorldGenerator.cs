using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    private List<GameObject> FieldRoomPicks = new List<GameObject>(); // The current list of picked field rooms for this world.

    public WorldRoomContainer roomContainer; // The ScriptableObject that contains all of our room lists.

    public GameObject[,] roomArrange; // Our selected rooms to instantiate, in the arrangement we're building.
    public GameObject[,] roomInstances; // The rooms we've instantiated as based FieldRoomPicks.
    public Room[,] roomScripts; // The instances of roomInstances' scripts, "Room".

    protected override void Awake()
    {
        base.Awake();

        PrefsCheck();
        // Check the PlayerPrefs for any special modifiers to change the parameters.

        SafetyCheck();
        // Ensures that variables aren't set to values that will cause errors.

        if (manualSeed)
            Random.InitState(seed);

        GenerateRoomList();



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
        if (PlayerPrefs.GetInt("useSeed", 0) == 1)
        {
            manualSeed = true;
            seed = PlayerPrefs.GetInt("seed");
        }
        else
        {
            manualSeed = false;
        }
    }

    /// Author: Paul Hernandez
    /// Date: 2/5/2021
    /// <summary>
    /// Makes sure you're not using variables that will give errors or crashes. 
    /// </summary>
    void SafetyCheck()
    {
        WorldRoomsParent = new GameObject("WorldRoomsParent").transform;

        roomArrange = new GameObject[WorldColumns, WorldRows];
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
        // =========================================================================================
        // First, pick random field tiles to use. ("Field" meaning not spawn or boss room.)
        // These are stored in a list and will be put into a 2D array later for instancing.
        // Previous iteration just took the entire existing list of field rooms, then
        // randomly removed the excess until we had the desired number of rooms.
        // Shuffle this list.

        FieldRoomPicks.Clear();
        FieldRoomPicks.AddRange(roomContainer.roomPrefabs);
        for (int i = 0; i < (roomContainer.roomPrefabs.Count - ((WorldRows * WorldColumns) - 2)); i++)
        {
            // Remove entries from the list until we just have what we need to fill in the map, minus the spawn and boss rooms.
            FieldRoomPicks.RemoveAt(Random.Range(0, FieldRoomPicks.Count));

        }


        // Shuffling the selected field rooms.
        for (int i = 0; i < FieldRoomPicks.Count; i++)
        {
            GameObject temp = FieldRoomPicks[i];
            int randomIndex = Random.Range(i, FieldRoomPicks.Count);
            FieldRoomPicks[i] = FieldRoomPicks[randomIndex];
            FieldRoomPicks[randomIndex] = temp;
        }


        // =========================================================================================           
        // Second, randomize two points in a 2D array and loop until they're a valid distance.
        // We have safety checks in void Start so that we never have an infinite loop.
        // We can do math on the 2D indexes to deduce if they're far enough away from each other,
        // since the 2D indexes will reflect their placements asides from the physical distance.
        // One of these will be the spawn, and the other will be the boss.
        // EDIT: The boss room should be random from a list of boss rooms.

        // Starting with two points.
        Vector2Int intendedStart = new Vector2Int();
        Vector2Int intendedEnd = new Vector2Int();
        do
        {
            intendedStart = new Vector2Int(Random.Range(0, WorldColumns), Random.Range(0, WorldRows));
            intendedEnd = new Vector2Int(Random.Range(0, WorldColumns), Random.Range(0, WorldRows));

        } while (((Mathf.Abs(intendedStart.x - intendedEnd.x)) + (Mathf.Abs(intendedStart.y - intendedEnd.y))) < minimumJourney);

        // Committing to the array.
        roomArrange[intendedStart.x, intendedStart.y] = roomContainer.spawnRoom;
        //roomArrange[intendedEnd.x, intendedEnd.y] = roomContainer.bossRoom;
        roomArrange[intendedEnd.x, intendedEnd.y] = roomContainer.bossRooms[Random.Range(0, roomContainer.bossRooms.Count)]; // Commit a RANDOM boss room.

        // =========================================================================================
        // Third, fill the empty spaces of the 2D array with the randomized field rooms from earlier.

        List<GameObject> tempRoomList = FieldRoomPicks;

        for (int i = 0; i < roomArrange.GetLength(0); i++)
        {
            for (int z = 0; z < roomArrange.GetLength(1); z++)
            {
                if (roomArrange[i, z] != null)
                {
                    //print(i +"," + z + " was not null; " + roomArrange[i,z]);
                    // Do nothing?
                }
                else
                {
                    //print(i + "," + z + " was null.");

                    int randomRoom = Random.Range(0, tempRoomList.Count);
                    roomArrange[i, z] = tempRoomList[randomRoom];
                    tempRoomList.RemoveAt(randomRoom);

                    //print(roomArrange[i, z]);
                }
            }
        }


        // =========================================================================================
        // Fourth, instantiate the 2D array of rooms.
        // Use a second 2D array to refer to the instances.

        for (int i = 0; i < roomArrange.GetLength(0); i++)
        {
            for (int z = 0; z < roomArrange.GetLength(1); z++)
            {
                // I feel like I've made a redundant amount of room arrays...?
                roomInstances[i, z] = Instantiate(roomArrange[i, z], new Vector3(i * WorldXScale, 0f, z * WorldZScale), Quaternion.identity);
                roomInstances[i, z].transform.SetParent(WorldRoomsParent);
                roomScripts[i, z] = roomInstances[i, z].transform.GetChild(1).GetComponent<Room>();
                roomScripts[i, z].gridPosition = new Vector2(i, z);

                //Debug.Log(roomScripts[i, z].gameObject.transform.parent.name + " grid position " + roomScripts[i, z].gridPosition.ToString());
            }
        }

        AStar.Instance.grid = roomScripts;
        AStar.Instance.GeneratePath(roomScripts[0, 0], roomScripts[3, 3]);
        AStar.Instance.GeneratePath(roomScripts[0, 0], roomScripts[1, 3]);
        AStar.Instance.GeneratePath(roomScripts[0, 0], roomScripts[3, 1]);
        //AStar.Instance.GeneratePath(roomScripts[0, 0], roomScripts[3, 3]);
        AStar.Instance.RemoveRemainingDoors();

    }

    public void ChaseWorldGen()
    {
        ///Randomly select rooms for spawning
        ///ADJUST VALUES FOR THE FOR LOOP AS NEEDED
        for (int i = 0; i < 16; i++)
        {
            while (true)
            {
                int index = Random.Range(0, roomContainer.roomPrefabs.Count);

                if (!FieldRoomPicks.Contains(roomContainer.roomPrefabs[index]))
                {
                    FieldRoomPicks.Add(roomContainer.roomPrefabs[index]);
                    break;
                }
            }
        }

        ///Select the spawn position of the boss and spawn room
        Vector2Int intendedStart = new Vector2Int();
        Vector2Int intendedEnd = new Vector2Int();
        do
        {
            intendedStart = new Vector2Int(Random.Range(0, WorldColumns), Random.Range(0, WorldRows));
            intendedEnd = new Vector2Int(Random.Range(0, WorldColumns), Random.Range(0, WorldRows));

        } while (((Mathf.Abs(intendedStart.x - intendedEnd.x)) + (Mathf.Abs(intendedStart.y - intendedEnd.y))) < minimumJourney);

        ///Instantiate the Boss and Spawn rooms immediately after, 
        ///and place them in the 2D array roomInstances
        ///Have a reference point to where the spawn and boss rooms are
        ///in the area for easy reference with A*
        ///private Room spawnRoom;
        ///private Room bossRoom;


        ///Then, run through FieldRoomPicks until empty
        while (FieldRoomPicks.Count != 0)
        {
            int index = Random.Range(0, FieldRoomPicks.Count());

            while(true)
            {
                int x = Random.Range(0, WorldRows);
                int y = Random.Range(0, WorldColumns);

                if (roomInstances[x, y] == null)
                {
                    GameObject temp = null;
                    ///Instantiate the obj and store the reference
                    ///Do NOT get the script reference yet
                    
                    ///DO adjust positioning in world as is needed

                    ///Let's assume we spawned the object
                    FieldRoomPicks.Remove(temp);
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
            }
        }

    }


    ///Notes for spawning world
    ///1. Instantiate the rooms selected randomly from the scriptable object.
    ///2. Make a temporary game object reference for that spawned prefab.
    ///
    ///GameObject temp = Instantiate("room", .....);
    ///
    ///3. Store the room script from that temp obj in the array
    ///
    ///roomScripts[x, y] = temp.transform.GetChild(1).GetComponent<Room>();

    #endregion


    #region AStar
    ///<summary>Generates the path.</summary>
    public void GeneratePath(Room startRoom, Room endRoom)
    {
        ///Let's start with the crit path to test
        //paths.Add("Main Path", Algo(startRoom, endRoom));
        Debug.Log("Start room: " + startRoom.gameObject.transform.parent.name);
        Debug.Log("End room: " + endRoom.gameObject.transform.parent.name);

        List<Room> tempList = Algo(startRoom, endRoom);

        for (int i = 0; i < tempList.Count; i++)
        {
            Room currRoom = tempList[i];
            //Debug.Log("Current room grid position " + currRoom.gridPosition.ToString());

            if (i < tempList.Count - 1)
            {
                ///A next room exists
                Room nextRoom = tempList[i + 1];
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

    public void RemoveDoors()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                roomScripts[x, y].RemoveDoors();
            }
        }
    }


    #endregion

}
