using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityVFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash VFX")]
    [SerializeField] private Material hitMat;
    public GameObject damageTextPrefab; // Drag and drop your DamageText prefab here
    public GameObject exlamationTextPrefab;
    public Transform damageTextSpawnPoint; // Optional: where to spawn the text
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
    public void TakeDamage(int damage)
    {
        
        Vector3 spawnPosition = damageTextSpawnPoint.position + Vector3.up * 1.5f;
        GameObject damageTextInstance = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, transform);
        
        DamageText floatingText = damageTextInstance.GetComponent<DamageText>();
        floatingText.ShowDamageText(damage);
    }
    public void NoticeEnemy(){
        Vector3 spawnPosition = damageTextSpawnPoint.position + Vector3.up * 1.5f;
        Instantiate(exlamationTextPrefab, spawnPosition, Quaternion.identity, transform);
    }
}
