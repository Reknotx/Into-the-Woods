using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA_2 : EnemyA
{
    public int shieldDurability = 3;

    public override void TakeDamage(int dmgAmount)
    {
        if (shieldDurability != 0)
        {
            shieldDurability--;
            if (shieldDurability == 0)
            {
                ///turn off the shield
                transform.Find("Shield").gameObject.SetActive(false);
            }
        }
        else
        {
            base.TakeDamage(dmgAmount);
        }
    }

}
