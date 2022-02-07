using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    //[SerializeField] public AudioSource audio;
    public enum GunType {Semi, Burst, Auto};
    private float secondsBetweenShots;

    public float damage = 1f;

    //public Transform shellEjectionpoiint;
   // public Rigidbody shell;
    public float rpm;
    private float nextPossibleShot;
    public GunType gunType;
    public Transform spawn;

    void Start()
    {
        secondsBetweenShots = 60/rpm;
    }
    public void Shoot() 
    {
        if(CanShoot())
        {
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;

            float shotDistance = 2000;
            if(Physics.Raycast(ray, out hit, shotDistance))
            {
                shotDistance = hit.distance;
                Debug.Log(hit.transform.name);
                EnemyTargetCollision target = hit.transform.GetComponent<EnemyTargetCollision>();
                if(target!=null)
                {
                    target.TakeDamage(damage);
                }
            }

            nextPossibleShot = Time.time + secondsBetweenShots;
            Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            
            //Rigidbody newShell = Instantiate(shell, shellEjectionpoiint.position, Quaternion.identity) as Rigidbody;
           // newShell.AddForce(shellEjectionpoiint.forward * Random.Range(150f, 200f) + spawn.forward * Random.Range(-10f, 10f));

        }
        
    }

    public void ShootAuto()
    {
        if(gunType == GunType.Auto)
        {
            Shoot();
        }
    }

    private bool CanShoot()
    {
        bool canShoot = true;

        if(Time.time < nextPossibleShot)
        {
            canShoot = false;
        }
        return canShoot;
    }
}
