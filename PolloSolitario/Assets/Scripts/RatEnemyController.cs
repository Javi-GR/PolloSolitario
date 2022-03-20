using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatEnemyController : MonoBehaviour
{
    private NavMeshAgent agent = null;
    ParticleSystem particles;
    private bool alreadyAttacking;
    GameObject target;
    StatCount statCount;
    ChickenController chicken;

    Animator anim;

    public int health = 30;

    public void TakeDamage (int amount)
    {
        health -= amount;
        playParticles();
        if(health <= 0f)
        {
            Stop();
            StartCoroutine(Die());
        }
    }

    
    private IEnumerator Die()
    {
        anim.SetBool("Die", true);
        yield return new WaitForSeconds(1);
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
        if(dist<1.5f){
            Attack();
        }
        else{
            MoveToPlayer();
        }
         
    }
    public void MoveToPlayer()
    {
        
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
    public void Stop(){
        agent.isStopped = true;
    }
    public void Resume()
    {
        agent.isStopped = false;
        anim.SetBool("Attack", false);
    }
    public void Attack(){
        if(!alreadyAttacking){
            StartCoroutine(attacks());
        }
        
    }
     IEnumerator attacks(){
        Stop();
        anim.SetBool("Attack", true);
        alreadyAttacking = true;
        yield return new WaitForSeconds(0.5f);
       if(Vector3.Distance(target.transform.position, gameObject.transform.position)<1.5f)
        {
            chicken.TakeDamage(10);
            Debug.Log("Player has "+chicken.currentHealth+" health left");
        }
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
        anim = GetComponent<Animator>();
        anim.SetBool("Run", true);
    }
    
}
