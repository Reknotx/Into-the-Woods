using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossB : EnemyC_3
{
    private int currentHealth;

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
                
                else
                {
                    WinLoseUI.Instance.bossDead = true;
                }

                Destroy(gameObject);
            }
        }
    }
}

