using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Codename: "Twoey"
/// "A high-level variant of this enemy will chase the player 
/// and when they are destroyed the first time, 
/// they will break into two enemies that must also be eliminated 
/// before their complete death. 
/// The original enemy will have 50 HP and when broken into two smaller enemies, 
/// each of those will have 15 HP. When hitting the player, 
/// the original enemy will deal 4 damage and the smaller enemies will deal 1 damage per hit."
/// </summary>

public class EnemyC_3 : EnemyC
{
    private int currentHealth;

    [SerializeField] protected bool splitOnDeath;
    [SerializeField] protected GameObject SubEnemyPrefab;
    [SerializeField] protected float miniSpawnDistance;



    protected override void Start()
    {
        base.Start();

        if (Health == 0)
        {
            Health = 50;
        }
    }

    public override int Health
    {
        get => currentHealth;

        set
        {
            currentHealth = value;

            if (currentHealth <= 0)
            {
                if (this is Enemy enemy && PlayerInfo.CurrentRoom != null)
                {
                    PlayerInfo.CurrentRoom.RemoveEnemy(enemy);
                }

                if (splitOnDeath)
                {
                    Instantiate(SubEnemyPrefab, this.transform.position + new Vector3(miniSpawnDistance, 0f, 0f), this.transform.rotation);
                    Instantiate(SubEnemyPrefab, this.transform.position + new Vector3(-miniSpawnDistance, 0f, 0f), this.transform.rotation);
                }             

                Destroy(gameObject);
            }
        }
    }
}
