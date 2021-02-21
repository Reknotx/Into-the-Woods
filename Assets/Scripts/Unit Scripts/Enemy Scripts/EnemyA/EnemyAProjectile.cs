using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// I'll try to utilize this but I don't exactly know how it works. - Paul.
public class EnemyAProjectile : MonoBehaviour
{
    public enum EnemyVariant
    {
        A,
        B,
        C
    }

    public EnemyVariant enemyVariant = EnemyVariant.A;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) // If "Player" layer.
        {
            SpellEffectOnPlayer();
        }
    }

    /// <summary>
    /// This could be in the base enemy class
    /// </summary>
    public void UpdateDest()
    {

    }


    private void SpellEffectOnPlayer()
    {
        switch (enemyVariant)
        {
            case EnemyVariant.A:
                ///Deal damage
                Player.TakeDamage(1);
                break;
            
            case EnemyVariant.B:
                ///damage and freeze spell casting
                break;
            
            case EnemyVariant.C:
                ///Bleed damage only
                break;
            
            default:
                break;
        }

        Destroy(this.gameObject);
    }

}
