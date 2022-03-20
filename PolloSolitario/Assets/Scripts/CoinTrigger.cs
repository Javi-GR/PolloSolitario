using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    //Reference to the particles when the coin is obtained
    public ParticleSystem particles;
    StatCount statCount;
    //Coin sound
    AudioSource audioClip;

    void Start()
    {
        audioClip  = GetComponent<AudioSource>();
        statCount = GameObject.FindGameObjectWithTag("HUD").GetComponent<StatCount>();
    }
    //When triggered the collider, the coin is destroyed, played the animation and added to the coins in HUD
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
