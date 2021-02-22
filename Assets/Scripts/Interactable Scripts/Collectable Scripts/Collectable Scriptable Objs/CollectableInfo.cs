using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new collectable info", menuName = "Create new item info")]
public class CollectableInfo : ScriptableObject
{
    public Sprite UI_Sprite;

    public float dropRate;

}
