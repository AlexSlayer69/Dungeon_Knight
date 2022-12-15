using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{   
    [SerializeField] private Animator anim;

    public void ChangeLvl(int lvl){
        StartCoroutine(Loadlvl(lvl));
    }

    IEnumerator Loadlvl(int lvl){
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(lvl);
    }
}