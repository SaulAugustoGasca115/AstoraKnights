using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Attributes")]
    [SerializeField] float health = 100.0f;
    bool isShielded = false;
    Animator animator;
    [SerializeField] Image healthImg;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        healthImg = GameObject.Find("Health Icon").GetComponent<Image>();
    }

    public bool Shielded
    {
        get { return isShielded; }
        set { isShielded = value; }
    }

   public void TakeDamage(float amount)
    {
        if(!isShielded)
        {
            health -= amount;

            healthImg.fillAmount = health / 100.0f;

            if(health <= 0.0f)
            {
                animator.SetBool("Death",true);

                if(!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Death") 
                    && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f )
                {
                    Destroy(this.gameObject, 2.5f);
                }

            }
        }
    }

    public void HealPlayer(float healAmount)
    {
        health += healAmount;

        if(healAmount > 100)
        {
            health = 100.0f;
        }

        healthImg.fillAmount = health / 100.0f;
    }


}
