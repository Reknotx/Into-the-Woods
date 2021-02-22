using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

}
