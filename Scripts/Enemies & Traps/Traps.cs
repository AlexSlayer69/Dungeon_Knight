using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    [SerializeField] private float damage = 100;
    
    void OnTriggerEnter2D(Collider2D collison){
        if(collison.tag == "Player")
            collison.GetComponent<Health>().TakeDamage(damage);
    }
}
