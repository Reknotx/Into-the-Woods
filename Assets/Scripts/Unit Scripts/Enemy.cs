using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    /// <summary> If the enemy is frozen they can't move. </summary>
    public bool IsFrozen { get; set; }


    /// <summary>
    /// Freezes the enemy in place for the specified amount of time.
    /// </summary>
    /// <param name="freezeDuration">The amount of real world time the enemy is frozen for.</param>
    public IEnumerator Freeze(float freezeDuration)
    {
        IsFrozen = true;

        yield return new WaitForSeconds(freezeDuration);

        IsFrozen = false;
    }

}
