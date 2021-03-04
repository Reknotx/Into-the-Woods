using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Codename: "Rocky"
/// "A low-level variant of this enemy shoots projectiles at the player. 
/// These will have 15 HP (3 hits from the player) and the projectiles 
/// that they release will deal 1 damage to the player."
/// </summary>

public class BossA : EnemyA
{

    private bool regening = false;


    public override void TakeDamage(int dmgAmount)
    {
        base.TakeDamage(dmgAmount);
        if (!regening) StartCoroutine(Regen());
    }

    IEnumerator Regen()
    {
        regening = true;

        while(Health < 100)
        {
            yield return new WaitForSeconds(5f);
            Health++;
            Debug.Log("Boss health regen: " + Health);
        }

        regening = false;
    }

}
