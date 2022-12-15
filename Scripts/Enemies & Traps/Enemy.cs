using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float atkcooldown;
    [SerializeField] private float range;
    [SerializeField] private float distance;
    [SerializeField] private int damage;
    
    [Header ("Colliders")]
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private Transform transform;
    
    [Header ("Player")]
    [SerializeField] private LayerMask playerlayer;
    private Health playerhealth;


    [Header ("Sounds")]
    [SerializeField] private AudioClip move;
    [SerializeField] private AudioClip attack;

    private float timer = Mathf.Infinity;
    private Animator anim;
    private Patrol enemypatrol;

    private void Awake(){
        anim = GetComponent<Animator>();
        enemypatrol = GetComponentInParent<Patrol>();
    }

    private void Update(){

        timer += Time.deltaTime;
        
        if(PlayerDetected()){

            if(timer >= atkcooldown){
                timer = 0;
                anim.SetTrigger("Attack");
                SoundManager.instance.PlaySound(attack);
            }
        
        }

        if(enemypatrol != null){
            enemypatrol.enabled = !PlayerDetected();
        }
    }   

    public void MoveSound(){
        SoundManager.instance.PlaySound(move);
    }

    private bool PlayerDetected(){

        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center + transform.right*distance*transform.localScale.x,
        new Vector3(box.bounds.size.x + range,box.bounds.size.y,box.bounds.size.z),0,Vector2.left,0,playerlayer);

        if(hit.collider != null){
            playerhealth = hit.transform.GetComponent<Health>();
        }

        return (hit.collider != null);

    }

    private void DamagePlayer(){
        if(PlayerDetected()){
            playerhealth.TakeDamage(damage);
        }
    }


    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.bounds.center + transform.right * distance*transform.localScale.x,
        new Vector3(box.bounds.size.x +range,box.bounds.size.y,box.bounds.size.z));
    }
}