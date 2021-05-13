using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// I'll try to utilize this but I don't exactly know how it works. - Paul.
public class EnemyAProjectile : MonoBehaviour
{
    public float selfDestructTime = 5f;

    /// <summary> The amount of damage this spell deals to the player. </summary>
    [SerializeField] private int DMG = 1;

    public enum EnemyVariant
    {
        V1,
        V2,
        V3,
        Boss
    }

    public EnemyVariant enemyVariant = EnemyVariant.V1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && PlayerInfo.IsProtected == false) // If "Player" layer.
        { 
            SpellEffectOnPlayer();
        }
        else if (other.gameObject.layer == 13 || other.gameObject.layer == 27)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Destroy(this.gameObject, selfDestructTime);
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
            case EnemyVariant.V1:
                ///Deal damage
                Player.Instance.TakeDamage(DMG);
                break;
            
            case EnemyVariant.V2:
                ///damage and freeze spell casting
                Player.Instance.StartCoroutine(Player.Instance.SpellsFrozen(3f));
                Player.Instance.TakeDamage(DMG);
                break;
            
            case EnemyVariant.V3:
                ///Bleed damage only
                Player.Instance.StartCoroutine(Player.Instance.Bleed(DMG));
                break;

            case EnemyVariant.Boss:
                Player.Instance.StartCoroutine(Player.Instance.Bleed(DMG));
                Player.Instance.StartCoroutine(Player.Instance.SpellsFrozen(5f));
                break;
            
            default:
                break;
        }

        Destroy(this.gameObject);
    }

}
