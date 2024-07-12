using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityVFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash VFX")]
    [SerializeField] private Material hitMat;
    private Material originialMat;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originialMat = sr.material;
    }

    private IEnumerator FlashVFX_Routine()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(.2f);
        sr.material = originialMat;
    }
}
