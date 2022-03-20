using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class ChickenController : MonoBehaviour
{
    //Handling
    //Max health that a player can have 
    public int maxHealth = 40;
    //Current health the player has
    public int currentHealth;
    //Reference to the healthbar script attached to the healthbar
    HealthBarScript healthBar;
    //Max rotation speed of the player
    public float rotationSpeed = 600;
    // Walking speed that the player has (without pressing left shift)
    public float walkSpeed = 5;
    //running speed the player has, (pressing left shift)
    public float runSpeed = 8;
    //Reference to the character controller component in the gameobject
    private CharacterController controller;
    //reference to the camera, so it can translate the rotation of the player to the mouse position in the camera
    private Camera cam;
    private Plane plane;
    //Reference to the stats in the hud gameobject
    StatCount statCount;
    //The gun the player carries
    public Gun gun;
    
    // Function called by the enemies to hit the player dealing damage, if the heaalth goes below 0, the scene loads again in StartPoint()
    public void TakeDamage(int damage)
    {
        if(currentHealth>0)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
        else{
            StartPoint();
        }
        
    }
    //Refers to the variables declared        
    void Start()
    {
        controller = GetComponent<CharacterController>();
        statCount = GameObject.FindGameObjectWithTag("HUD").GetComponent<StatCount>();
        cam = Camera.main;
        plane = new Plane(Vector3.up, Vector3.zero);
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    // Calls the ControlMouse on update so it can move with WASD and rotate around looking to the mouse
    void Update()
    {
       ControlMouse();
        //While having the gun it shoots depending on the rpm in the gun in semi
       if(Input.GetButtonDown("Shoot"))
       {
           gun.Shoot();
       }
       //Shooting while having an automatic gun
       else if(Input.GetButton("Shoot"))
       {
           gun.ShootAuto();
       }else if(Input.GetButtonUp("Shoot")&&gun.gunType == Gun.GunType.Auto){
           gun.audioSource.Stop();
       }
        
    }
    //Loads the scene and resets rounds, for when the player dies
    public void StartPoint(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        statCount.ResetRounds();
        statCount.DisplayRound();
    }
   
    void ControlMouse()
    {
        //Points a ray to the point of the mouse on the screen
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        //Rotation to the mouse in the plane
        if(plane.Raycast(ray, out var enter))
        {
            var hitPoint = ray.GetPoint(enter);

            var playerPositionOnPlane = plane.ClosestPointOnPlane(transform.position);
            transform.rotation = Quaternion.LookRotation(hitPoint - playerPositionOnPlane);
        }
        //Grabs the horizontal and vertical inputs of the player
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        // Where the movement is actually declared in an iso enviroment (thanks to DapperDino for the tutorials)
        Vector3 motion = input.ToIso();
        motion *= (Mathf.Abs(input.ToIso().x)== 1 && Mathf.Abs(input.ToIso().z)==1)?.7f:1;
        motion *= (Input.GetButton("Dash"))?runSpeed:walkSpeed;
        motion += Vector3.up * -8;
        controller.Move(motion * Time.deltaTime);
    }

   
    // If the player collides with water, the scene resets too
    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if(collision.gameObject.tag == "Water")
        {
            StartPoint();
        }
    }
   
  
}
    
// Helpers that makes the game look and play ISO in a 3d enviroment, changes the mouse input to correct the angles in an iso plane
public static class Helpers
    {
        private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    }   
