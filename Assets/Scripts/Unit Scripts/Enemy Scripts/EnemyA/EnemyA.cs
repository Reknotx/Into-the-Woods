using System.Collections;
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
    protected float shotCooldown; // How long to wait before firing again.
    protected bool onCooldown; // Am I on cooldown?
    protected float shotVelocity; // How fast does the bullet fire?



    protected override void Start()
    {
        base.Start();

        if (aimerObj == null)
        {
            Debug.Log("aimerObj not connected to " + this.name + " !");
        }

       

    }

    protected void FixedUpdate()
    {
        // Rotate towards player if they're in awareness range.
        // If in line of sight, fire projectile.
        AimAtPlayer();

    }

    /// Author: Paul Hernandez
    /// Date: 2/20/2021
    /// <summary>
    /// This is all the logic for aiming and firing at the player.
    /// </summary>
    protected void AimAtPlayer()
    {
        if (!onCooldown && (distanceFromPlayer < awarenessRange))
        {
            // Rotate the aimer towards the player.
            aimerObj.transform.LookAt(PlayerObject.transform);

            // Check if line of sight is valid.

            // If I can see them, fire.

            // Spawn a bullet.
            GameObject bulletInstance = Instantiate(bullet, aimerObj.gameObject.transform.position, aimerObj.transform.rotation);
            // Rotate the bullet.
            bulletInstance.GetComponent<Transform>().rotation = aimerObj.transform.rotation;
            // Push the bullet forward.
            bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * (bulletForce), ForceMode.Impulse);

            StartCoroutine(fireCooldown(1));


        }

    }


    /// Author: Paul Hernandez
    /// Date: 2/20/2021
    /// <summary>
    /// This puts bullet firing on cooldown for the given amount of seconds.
    /// </summary>
    IEnumerator fireCooldown(float time)
    {
        onCooldown = true;
        yield return new WaitForSeconds(time);
        onCooldown = false;
    }

}
