using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyBomb : MonoBehaviour
{
    [SerializeField] protected float fuseTime; // How long before bomb blows up.
    [SerializeField] protected int bombDamage; // How much damage the bomb does.
    [SerializeField] protected GameObject bombVisual; // Visual bomb ball.
    [SerializeField] protected GameObject explosionVisual; // Visual explosion effect.
    private bool explosionLive; // If explosion hitbox is active and can hurt player.

    private void OnTriggerStay(Collider other)
    {
        if (explosionLive && other.gameObject.layer == 8) // If "Player" layer.
        {
            Player.Instance.TakeDamage(bombDamage);
            explosionLive = false;
        }
    }

    private void Start()
    {
        explosionVisual.SetActive(false);
        StartCoroutine(StartFuse(fuseTime));
    }

    IEnumerator StartFuse(float time)
    {
        // Fuse and buildup.
        yield return new WaitForSeconds(time);

        // Explode.
        bombVisual.SetActive(false);
        explosionVisual.SetActive(true);
        explosionLive = true;
        yield return new WaitForSeconds(0.1f);
        explosionLive = false;
        explosionVisual.SetActive(false);

        // Destroy object.
        // This is where you'd put smoke effects I assume.
        //yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);

    }

    /*
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

    
    private void SpellEffectOnPlayer()
    {
        switch (enemyVariant)
        {
            case EnemyVariant.A:
                ///Deal damage
                Player.Instance.TakeDamage(1);
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
    */
}
