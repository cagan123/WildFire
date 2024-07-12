using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool gettingKnockedBack { get; private set; } // publicly accessible but can be only changed here
    private Rigidbody2D rb;

    [SerializeField] private float knockbackTime = 0.2f;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
    }
    public void GetKnockedBacked(Transform damageSource, float knockbackForce)
    {
        gettingKnockedBack = true;
        Vector2 difference = (transform.position-damageSource.position).normalized * knockbackForce * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockBackRoutine());
    }

    private IEnumerator KnockBackRoutine()
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity= Vector2.zero;
        gettingKnockedBack= false;
    }
}
