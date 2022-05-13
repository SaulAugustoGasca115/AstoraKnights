using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health Stats")]
    public float health = 150.0f;
    [SerializeField] Image healthImg;

    public void TakeDamage(float damage)
    {
        health -= damage;

        healthImg.fillAmount = health / 100.0f;


    }
}
