using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// Author: Chase O'Connor
/// Date: 3/8/2021
/// <summary>
/// Handles the individual rooms in the game and their functionality.
/// </summary>
/// <rem
public class Room : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>(0);

    static GameObject[] doors;

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
            Debug.Log(enemy.name);
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
            OpenDoors();
        }
    }

    /// <summary> Closes all of the doors in the level. </summary>
    private void CloseDoors()
    {
        Debug.Log("Closing doors.");

        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }

        foreach (Enemy enemy in enemies)
        {
            Debug.Log(enemy.gameObject.name);
            enemy.gameObject.SetActive(true);
        }
    }

    /// <summary> Opens all of the doors in the level. </summary>
    private void OpenDoors()
    {
        Debug.Log("Opening doors.");

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

            CameraTransition.Instance.TransitionToPoint(transform.position);
            PlayerInfo.CurrentRoom = this;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == 8)
    //    {
    //        OpenDoors();
    //    }
    //}
}
