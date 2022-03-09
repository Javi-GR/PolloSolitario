using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private bool alreadyAttacking;
    GameObject target;
    ChickenController chicken;

    public float health = 50f;

    public void TakeDamage (float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
    }

    
    void Die()
    {
        Destroy(gameObject);
    }
    
    private void Start()
    {
        GetReferences();
    }
    private void Update(){

       if(chicken.currentHealth<=0){
            chicken.ResetGame();
        }
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if(dist<2){
            Attack();
        }
        else{
            MoveToPlayer();
        }
         
    }
    private void MoveToPlayer()
    {
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
    public void Stop(){
        agent.isStopped = true;
    }
    public void Resume()
    {
        agent.isStopped = false;
    }
    public void Attack(){
        if(!alreadyAttacking){
            StartCoroutine(attacks());
        }
        
    }
     IEnumerator attacks(){
        Stop();
        chicken.TakeDamage(10);
        alreadyAttacking = true;
        Debug.Log("Player has "+chicken.currentHealth+" health left");
        yield return new WaitForSeconds(2);
        Resume();
        alreadyAttacking = false;
    }
    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        chicken = target.GetComponent<ChickenController>();
    }
}
