using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Waypoint Values")]
    [SerializeField] private Vector3[] points;
    public Vector3[] Points => points;
    [HideInInspector]public Vector3 EntityPos;
    private bool gameStarted;

    private void Start(){
        EntityPos = transform.position;
        gameStarted = true;
    }
    public Vector3 GetPosition(int pointIndex){
        if(Points.Length <= 0){
            return EntityPos;
        }
        else{
            return EntityPos + points[pointIndex];
        }
        
    }
    private void OnDrawGizmosSelected(){
        if(!gameStarted && transform.hasChanged){
            EntityPos = transform.position;
        }
    }
}
