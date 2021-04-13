using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossB : EnemyC_3
{
    private int _currentHealth;

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
        get => _currentHealth;

        set
        {
            _currentHealth = value;

            if (_currentHealth <= 0)
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

