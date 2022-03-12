using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class ChickenController : MonoBehaviour
{
    //Handling
    public int maxHealth = 40;
    public int currentHealth;
    HealthBarScript healthBar;
    public float rotationSpeed = 600;
    public float walkSpeed = 5;
    public float runSpeed = 8;
    private CharacterController controller;
    private Camera cam;
    private Plane plane;

    //NOT YET IMPLEMENTED BELOW 
    //[SerializeField] private Animator chickenAnim;
   // [SerializeField] private string chickenRun;

    //[SerializeField] private string chickenRotate;
    //[SerializeField] private string chickenWalk;

    //NOT YET IMPLEMENTED

    public Gun gun;
    

    private Quaternion targetRotation;
    // Start is called before the first frame update
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }       
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        plane = new Plane(Vector3.up, Vector3.zero);
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
       ControlMouse();

       if(Input.GetButtonDown("Shoot"))
       {
           gun.Shoot();
       }else if(Input.GetButton("Shoot"))
       {
           gun.ShootAuto();
       }
        
    }
    public void StartPoint(){
        StartCoroutine("ResetGame");
    }
    private IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(1);
        gameObject.transform.position = new Vector3(15.12787f, 0.344f, 22.409f);
        Debug.Log("Resetting Game, current chcicken position "+transform.position);
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
    void ControlMouse()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if(plane.Raycast(ray, out var enter))
        {
            var hitPoint = ray.GetPoint(enter);

            var playerPositionOnPlane = plane.ClosestPointOnPlane(transform.position);
            transform.rotation = Quaternion.LookRotation(hitPoint - playerPositionOnPlane);
        }

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
      
        Vector3 motion = input.ToIso();
        motion *= (Mathf.Abs(input.ToIso().x)== 1 && Mathf.Abs(input.ToIso().z)==1)?.7f:1;
        motion *= (Input.GetButton("Dash"))?runSpeed:walkSpeed;
        motion += Vector3.up * -8;
        controller.Move(motion * Time.deltaTime);
    }

    void ControlWASD()
    {
         Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if(input != Vector3.zero){
            targetRotation = Quaternion.LookRotation(input.ToIso());
            transform.eulerAngles =Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        }

        Vector3 motion = input.ToIso();
        motion *= (Mathf.Abs(input.ToIso().x)== 1 && Mathf.Abs(input.ToIso().z)==1)?.7f:1;
        motion *= (Input.GetButton("Dash"))?runSpeed:walkSpeed;
        motion += Vector3.up * -8;
        controller.Move(motion * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if(collision.gameObject.tag == "Water")
        {
            Debug.Log("Has caido al agua");
            StartPoint();
        }
    }
}
    

public static class Helpers
    {
        private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    }   
