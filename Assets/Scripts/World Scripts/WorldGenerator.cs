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

        SafetyCheck();
        // Ensures that variables aren't set to values that will cause errors.

        if (manualSeed)
            Random.InitState(seed);

        GenerateRoomList();

    }

    #region Setup
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
        roomArrange[intendedEnd.x, intendedEnd.y] = roomContainer.bossRoom;


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
                roomScripts[i, z] = roomInstances[i, z].GetComponent<Room>();
                roomScripts[i, z].GetComponent<Room>().gridPosition = new Vector2(i, z);
            }
        }

    }
    #endregion


}
