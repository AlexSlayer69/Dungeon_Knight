using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    [SerializeField] private AudioClip sound;

    public void ButtonPress(){
        SoundManager.instance.PlaySound(sound);
    }

    public void Quit(){
        Application.Quit();
    }
}
