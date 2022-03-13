using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    //[SerializeField] public AudioSource audio;
    public enum GunType {Semi, Burst, Auto};
    private float secondsBetweenShots;

    public int damage = 10;

    //public Transform shellEjectionpoiint;
   // public Rigidbody shell;
    public float rpm;

    public GameObject projectile;
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
                ChaserEnemyController target = hit.transform.GetComponent<ChaserEnemyController>();
                ShootingEnemyController shootingTarget = hit.transform.GetComponent<ShootingEnemyController>();
                if(target!=null)
                {
                    target.TakeDamage(damage);
                }
                if(shootingTarget!=null){

                    shootingTarget.TakeDamage(damage);
                }
            }

            nextPossibleShot = Time.time + secondsBetweenShots;
            Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 60f, ForceMode.Impulse);
            StartCoroutine(destroyBullet(bullet));
            

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
     private IEnumerator destroyBullet(GameObject bullet){

        yield return new WaitForSeconds(1);
        Destroy(bullet);
    }
}
