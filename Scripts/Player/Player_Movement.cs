using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour{
    
    //Private variables
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool right = true;
    private float walljumpcooldown;
    private BoxCollider2D box;
    private float x_input;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private LayerMask walllayer;
    [SerializeField] private Transform ceil;
    private bool crouch  = false;
    private CircleCollider2D circle;
    
    //Audio
    [SerializeField] private AudioClip move;
    [SerializeField] private AudioClip jump;  
    
    //Public variables
    public float velocity = 1f;
    public float jump_velocity = 1f;
    public float under = 0.1f;
    public float walljump = 0.2f;
    public float wall_h = 10f;
    public float wall_v = 0.1f;
    public float radius = 0.2f;
    public float crouchfactor = 0.36f;

    private void Awake(){
        rb2d = GetComponent<Rigidbody2D>(); // Get the rigidbody
        anim = GetComponent<Animator>(); //Get animator
        box = GetComponent<BoxCollider2D>(); // Get the Box collider
        circle = GetComponent<CircleCollider2D>(); // Get the Circle collider
    }

    private void Update(){
        
        x_input =  Input.GetAxisRaw("Horizontal");
        
        if(walljumpcooldown > walljump){
        
            if(crouch) rb2d.velocity = new Vector2(x_input * velocity * crouchfactor,rb2d.velocity.y);
            else rb2d.velocity = new Vector2(x_input * velocity,rb2d.velocity.y);

            if(onWall() && !isGrounded()){
                rb2d.gravityScale = 0;
                rb2d.velocity = Vector2.zero;
            }
            else rb2d.gravityScale = 1;

            if(Input.GetButtonDown("Jump") && !crouch) Jump(); 
        }
        else{
            walljumpcooldown += Time.deltaTime;
        }

        if((Input.GetAxis("Crouch") != 0) && isGrounded()){ 
            crouch = true;
            box.enabled = false;
            anim.SetBool("Crouch",true);
        }
        else if(!Physics2D.OverlapCircle(ceil.position,radius,groundlayer)){
            crouch = false;
            anim.SetBool("Crouch",false);
            box.enabled = true;
        }

        //animation
        if(x_input < 0 && right) Flip();
        else if(x_input > 0 && !right) Flip();

        anim.SetFloat("Speed",Mathf.Abs(x_input));
        anim.SetBool("Grounded",isGrounded());
        anim.SetBool("Wall",onWall());

    }			

    public void MoveSound(){
            SoundManager.instance.PlaySound(move);
    }


    private bool isGrounded(){

        RaycastHit2D rch = Physics2D.BoxCast(circle.bounds.center,circle.bounds.size,0,Vector2.down,under,groundlayer);     

        return (rch.collider != null);

    }

    private bool onWall(){

        RaycastHit2D rch = Physics2D.BoxCast(box.bounds.center,box.bounds.size,0,new Vector2(transform.localScale.x,0),under,walllayer);     

        return (rch.collider != null);

    }

    private void Jump(){
        if (isGrounded()){
            rb2d.velocity = new Vector2(rb2d.velocity.x,jump_velocity);
            anim.SetTrigger("Jump");
            SoundManager.instance.PlaySound(jump);
        }
        else if(onWall() && !isGrounded()){
                    rb2d.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*wall_h,wall_v);
                    Flip();               
                walljumpcooldown = 0;
            }
        
    }

    private void Flip(){
        right = !right;
        transform.localScale =  new Vector3(-1*transform.localScale.x,transform.localScale.y,transform.localScale.z);
    }
}
