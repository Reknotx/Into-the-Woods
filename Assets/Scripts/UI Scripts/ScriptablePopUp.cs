using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Pop Up", menuName = "PopUp")]
public class ScriptablePopUp : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite art;
}
