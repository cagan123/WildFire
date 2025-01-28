using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageSource : MonoBehaviour
{
    FireSpirit fireSpirit => GetComponentInParent<FireSpirit>();

    private void OnParticleCollision(GameObject hit){
        if (hit.gameObject.GetComponent<Enemy>() != null && fireSpirit.damageSourceActive){
            DoDamageToEnemy(hit.GetComponent<Collider2D>());
        }
    }
    public void DoDamageToEnemy(Collider2D _hit){
        EnemyStats _target = _hit.gameObject.GetComponent<EnemyStats>();
        fireSpirit.stats.DoDamage(_target);
    }
}
