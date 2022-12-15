using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{   
    [Header ("Health")]

    [SerializeField] private float maxhealth = 100f;
    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioClip death;
    private Animator anim;
    private bool dead = false;
    public float currhealth {get; private set;}
    
    [Header ("Iframes")]
    [SerializeField] private float inv = 5f;
    [SerializeField] private int num_flash = 4;
    private SpriteRenderer sp;


    private void Awake(){
        currhealth = maxhealth;
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }
  
    public void TakeDamage(float damage){

        currhealth = Mathf.Clamp(currhealth - damage,0f,maxhealth);
        
        if(currhealth > 0){ 
            anim.SetTrigger("Hurt");
            SoundManager.instance.PlaySound(sound);
            StartCoroutine(Invincible());
        }
        else if(!dead){
            anim.SetTrigger("Die");
            SoundManager.instance.PlaySound(death);
            
            if(GetComponent<Player_Movement>() != null){
                
                SceneManager.LoadScene("Game"); 
                GetComponent<Player_Movement>().enabled =false;
            }

            if(GetComponentInParent<Patrol>() != null)
                GetComponentInParent<Patrol>().enabled = false;
            
            if(GetComponent<Enemy>() != null)
                GetComponent<Enemy>().enabled = false;
            
            dead = true;
            }
        }

    public void GiveHealth(float health){
        currhealth = Mathf.Clamp(currhealth + health,0f,maxhealth);
    }

    private IEnumerator Invincible(){

        Physics2D.IgnoreLayerCollision(6,8,true);

        for (int i = 0; i < num_flash; i++){
            sp.color = new Color(1,0,0,0.5f);
            yield return new WaitForSeconds(inv/num_flash);       
            sp.color = Color.white;
            yield return new WaitForSeconds(inv/num_flash);     
        }
        Physics2D.IgnoreLayerCollision(6,8,false);
    
    }

    private void Deactivate(){
        gameObject.SetActive(false);
    }

}
