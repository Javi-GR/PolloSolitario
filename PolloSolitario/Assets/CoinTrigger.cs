using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    public ParticleSystem particles;
    StatCount statCount;
    AudioSource audioClip;

    void Start()
    {
        audioClip  = GetComponent<AudioSource>();
        statCount = GameObject.FindGameObjectWithTag("HUD").GetComponent<StatCount>();
    }
    void OnTriggerEnter(Collider other){
        Debug.Log("Chocando contra la moneda");
        if(other.gameObject.tag == "Player"){
            particles.Play();
            audioClip.Play();
            StartCoroutine(DestroyAndAdd());
        }
    }
    private IEnumerator DestroyAndAdd(){
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        statCount.AddCoin();
    }
}
