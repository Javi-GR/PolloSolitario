using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserEnemyController : MonoBehaviour
{
    private NavMeshAgent agent = null;
    ParticleSystem particles;
    private bool alreadyAttacking;
    GameObject target;
    StatCount statCount;
    ChickenController chicken;

    public int health = 50;

    public void TakeDamage (int amount)
    {
        health -= amount;
        playParticles();
        if(health <= 0f)
        {
            Die();
        }
    }

    
    void Die()
    {
        Destroy(gameObject);
        statCount.AddKill();

    }
    private void playParticles()
    {
        particles.transform.position = gameObject.transform.position;
        particles.Play();
    }
    
    private void Start()
    {
        GetReferences();
    }
    private void Update(){

       if(chicken.currentHealth<=0){
            chicken.StartPoint();
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
        particles = GameObject.FindGameObjectWithTag("DamageParticles").GetComponent<ParticleSystem>();
        statCount = GameObject.FindGameObjectWithTag("HUD").GetComponent<StatCount>();
    }
}
