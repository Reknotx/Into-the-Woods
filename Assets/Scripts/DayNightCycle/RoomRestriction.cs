using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Author: JT Esmond
/// Date: 3/29/2021
/// <summary>
/// Class that restricts access to different rooms depending on the time of day.
/// </summary>
public class RoomRestriction : SingletonPattern<RoomRestriction>
{

    #region List
    //list for the doors in each room gameobject
    private List<GameObject> _doors = new List<GameObject>();
    #endregion

    #region String
    // string for the Door tag
    private string searchTag = "Door";
    #endregion

    [HideInInspector] public bool nightOwl;
    private bool open;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        open = true;
        _doors.Clear();

        var nightRooms = FindObjectsOfType<NightRoom>();

        foreach (NightRoom nightRoom in nightRooms)
        {
            if (nightRoom.enabled)
            {
                Transform parent = nightRoom.transform;
                GetChildObject(parent, searchTag);
            }
        }
    }


    /// Author: JT Esmond
    /// Date: 3/29/2021
    /// <summary>
    /// functionality for the night doors
    /// </summary>
    public void NightDoors()
    {
        if (LightingManager.Instance.night == true || nightOwl == true || PlayerInfo.NightRoom != null)
        {
            foreach (GameObject door in _doors)
            {
                door.SetActive(false);
            }
        }

        else if (!LightingManager.Instance.night && !nightOwl && PlayerInfo.NightRoom == null)
        {
            foreach (GameObject door in _doors)
            {
                door.SetActive(true);
            }
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
