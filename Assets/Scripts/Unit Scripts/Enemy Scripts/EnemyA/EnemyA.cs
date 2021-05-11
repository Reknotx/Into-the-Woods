﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "These enemies will not be moving
/// and will remain stationary in their place."
/// </summary>

    // To do:
    // Make this enemy start aiming and shooting!

public class EnemyA : Enemy
{
    // Objects
    [SerializeField] protected GameObject aimerObj; // Empty GameObject I'll rotate and will fire things from.
    [SerializeField] protected GameObject bullet; // What to shoot.

    // Shooting AI
    [SerializeField] protected float bulletForce; // How fast to shoot bullet.
    protected float shotAnticipation; // How long do I have to see the target before firing?
    [SerializeField] protected float shotCooldown; // How long to wait before firing again.
    // protected float shotVelocity; // How fast does the bullet fire?

    public Animator animController;


    protected override void Start()
    {
        base.Start();

        if (aimerObj == null)
        {
            Debug.Log("aimerObj not connected to " + this.name + " !");
        }

       

    }

    protected override void FixedUpdate()
    {
        // Rotate towards player if they're in awareness range.
        // If in line of sight, fire projectile.
        if (IsFrozen || PlayerInfo.IsInvisible) return;

        transform.LookAt(PlayerObject.transform);
        gameObject.transform.eulerAngles = new Vector3(0f, gameObject.transform.eulerAngles.y, 0f); // reset other rotations asides Y.


        AimAtPlayer();

    }

    /// Author: Paul Hernandez
    /// Date: 2/20/2021
    /// <summary>
    /// This is all the logic for aiming and firing at the player.
    /// </summary>
    protected void AimAtPlayer()
    {
        if (!onCooldownShoot)
        {
            distanceFromPlayer = Vector3.Distance(transform.position, PlayerObject.transform.position);

            if (distanceFromPlayer < awarenessRange)
            {
                // Rotate the aimer towards the player.
                //aimerObj.transform.LookAt(PlayerObject.transform);

                // Check if line of sight is valid.

                // If I can see them, fire.

                // Spawn a bullet.
                GameObject bulletInstance = Instantiate(bullet, aimerObj.gameObject.transform.position, aimerObj.transform.rotation);
                // Rotate the bullet.
                bulletInstance.GetComponent<Transform>().rotation = aimerObj.transform.rotation;
                // Push the bullet forward.
                bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * (bulletForce), ForceMode.Impulse);

                animController.SetTrigger("CastAttack");

                StartCoroutine(fireCooldown(shotCooldown));


            }
        }
    }


    /// Author: Paul Hernandez
    /// Date: 2/20/2021
    /// <summary>
    /// This puts bullet firing on cooldown for the given amount of seconds.
    /// </summary>
    IEnumerator fireCooldown(float time)
    {
        onCooldownShoot = true;
        yield return new WaitForSeconds(time);
        onCooldownShoot = false;
    }

}
