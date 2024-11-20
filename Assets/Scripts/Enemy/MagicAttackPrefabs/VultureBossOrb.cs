using UnityEngine;

public class VultureBossOrb : MagicAttack
{
    public Transform target; // The player (center of orbit)
    public float orbitRadius = 5f; // The radius of the orbit
    public float orbitSpeed = 50f; // The speed of the orbit (in degrees per second)
    public float orbitEasing = 0.1f; // Controls how smooth the movement is (higher is smoother)
    public float offsetAngle = 0f; // Starting angle (to make each orb different)
    
    private Vector3 velocity; // For smooth movement

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        // Randomize orbit parameters for variation
        orbitRadius += Random.Range(-1f, 1f); // Slight random radius variation
        orbitSpeed += Random.Range(-10f, 10f); // Random speed variation
        offsetAngle += Random.Range(0f, 360f); // Random initial angle
    }

    public override void Update()
    {
        if(enemy.amountofMagicInstantiated >=3){
            Vector3 direction = (transform.position - target.position).normalized;
            transform.position += -direction * 10f * Time.deltaTime;
            //enemy.amountofMagicInstantiated = 0;
        }
        else{
            // Time-based angle (smooth progression of angle)
        float angle = (Time.time * orbitSpeed + offsetAngle) % 360f;
        float radian = Mathf.Deg2Rad * angle; // Convert to radians for Mathf.Sin/Mathf.Cos

        // Calculate the new position in the orbit
        Vector3 targetPosition = target.position + new Vector3(Mathf.Cos(radian) * orbitRadius, Mathf.Sin(radian) * orbitRadius, 0);

        // Smooth movement using Vector3.SmoothDamp (helps prevent rigid movement)
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, orbitEasing);
        }
        

        
    }


}
