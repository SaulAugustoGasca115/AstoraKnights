using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour
{
    [Header("Skill Damage Attributes")]
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float radius = 0.50f;
    [SerializeField] float damageCount = 10.0f;
    EnemyHealth enemyHealth;
    bool collided = false;


    // Update is called once per frame
    void Update()
    {
        CheckForDamage();
    }


    void CheckForDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position,radius,enemyMask);

        foreach(Collider c in hits)
        {
            enemyHealth = c.gameObject.GetComponent<EnemyHealth>();
            collided = true;
        }

        if(collided)
        {
            enemyHealth.TakeDamage(damageCount);
            enabled = false;
        }
    }
}
