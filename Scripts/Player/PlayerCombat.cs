using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour{   
    
    [SerializeField] private Transform atkpoint;
    [SerializeField] private LayerMask enemylayer;
    [SerializeField] private AudioClip sound;

    private Animator animator;
    
    
    public int damage = 20;
    public float atkrange = 0.5f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update(){
        
            if (Input.GetKeyDown(KeyCode.Z))
                Attack();
               
    }

    void Attack(){
        animator.SetTrigger("Attack");
        SoundManager.instance.PlaySound(sound);

        Collider2D[] hit = Physics2D.OverlapCircleAll(atkpoint.position,atkrange,enemylayer);

        foreach (Collider2D enemy in hit){

            enemy.GetComponent<Health>().TakeDamage(damage);
            
        }
    }
}
