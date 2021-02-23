using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A type of enemy that chases the player.
/// </summary>


public class EnemyC : Enemy
{
    protected bool onCooldown;

    ///Movement logic of Enemy C variants stays here
    ///

    protected override void Start()
    {
        base.Start();

        // All enemy C types chase the player.
        ChasePlayer(); 
    }


    protected void OnTriggerStay(Collider other)
    {
        if (!onCooldown && other.gameObject.layer == 8) // If "Player" layer.
        {
            Player.Instance.TakeDamage(1);
            StartCoroutine(TackleCooldown(1f));
            Debug.Log(onCooldown);
        }
    }

    /// Author: Paul Hernandez
    /// Date: 2/22/2021
    /// <summary>
    /// This puts the body contact hitbox for this unit on cooldown.
    /// </summary>
    protected IEnumerator TackleCooldown(float time)
    {
        onCooldown = true;
        yield return new WaitForSeconds(time);
        onCooldown = false;
    }

}
