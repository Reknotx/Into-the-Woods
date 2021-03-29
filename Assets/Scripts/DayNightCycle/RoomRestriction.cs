using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author: JT Esmond
/// Date: 3/29/2021
/// <summary>
/// Class that restricts access to different rooms depending on the time of day.
/// </summary>
public class RoomRestriction : MonoBehaviour
{

    #region List
    //list for the doors in each room gameobject
    private List<GameObject> _doors = new List<GameObject>();
    #endregion

    #region String
    // string for the Door tag
    private string searchTag = "Door";
    #endregion

    #region Script References
    //reference for the room script
    private Room roomRef;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //assigns the room script reference
        roomRef = gameObject.GetComponent<Room>();
        
        //assigns all of the doors in the gameobject to the list
        if(searchTag != null)
        {
            FindObjectWithTag(searchTag);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //checks if it is night or its the players current room and determines wether to open or close the doors
        if(LightingManager.Instance.night || PlayerInfo.CurrentRoom == this)
        {
            OpenNightDoors();
        }
        else
        {
            CloseNightDoors();
        }

    }

    /// Author: JT Esmond
    /// Date: 3/29/2021
    /// <summary>
    /// opens the doors in the gameobject
    /// </summary>
    private void OpenNightDoors()
    {
        foreach (GameObject door in _doors)
        {
            door.SetActive(false);
        }
    }
    /// Author: JT Esmond
    /// Date: 3/29/2021
    /// <summary>
    /// closes the doors in the gameobject
    /// </summary>
    private void CloseNightDoors()
    {
        foreach (GameObject door in _doors)
        {
            door.SetActive(true);
        }
    }

    /// Author: JT Esmond
    /// Date: 3/29/2021
    /// <summary>
    /// finds the children of a gameobject with a specific tag
    /// </summary>
    public void FindObjectWithTag(string _tag)
    {
        _doors.Clear();
        Transform parent = transform;
        GetChildObject(parent, _tag);
    }

    /// Author: JT Esmond
    /// Date: 3/29/2021
    /// <summary>
    /// adds a child to the list
    /// </summary>
    public void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if(child.tag == _tag)
            {
                _doors.Add(child.gameObject);
            }
            if(child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }
}
