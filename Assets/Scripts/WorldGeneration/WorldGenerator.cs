using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldGenerator : MonoBehaviour
{

    public int WorldRows; // Just the number of rows the world's rooms will be arranged in.
    public int WorldColumns; // Same for columns.
    public int minimumJourney; // The minimum distance between the home and boss tiles.
    private float WorldXScale; // Scale of grid on the X axis. Currently bugged, minimumJourney doesn't play well.
    private float WorldZScale; // Scale of grid on the Z axis. Currently bugged, minimumJourney doesn't play well.

    public bool manualSeed; // Do you want to force a certain seed?
    public int seed; // What exact randomization seed to use.
    
    public GameObject[] HomeRoomList; // The spawn room prefab. I call it home, because that's where the heart is.
    public GameObject[] BossRoomList; // The boss room prefab.
    public GameObject[] FieldRoomList; // List of field room prefabs to be assigned in the inspector.

    private List<GameObject> FieldRoomPicks = new List<GameObject>(); // The current list of picked field rooms for this world.
    private GameObject[] FieldRoomFinal; // The final array of picked field rooms as an array.

    private List<Vector3> gridPositions = new List<Vector3>(); // A list of possible locations to place tiles.

    // These two have their own thing because they get re-rolled until they're a valid distance (minimumJourney) from each other.
    private Vector3 intendedStart = new Vector3(); // The location of the starting home spawn room.
    private Vector3 intendedEnd = new Vector3(); // The location of the ending boss room. 
    private int intendedStartIndex; // Index of the above start location.
    private int intendedEndIndex; // Index of the above end location.

    // Start is called before the first frame update
    void Start()
    {
        SafetyCheck();

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
    /// Currently has a bug with overlapping tiles with start/end points.
    /// This specific function might get rewritten to be cleaner.
    /// STILL NEEDS WALL/DOOR GENERATION.
    /// </summary>
    public void GenerateRoomList()
    {
        // Pick random field tiles to use.
        FieldRoomPicks.Clear();
        FieldRoomPicks.AddRange(FieldRoomList);
        // I'm opting to add everything, then remove the excess, because we don't want duplicates from adding individually.
        // Feel free to change if that's dumb. - Paul

        for (int i = 0; i < (FieldRoomList.Length - ((WorldRows * WorldColumns) - 2)); i++)
        {
            // Remove entries from the list until we just have what we need to fill in the map, minus the spawn and boss rooms.
            FieldRoomPicks.RemoveAt(Random.Range(0, FieldRoomPicks.Count));
        
        }

        // Generate grid with coordinates
        gridPositions.Clear();

        // Loop through x axis (columns).
        for (int x = 1; x < WorldColumns+1; x++)
        {
            // Within each column, loop through y axis (rows).
            for (int z = 1; z < WorldRows+1; z++)
            {
                // At each index add a new Vector3 to our list with the x and y coordinates of that position.
                gridPositions.Add(new Vector3(x * WorldXScale, 0f, z * WorldZScale));
            }
        }


        // Find valid start and end points.
        do
        {
            intendedStart = new Vector3();
            intendedEnd = new Vector3();
            SearchForValidSpace(HomeRoomList, "start");
            SearchForValidSpace(BossRoomList, "end");

        } while ( ((Mathf.Abs(intendedStart.x - intendedEnd.x)) + (Mathf.Abs(intendedStart.z - intendedEnd.z)) ) < minimumJourney);
        // While the absolute value of the sum of the two points' x's and z's is less than the minimum...
        // In other words, keep rolling for different endpoints if they're too close together.

        // Instantiate them.
        gridPositions.RemoveAt(intendedStartIndex);
        if (intendedStartIndex < intendedEndIndex) 
        {
            // We need to specifically have this because depending on where the two indexes are in relation
            // to each other in the list, intendedEndIndex could get shifted downward, so we need to
            // compensate for that case when it happens.
            gridPositions.RemoveAt(intendedEndIndex-1);
        }
        else
        {
            gridPositions.RemoveAt(intendedEndIndex);
        }
        LayoutAtPoint(HomeRoomList, intendedStart);
        LayoutAtPoint(BossRoomList, intendedEnd);
        

        // Lay down the remainder of tiles.
        FieldRoomFinal = FieldRoomPicks.ToArray();
        LayoutAtRandom(FieldRoomFinal, (WorldRows*WorldColumns-2));

    }


    #endregion

    #region FieldPlacement
    /// Author: Paul Hernandez
    /// Date: 2/4/2021
    /// <summary>
    /// Instantiates room object from a prefab list to a random location.
    /// </summary>
    /// <param name="tileArray">What set of room prefabs to use.</param>
    /// <param name="objectCount">How many rooms are we placing?</param>
    void LayoutAtRandom(GameObject[] tileArray, int objectCount)
    {
        //Instantiate objects until the limit objectCount is reached
        for (int i = 0; i < objectCount; i++)
        {


            //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
            Vector3 randomPosition = RandomPosition();


            // Choose a random tile from tileArray and assign it to tileChoice.
            // Pop off the one picked, so no duplicates.
            List<GameObject> asdf = tileArray.ToList();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length-1)]; // I THINK -1 is correct here.
            asdf.RemoveAt(System.Array.IndexOf(tileArray, tileChoice));
            tileArray = asdf.ToArray();
            // This is ugly, but I'm not sure how else to do this.
            // I just needed to make sure that the list of available rooms doesn't yield duplicates.

            //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation.
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    //RandomPosition returns a random position from our list gridPositions.
    /// Author: Paul Hernandez
    /// Date: 2/4/2021
    /// <summary>
    /// Returns a random position on the grid, and removes it from the list of available positions.
    /// </summary>
    /// <returns></returns>
    Vector3 RandomPosition()
    {
        // Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
        int randomIndex = Random.Range(0, gridPositions.Count -1); // -1 otherwise we can get an out of range index.

        // Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
        Vector3 randomPosition = gridPositions[randomIndex];

        // Remove the entry at randomIndex from the list so that it can't be re-used.
        gridPositions.RemoveAt(randomIndex);

        // Return the randomly selected Vector3 position.
        return randomPosition;
    }
    #endregion

    #region StartEndPointsPlacement 
    /// Author: Paul Hernandez
    /// Date: 2/4/2021
    /// <summary>
    /// Search for a single available position, and save it to either intendedStart or intendedEnd.
    /// </summary>
    /// <param name="tileArray">What array of room prefabs are we going to use?</param>
    /// <param name="type">Are you searching for the "start" or "end" point?</param>
    void SearchForValidSpace(GameObject[] tileArray, string type)
    {
        // Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
        Vector3 randomPosition = RandomPositionSearch(type);

        // Choose a random tile from tileArray and assign it to tileChoice
        GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length-1)]; // I THINK -1 is correct here.

        // 
        if (type == "start")
        {
            intendedStart = randomPosition;
        }
        else if (type == "end")
        {
            intendedEnd = randomPosition;
        }

    }

    /// Author: Paul Hernandez
    /// Date: 2/4/2021
    /// <summary>
    /// Search for a random position, and save its index to either intendedStartIndex or intendedEndIndex.
    /// </summary>
    /// <param name="type">Are you searching for the "start" or "end" point?</param>
    /// <returns></returns>
    Vector3 RandomPositionSearch(string type)
    {
        //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
        int randomIndex = Random.Range(0, gridPositions.Count-1); // -1 otherwise we can get an out of range index.

        //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
        Vector3 randomPosition = gridPositions[randomIndex];

        // 
        if (type == "start")
        {
            intendedStartIndex = randomIndex;
        }
        else if (type == "end")
        {
            intendedEndIndex = randomIndex;
        }

        //Return the randomly selected Vector3 position.
        return randomPosition;
    }

    /// Author: Paul Hernandez
    /// Date: 2/4/2021
    /// <summary>
    /// Place a specific tile at a specific point.
    /// </summary>
    /// <param name="tileArray">What array of room prefabs are we going to use?</param>
    /// <param name="tileType">Are you laying down the "start" or "end point?</param>
    void LayoutAtPoint(GameObject[] tileArray, Vector3 tileType)
    {
        //Choose a random tile from tileArray and assign it to tileChoice
        GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length-1)]; // I THINK -1 is correct here.

        //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation.
        Instantiate(tileChoice, tileType, Quaternion.identity);
    }
    #endregion



}
