using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Codename: "Shieldy"
/// "A high-level variant of this enemy will run around the map 
/// but also have a shield that will prohibit the player from damaging them. 
/// The enemy will be able to put up their shield every 5 seconds
/// and have 50 HP (10 hits from the player). 
/// This enemy will take away 3 damage if it touches the player."
/// </summary>

public class EnemyB_3 : EnemyB
{
    [SerializeField] protected GameObject shieldVisual; // Object to visualize shield. Does nothing otherwise!
    // protected float shieldCooldownTime;
    
    [SerializeField] protected float shieldActiveTime; // Time to stay in shield.
    // Time spent NOT in shield is determined by wanderCooldown!!!!!!!!!!!!!!!!!!!!!!
    protected bool isShielded; // Whether the unit is currently shielded.

    protected override void Start()
    {
        base.Start();



        // Custom stats
        if (Health == 0)
        {
            Health = 50;
        }

        // Start the infinite wander logic.
        RunNShield();

    }

    public override void TakeDamage(int dmgAmount)
    {
        if (!isShielded)
        {
            base.TakeDamage(dmgAmount);
            print(dmgAmount);
        }
    }

    private void RunNShield()
    {
        StartCoroutine(ShieldCooldown(wanderCooldown, shieldActiveTime));
    }

    /// Author: Paul Hernandez
    /// Date: 3/2/2021
    /// <summary>
    /// Wander, then stop and put up a shield.
    /// This timed function calls itself when it's done!
    /// </summary>
    /// <param name="timeOff">Max time to walk around without shield.</param>
    /// <param name="timeOn">Time spent shielding.</param>
    /// <returns></returns>
    IEnumerator ShieldCooldown(float timeOff, float timeOn)
    {
        isShielded = false;
        shieldVisual.SetActive(false);
        agent.SetDestination(PickRandomPoint(agent.transform.position, wanderDistance, 31));
        yield return new WaitForSeconds(timeOff);

        isShielded = true;
        shieldVisual.SetActive(true);
        agent.SetDestination(this.transform.position);
        yield return new WaitForSeconds(timeOn);
        RunNShield();
    }

}
