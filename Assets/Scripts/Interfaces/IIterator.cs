using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// Author: Chase O'Connor
/// Date: 2/26/2021
/// <summary> Iterator class that enforces the iterator design pattern. </summary>
public interface IIterator
{
    /// <summary>Checks to see if we still have items left in the inventory to look at. </summary>
    /// <remarks>Uses an index to loop through the list, that index needs to start at -1;</remarks>
    /// <returns>True if there are items left to look at, false otherwise.</returns>
    bool hasMoreItems();

    /// <summary> Returns a reference to the next collectable in the inventory. </summary>
    /// <returns>A reference to the current collectable in our inventory.</returns>
    Collectable GetCurrentItem();

    /// <summary> Advances the index ahead by 1 </summary>
    void Next();

}
