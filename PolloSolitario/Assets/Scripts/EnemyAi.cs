using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    private NavMeshAgent agent = null;

    private void Start()
    {
        GetReferences();
    }
    private void Update(){
        MoveToPlayer();
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
        StartCoroutine(attacks());
    }
     IEnumerator attacks(){
        Stop();
        yield return new WaitForSeconds(2);
        Resume();
    }
    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
    }
}
