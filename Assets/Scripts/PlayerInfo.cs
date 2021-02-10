using System.Collections;
using System.Collections.Generic;

/// Author: Chase O'Connor
/// Date: 2/5/2021
/// <summary> 
/// A static class that holds all of the info of the player.
/// </summary>
public static class PlayerInfo
{
    /// <summary> The damage of the attack spell. </summary>
    public static int AttackDamage { get; set; } = 5;

    /// <summary> If true spawn two spells instead of one. </summary>
    /// <value>Value is true if the player has the two peas in a pod item.</value>
    public static bool DoubleShot { get; set; } = false;

    /// <summary> If true spells should track. </summary>
    /// <value>Value is true if player has the compass item.</value>
    public static bool SpellTracking { get; set; } = false;


    /// <summary> If true collect double the potion ingredient. </summary>
    /// <value>Value is true if the player has the lucky penny item.</value>
    public static bool DoubleHarvest { get; set; } = false;


}
