using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject game;
  void OnTriggerEnter2D(Collider2D other){
      game.GetComponent<Loader>().ChangeLvl(0);
  }
}
