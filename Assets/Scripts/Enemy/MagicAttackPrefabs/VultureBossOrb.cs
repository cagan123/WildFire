using UnityEngine;

public class VultureBossOrb : MagicAttack
{
    public Transform target;       // The player (assigned dynamically)
    public float orbitRadius = 5f; // Initial orbit radius
    public float orbitSpeed = 50f; // Orbiting speed (degrees per second)
    public float crashSpeed = 150f;
    public float spiralSpeed = 2f; // Speed of the spiral motion
    public static int activeOrbCount = 0; // Tracks how many orbs are active
    public static int maxOrbs = 3;        // Max orbs before triggering crash

    private static bool shouldCrash = false; // Flag for triggering crashes
    private float radius;  // Current orbit radius
    private float angle;   // Current angle of orbit
    private bool isCrashing = false;

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        if (target == null)
        {
            Debug.LogWarning("No target assigned for MagicOrb!");
            Destroy(gameObject);
            return;
        }

        // Initialize the orbit
        radius = orbitRadius + Random.Range(-1f, 1f); // Add random variation
        angle = Random.Range(0f, 360f);              // Randomize starting angle
        activeOrbCount++;

        // Check if we should start crashing
        if (activeOrbCount >= maxOrbs)
        {
            TriggerCrashForAllOrbs();
        }
    }

    public void Update()
    {
        if (shouldCrash && !isCrashing)
        {
            StartCrashing();
        }

        if (isCrashing)
        {
            PerformCrashMotion();
        }
        else
        {
            PerformOrbitMotion();
        }
    }

    private void PerformOrbitMotion()
    {
        // Calculate the new orbit position
        angle += orbitSpeed * Time.deltaTime; // Increment angle
        float radian = Mathf.Deg2Rad * angle; // Convert to radians

        // Calculate position around the target
        Vector3 orbitPosition = target.position + new Vector3(Mathf.Cos(radian) * radius, Mathf.Sin(radian) * radius, 0);

        // Move the orb to the new orbit position
        transform.position = Vector3.Lerp(transform.position, orbitPosition, 0.1f); // Smooth transition
    }

    private void PerformCrashMotion()
    {
        radius -= spiralSpeed* Time.deltaTime;
        orbitSpeed = crashSpeed;
        PerformOrbitMotion();
        // Check if we've reached the target
        if (damageSource.damageDone)
        {
            OnCrash();
        }
    }

    public void StartCrashing()
    {
        isCrashing = true;
    }

    private void OnCrash()
    {
        activeOrbCount--;
        Destroy(gameObject);
    }
    private static void TriggerCrashForAllOrbs()
    {
        shouldCrash = true; // Set global crash flag
    }


}
