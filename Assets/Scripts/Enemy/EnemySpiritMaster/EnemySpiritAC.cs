using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemySpiritAC : MonoBehaviour
{
   private EnemySpirit enemy => GetComponentInParent<EnemySpirit>();
   
        private void AnimationTrigger()
        {
            enemy.AnimationFinishTrigger();
        }
        
        private void DeathTrigger(){
            Destroy(enemy.GameObject());
        }

}