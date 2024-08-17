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
        private void Attack1Trigger()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck[0].position, enemy.attackCheckRadius[0]);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Player>() != null)
                {
                    DoDamageToPlayer(hit);
                }
            }
        }
        private void Attack2Trigger(){
            if(enemy.attackCheck[1] != null){
                Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck[1].position, enemy.attackCheckRadius[1]);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Player>() != null){
                        DoDamageToPlayer(hit);
                    }
                }
            }
  
        }
        private void Attack3Trigger(){
            if(enemy.attackCheck[2] != null){
                Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck[2].position, enemy.attackCheckRadius[2]);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Player>() != null){
                        DoDamageToPlayer(hit);
                    }
                }
            }
        }
        private void DeathTrigger(){
            Destroy(enemy.GameObject());
        }

        public void DoDamageToPlayer(Collider2D _hit){
            PlayerStats _target = _hit.GetComponent<PlayerStats>();
            _hit.GetComponent<Player>().closestEnemyPos = enemy.transform.position;
            enemy.stats.DoDamage(_target);
        }
}