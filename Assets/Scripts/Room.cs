﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// Author: Chase O'Connor
/// Date: 3/8/2021
/// <summary>
/// Handles the individual rooms in the game and their functionality.
/// </summary>
public class Room : MonoBehaviour
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public GameObject chestOnCompletion;

    public Transform chestSpawnLoc;

    public Dictionary<Direction, bool> connections;

    public List<Enemy> enemies = new List<Enemy>(0);

    static GameObject[] doors;

    #region Pathfinding stuff
    /// <summary> The position in the map for this room. </summary>
    /// <remarks>This is indicative of the position of this room
    /// on the 2D grid array.</remarks>
    //[HideInInspector]
    public Vector2 gridPosition = Vector2.zero;

    /// <summary> The g score of this room. </summary>
    [HideInInspector]
    public int gCost = 0;

    /// <summary> The h score of this room. </summary>
    [HideInInspector]
    public int hCost = 0;

    /// <summary> The parent of this room in the algorithm. </summary>
    [HideInInspector]
    public Room parent;

    /// <summary> The f score of this room. </summary>
    public int fCost { get => gCost + hCost; }
    #endregion

    #region Connections
    int _minConnect = 1;

    int _maxConnect = 4;

    int _currConnect;

    /// <summary>
    /// Refers to the minimum number of connections this room is allowed
    /// to have.
    /// </summary>
    /// Essentially what this is used for is for indicating which rooms
    /// are on critical paths. All rooms that are on a critical path or
    /// a branching path has a minimum of 2, but CAN have more than 2;
    public int MinConnections
    {
        get => _minConnect;

        set { _minConnect = Mathf.Clamp(value, 1, 4); }
    }

    /// <summary>
    /// Refers to the maximum number of connections this room is allowed
    /// to have.
    /// </summary>
    /// Essentially what this is used for is to indicate the limit of connections
    /// for rooms. How many they can have in total. Corners can have 2, edge rooms
    /// can have 3, and all other rooms can have up to 4.
    public int MaxConnections
    {
        get => _maxConnect;

        set { _maxConnect = Mathf.Clamp(value, 2, 4); }
    }

    /// <summary>
    /// Refers to the current number of connections this room has
    /// to other surrounding rooms.
    /// </summary>
    /// Essentially what this is used for is to indicate the current number
    /// of connections this room has at the moment to other rooms.
    public int CurrConnections
    {
        get => _currConnect;

        set { _currConnect = Mathf.Clamp(value, 1, 4); }
    }
    #endregion

    public bool IsBossRoom { get; set; } = false;

    public AudioClip bossDeath;

    private void Awake()
    {
        connections = new Dictionary<Direction, bool>()
        {
            {Direction.North, false},
            {Direction.East, false},
            {Direction.South, false},
            {Direction.West, false}
        };
    }

    private void Start()
    {
        enemies.Clear();
        
        if (doors == null)
        {
            doors = GameObject.FindGameObjectsWithTag("Door");
        }
        else if (doors[0] == null)
        {
            doors = null;
            doors = GameObject.FindGameObjectsWithTag("Door");
        }

        foreach (Transform enemy in transform.Find("Enemies"))
        {
            enemies.Add(enemy.GetComponent<Enemy>());
            //Debug.Log(enemy.name);
            enemy.gameObject.SetActive(false);
        }

        OpenDoors();
    }


    /// <summary>
    /// When an enemy is killed it needs to be removed from the room list.
    /// </summary>
    /// <param name="deadEnemy">The enemy that was killed.</param>
    public void RemoveEnemy(Enemy deadEnemy)
    {
        enemies.Remove(deadEnemy);

        if (enemies.Count == 0)
        {
            if (IsBossRoom)
            {
                WinLoseUI.Instance.bossDead = true;
                GetComponent<AudioSource>().PlayOneShot(bossDeath);
            }
            else
            {
                Instantiate(chestOnCompletion, chestSpawnLoc.position, Quaternion.identity);
            }
            if(GetComponent<NightRoom>() != null)
            {
                gameObject.GetComponent<NightRoom>().enabled = false;
                RoomRestriction.Instance.NightDoorUpdate();
            }
            OpenDoors();
        }
    }

    /// <summary> Closes all of the doors in the level. </summary>
    private void CloseDoors()
    {
        //Debug.Log("Closing doors.");

        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }

        StartCoroutine(SpawnDelay());

        
    }

    /// <summary> Opens all of the doors in the level. </summary>
    private void OpenDoors()
    {
        //Debug.Log("Opening doors.");

        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (enemies.Count != 0)
            {
                CloseDoors();
            }

            if (IsBossRoom)
            {
                GetComponent<AudioSource>().Play();
                MusicManager.Instance.PlayBossMusic();
            }

            CameraTransition.Instance.TransitionToPoint(transform.position);
            PlayerInfo.CurrentRoom = this;
        }
        else if (other.gameObject.layer == 10 && !enemies.Contains(other.gameObject.GetComponent<Enemy>()))
        {
            if (enemies.Count == 0)
            {
                CloseDoors();
            }

            enemies.Add(other.gameObject.GetComponent<Enemy>());

        }
    }


    public void RemoveDoors()
    {
        List<GameObject> moveList = new List<GameObject>();

        int labelX = 0;
        int labelZ = 0;

        if (!connections[Direction.North])
        {
            foreach (Transform item in transform.GetChild(1).GetChild( (int)Direction.North) )
            {
                moveList.Add(item.gameObject);
            }
        }
        else
        {
            labelZ = 5;
        }

        if (!connections[Direction.East])
        {
            foreach (Transform item in transform.GetChild(1).GetChild( (int)Direction.East) )
            {
                moveList.Add(item.gameObject);
            }
        }
        else
        {
            labelX = 7;
        }

        if (!connections[Direction.South])
        {
            foreach (Transform item in transform.GetChild(1).GetChild( (int)Direction.South) )
            {
                moveList.Add(item.gameObject);
            }
        }
        else
        {
            labelZ = -5;
        }

        if (!connections[Direction.West])
        {
            foreach (Transform item in transform.GetChild(1).GetChild( (int)Direction.West) )
            {
                moveList.Add(item.gameObject);
            }
        }
        else
        {
            labelX = -7;
        }

        foreach (GameObject item in moveList)
        {
            item.transform.parent = transform.GetChild(2);
        }

        if (IsBossRoom)
        {
            foreach (Transform child in transform.parent)
            {
                if (child.gameObject.layer == 5)
                {
                    child.transform.localPosition = new Vector3(labelX, 3, labelZ);
                }
            }
        }

    }

    IEnumerator SpawnDelay()
    {
        //yield return new WaitUntil(() => CameraTransition.Instance.Done == true);

        //while(CameraTransition.Instance.Done == false)
        //{
        //    //yield return new WaitForFixedUpdate();
        //    yield return null;
        //}
        yield return new WaitForSeconds(.75f);
        //Debug.Log(CameraTransition.Instance.Done);

        foreach (Enemy enemy in enemies)
        {
            //Debug.Log(enemy.gameObject.name);
            enemy.gameObject.SetActive(true);
        }
    }
}
