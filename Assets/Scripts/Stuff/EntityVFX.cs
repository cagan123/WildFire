using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityVFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash VFX")]
    [SerializeField] private Material hitMat;
    [SerializeField] private Material tiredMat;
    [Header("UI")]
    public GameObject damageTextPrefab; // Drag and drop your DamageText prefab here
    public GameObject exlamationTextPrefab;
    public Transform damageTextSpawnPoint; // Optional: where to spawn the text
    [Header("Time Stop")]
    public float timeStopDuration = 1f;
    private bool isStopping = false;
    private Coroutine timeStopRoutine;
    
    private Material originialMat;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originialMat = sr.material;
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
    public void StopTime(){
        if (isStopping)
            return; // Prevent overlapping hitstop calls

        // Start the hitstop coroutine
        timeStopRoutine = StartCoroutine(HitTimeStop_Routine());
    }
    private IEnumerator HitTimeStop_Routine()
    {
        isStopping = true;
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(timeStopDuration);
        Time.timeScale = originalTimeScale;
        isStopping = false;
        timeStopRoutine = null;
    }
    private IEnumerator FlashVFX_Routine()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(.2f);
        sr.material = originialMat;
    }
    private IEnumerator NotEnoughStaminaVFX_Routine()
    {
        sr.material = tiredMat;
        yield return new WaitForSeconds(.2f);
        sr.material = originialMat;
    }
    public void NotEnoughStaminaVFX(){
        StartCoroutine("NotEnoughStaminaVFX_Routine");
        return;
    }

}
