using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerhealth;
    [SerializeField] private Image thbar;
    [SerializeField] private Image chbar;

    void Start(){

        thbar.fillAmount = playerhealth.currhealth/100;
    }

    void Update()
    {
        chbar.fillAmount = playerhealth.currhealth/100; 
    }

}
