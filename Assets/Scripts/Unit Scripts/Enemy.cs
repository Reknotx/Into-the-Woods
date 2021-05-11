using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// Author: Paul Hernandez
/// Date: 2/18/2021
/// <summary>
/// Base of all enemy units.
/// </summary>
public class Enemy : Unit
{
    // Player tracking
    [SerializeField] protected GameObject PlayerObject; // The player object.
    protected float playerTrackRate = 0.2f; // How often I'll update the player position.
    protected float distanceFromPlayer; // The distance between me and the player.
    [SerializeField] protected float awarenessRange; // How far I can sense the player.

    // Neo Navigation DX
    [SerializeField] protected float movementSpeed;
    /*
    protected bool wallHugMode;
    protected float rayLength = 1.3f;
    protected bool wallF;
    protected bool wallL;
    protected bool wallR;
    protected bool wallB;
    */

    /// <summary> If the enemy is frozen they can't move. </summary>
    public bool IsFrozen { get; set; }

    // Stats
    // HP is currently handled by Unit.cs
    protected bool onCooldownShoot; // Whether this unit's shooting attack is on cooldown.
    protected bool onCooldownBody; // Whether this unit's contact damage attack is on cooldown.
    [SerializeField] protected bool contactDamage; // Do I hurt the player on touch?
    [SerializeField] protected int contactDamageAmount; // How much damage do I do on touch?

    public EnemyLootTable lootTable;

    public AudioSource hitSource;



    //protected bool isDamageFlashing; // If the unit is currently flashing from damage.

    /// Author: Paul Hernandez
    /// Date: 2/20/2021
    /// <summary>
    /// All AI starts by calling "initialization". It grabs components necessary
    /// for its operations. This way, the designers don't have to fiddle with
    /// enemy stuff in the inspector or hierarchy (hopefully!).
    /// </summary>
    /// Edit - Chase O'Connor: Made a start function in Unit that is virtual for initializing health.
    protected override void Start()
    {
        base.Start();
        InitAI();

    }

    protected virtual void FixedUpdate()
    {
        //CheckWalls();
    }


    #region BasicAI

    /// Author: Paul Hernandez
    /// Date: 2/18/2021
    /// <summary>
    /// Finds player for navigation, and sets home location.
    /// </summary>
    protected void InitAI()
    {

        if (movementSpeed == 0)
        {
            movementSpeed = 1f;
        }

        // Look for player, with a safety check.
        if (Player.Instance.gameObject != null)
        {
            PlayerObject = Player.Instance.transform.parent.gameObject;
        }
        else
        {
            Debug.Log("No player object found!");
        }

    }

    #endregion

    #region Pathfinding
    /*
    protected void StartMovingTo()
    {
        // Rotate to target
        transform.LookAt(PlayerObject.transform);
        gameObject.transform.eulerAngles = new Vector3(0f, gameObject.transform.eulerAngles.y, 0f); // reset other rotations asides Y.
        
        // Move forward
        wallHugMode = false;

    }

    

    protected void CheckWalls()
    {
        //Vector3 raymondF = transform.TransformDirection(Vector3.forward) * 10;
        // Debug draw
        Debug.DrawRay(this.transform.position, this.transform.forward * rayLength, Color.red);
        Debug.DrawRay(this.transform.position, -this.transform.right * rayLength, Color.blue);
        Debug.DrawRay(this.transform.position, this.transform.right * rayLength, Color.blue);
        Debug.DrawRay(this.transform.position, -this.transform.forward * rayLength, Color.blue);

        RaycastHit hit;

        // If there's a wall in front.
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, rayLength) 
            && (hit.transform.gameObject.layer == 27))
        {
            wallF = true;
        }
        else
        {
            wallF = false;
        }
        // If there's a wall to the left.
        if (Physics.Raycast(this.transform.position, -this.transform.right, out hit, rayLength)
                && hit.transform.gameObject.layer == 27)
        {
            wallL = true;
        }
        else
        {
            wallL = false;
        }
        // If there's a wall to the right.
        if (Physics.Raycast(this.transform.position, this.transform.right, out hit, rayLength)
            && hit.transform.gameObject.layer == 27)
        {
            wallR = true;
        }
        else
        {
            wallR = false;
        }
        // If there's a wall behind.
        if (Physics.Raycast(this.transform.position, -this.transform.forward, out hit, rayLength)
            && hit.transform.gameObject.layer == 27)
        {
            wallB = true;
        }
        else
        {
            wallB = false;
        }
    }
    */

    #endregion

    #region Contact Damage

    protected virtual void OnTriggerStay(Collider other)
    {
        if (contactDamage && !onCooldownBody && other.gameObject.layer == 8) // If "Player" layer.
        {
            Player.Instance.TakeDamage(contactDamageAmount);
            StartCoroutine(TackleCooldown(1f));
            //Debug.Log(onCooldown);
        }
    }

    /// Author: Paul Hernandez
    /// Date: 2/22/2021
    /// <summary>
    /// This puts the body contact hitbox for this unit on cooldown.
    /// </summary>
    protected IEnumerator TackleCooldown(float time)
    {
        onCooldownBody = true;
        yield return new WaitForSeconds(time);
        onCooldownBody = false;
    }

    #endregion

    #region misc

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

    public override void TakeDamage(int dmgAmount)
    {
        base.TakeDamage(dmgAmount);
        StartCoroutine(FlashColor());
        hitSource.Play();
    }


    /// Author: Paul Hernandez
    /// Date: 3/7/2021
    /// <summary>
    /// This just makes the unit material flash an emissive color.
    /// Material must have Emission set to true and Global Illumination set to Realtime!!!!!!!!!!!!!!!
    /// </summary>
    /// <returns></returns>
    IEnumerator FlashColor()
    {
        if (GetComponent<MeshRenderer>() != null)
        {
            GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
            yield return new WaitForSeconds(0.1f);
            GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);


        }
        else // Look in children objects instead, such as in the case of the current level designer versions of enemy objects.
        {
            GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
            yield return new WaitForSeconds(0.1f);
            GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
        }
    }

    public void Drop()
    {
        float dropYes = Random.Range(0f, 100f);

        if (dropYes <= 35f) return;

        GameObject item = lootTable.Drop();

        if (item != null)
        {
            //Debug.Log("Success");
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }

    #endregion
}
