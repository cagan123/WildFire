using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obtaining_FireSpirit : MonoBehaviour
{
    [SerializeField]FireSpirit actualFire;
    public float radius;
    private void Update(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponentInParent<Player>() != null){
                StartCoroutine("DestructionRoutine");
            }
        }
    }
    public IEnumerator DestructionRoutine(){
        actualFire.gameObject.SetActive(true);
        Destroy(this.gameObject);
        yield return new WaitForSeconds(1f);
    }

}
