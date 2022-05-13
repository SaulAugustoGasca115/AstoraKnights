using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornado : MonoBehaviour
{
    [Header("Fire Tornado Attributes")]
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float radius = 0.50f;
    [SerializeField] float damageCount = 20.0f;
    public GameObject fireExplosion;
    //EnemyHealth enemyHealth;
    bool collided = false;
    [SerializeField] float speed = 5.0f;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.LookRotation(player.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckForDamage();
    }


    void Move()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    void CheckForDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position,radius,enemyMask);

        foreach(Collider c in hits)
        {
            //enemyHealth = c.gameObject.GetComponent<EnemyHealth>();
            collided = true;
        }

        if(collided)
        {
            //enemyHealth.TakeDamage(damageCount);
            Vector3 temp = transform.position;
            temp.y += 2.0f;
            Instantiate(fireExplosion, temp, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
