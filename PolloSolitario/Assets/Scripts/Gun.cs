using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    //Enum to see wether the weapon is automatic or semiautomatic (this is not too ealborated and wouldve liked to make more out of this) 
    public enum GunType {Semi, Auto};
    // Float that represents the seconds between shots
    private float secondsBetweenShots;
    //The base damage the weapon of the chicken deals
    public int damage = 10;
    //The rounds per minute of the weapon
    public float rpm;
    //The gameobject that is shot from the gun
    public GameObject projectile;
    //Time between the time shot and the secondBetweenShots variable
    private float nextPossibleShot;
    //To declare from the editor if the gun is semi or auto
    public GunType gunType;
    //The spawn point from where the bullet is shot
    public Transform spawn;
    public AudioSource audioSource;

    void Start()
    {
        secondsBetweenShots = 60/rpm;
        audioSource= GetComponent<AudioSource>();
    }
    //Shoot function that is called in the update of the character controller script
    public void Shoot() 
    {
        //If he can shoot draws a ray from the spawn point forwards (to the way the character is facing / to the mouse)
        if(CanShoot())
        {
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;

            float shotDistance = 1000;
            if(Physics.Raycast(ray, out hit, shotDistance))
            {
                
                shotDistance = hit.distance;
                ChaserEnemyController target = hit.transform.GetComponent<ChaserEnemyController>();
                RatEnemyController ratTarget = hit.transform.GetComponent<RatEnemyController>();
                ShootingEnemyController shootingTarget = hit.transform.GetComponent<ShootingEnemyController>();
                BeeEnemyController bee = hit.transform.GetComponent<BeeEnemyController>();

                //Checks which enemy has it hit, so it can deal damage to all of them
                if(target!=null)
                {
                    target.TakeDamage(damage);
                }
                if(shootingTarget!=null){

                    shootingTarget.TakeDamage(damage);
                }
                if(ratTarget!=null){

                    ratTarget.TakeDamage(damage);
                }
                if(bee!=null){
                    bee.TakeDamage(damage);
                }
            }

            nextPossibleShot = Time.time + secondsBetweenShots;

            //To see the ray drawn in gizmos
            Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);
            //PLays the audio attached to the gun
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            

            //Instantiates a bullet in the way the ray has been drawn with a rigidbody
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 60f, ForceMode.Impulse);
            StartCoroutine(destroyBullet(bullet));
            

        }
        
    }

    //If the gun is auto it shoots deppending on the rpm
    public void ShootAuto()
    {
        if(gunType == GunType.Auto)
        {
            Shoot();
        }
    }
    //Checks if the next shot is available
    private bool CanShoot()
    {
        bool canShoot = true;

        if(Time.time < nextPossibleShot)
        {
            canShoot = false;
        }
        return canShoot;
    }
    //destroys the bullet 3ms after being instantiated
     private IEnumerator destroyBullet(GameObject bullet){

        yield return new WaitForSeconds(0.3f);
        Destroy(bullet);
    }
    //Drops the gun when colliding with other guns in the game (i got quite stuck here)
    public void DropGun()
    {
        gameObject.transform.parent = null;
        Destroy(gameObject);

    }
}
