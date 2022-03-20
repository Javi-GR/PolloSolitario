using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeeEnemyController : MonoBehaviour
{
    //Reference to the NavMeshAgent component in the gameObject
    private NavMeshAgent agent = null;
    //Reference to the hit particles
    ParticleSystem particles;
    //Check if the enemy is already attacking
    private bool alreadyAttacking;
    //Reference to the chicken to target
    GameObject target;
    //Reference to the HUD to update the kills
    StatCount statCount;
    //Reference to the player controller so enemies can damage the player
    ChickenController chicken;
    //Reference to the animator component in the bee enemy
    Animator anim;
    //Health of the enemy
    public int health;

    //When the player hits the enemy this function is called and takes away the damage dealt to the health, if its lower than 0, it dies
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

    //Coroutine that makes the enemy faint and die, adds a kill to the HUD 
    private IEnumerator Die()
    {
        anim.SetBool("Die", true);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        statCount.AddKill();

    }
    // When the enemy recieves a hit it plays the hit particles in its transform.position 
    private void playParticles()
    {
        particles.transform.position = gameObject.transform.position;
        particles.Play();
    }
    
    private void Start()
    {
        GetReferences();
    }
    //If the enemy kills the player it resets the level, loading it again, if the distance is <1.5 the enemy attacks, elsewise it moves to the player
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
    //Function that moves the agent component to the player
    public void MoveToPlayer()
    {
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
    //Function that stops the enemy
    public void Stop(){
        agent.isStopped = true;
    }
    //Function that resumes the attack state from the enemy
    public void Resume()
    {
        agent.isStopped = false;
        anim.SetBool("Attack", false);
    }
    //Function that starts the attacking animation and coroutine if its not attacking already
    public void Attack(){
        if(!alreadyAttacking){
            StartCoroutine(attacks());
        }
        
    }
    //Coroutine that handles the bee attacks, it deals damage each 0.7 sec checking if the player is close enough to the enemy so it can deal the damage
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
        anim = GetComponentInChildren<Animator>();
    }
}
