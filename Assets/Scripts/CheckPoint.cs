using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public ParticleSystem fire;
    public string checkPointID;
    public bool activated;

    public void Start()
    {
        fire = GetComponentInChildren<ParticleSystem>();
        fire.Stop();
    }

    [ContextMenu("GenerateID")]
    private void GenerateID(){
        checkPointID = System.Guid.NewGuid().ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            ActivateCheckpoint();
        }
    }

    public void ActivateCheckpoint()
    {
        activated = true;
        fire.Play();
    }
}
