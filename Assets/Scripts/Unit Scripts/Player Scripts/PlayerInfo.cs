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

    /// <summary> Indicates if the player is currently protected by a spell. </summary>
    /// <value>A value of true indicates that the player is protected and can't take damage.</value>
    public static bool IsProtected { get; set; } = false;

    /// <summary> Indicates if the player is currently invisible. </summary>
    /// <value>A value of true means the player is invisible and shouldn't be chased
    /// by enemies.</value>
    public static bool IsInvisible { get; set; } = false;

    /// <summary> Indicates if the player's spell casting is frozen. </summary>
    /// <value>A value of true means the player is unable to cast any spells.</value>
    public static bool SpellsFrozen { get; set; } = false;

    /// <summary> Indicates if the player is currently immune to spell freezing. </summary>
    /// <value>A value of true means the player can't have their spell casting frozen by enemies.</value>
    public static bool SpellFreezeImmune { get; set; } = false;

    /// <summary> The room the player is currently in. </summary>
    public static Room CurrentRoom { get; set; }

    /// <summary> The room the player is currently in, used for the night room restrictions </summary>
    public static RoomRestriction NightRoom { get; set; }

    public static void Reset()
    {
        AttackDamage = 5;
        DoubleShot = false;
        SpellTracking = false;
        DoubleHarvest = false;
        IsProtected = false;
        IsInvisible = false;
        SpellsFrozen = false;
        SpellFreezeImmune = false;
    }
}
