using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{   
    [Header ("Patrol Points")]
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private Animator anim;

    [Header ("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float idle;
    private float idletimer;
    private Vector3 initScale;
    private bool movingleft;
    private bool right_dir;
    

    private void Awake(){
        initScale = enemy.localScale;
    }

    void Update(){
        
        if(movingleft){
            if(enemy.position.x >+ left.position.x){
                idletimer = 0;
                anim.SetBool("Moving",true);
                Move(-1);
            }
        
            else ChangeDirection();
        }
        else {
            if(enemy.position.x <= right.position.x){
                idletimer = 0;
                anim.SetBool("Moving",true);
                Move(1);
                }
            else ChangeDirection();
        }
    }

    private void ChangeDirection(){
        anim.SetBool("Moving",false);
        idletimer += Time.deltaTime;

        if(idletimer > idle){
            movingleft = !movingleft;
        }
    }

    private void Move(int direction){

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x)*direction,initScale.y,initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime *direction*speed,enemy.position.y,enemy.position.z);

    }

    private  void OnDisable()
    {
        anim.SetBool("Moving",false);
    }
}
